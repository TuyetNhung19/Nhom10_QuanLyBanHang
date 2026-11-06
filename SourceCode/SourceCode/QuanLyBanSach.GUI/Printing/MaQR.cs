// File: QuanLyBanSach.GUI/Printing/MaQR.cs
using System.Text;

namespace QuanLyBanSach.GUI.Printing;

/// <summary>
/// Tự sinh mã QR (chuẩn ISO/IEC 18004) để in trên hóa đơn – không dùng thư viện ngoài, đúng quy ước đồ án.
/// Phạm vi: chế độ byte (UTF-8), mức sửa lỗi M (~15%), phiên bản 1–10 (tối đa 213 byte) – đủ cho chuỗi VietQR.
/// Các bước: mã hóa dữ liệu → Reed-Solomon → xếp xen kẽ khối → vẽ mẫu cố định → đặt bit dữ liệu
/// → thử 8 mặt nạ, chọn mặt nạ có điểm phạt nhỏ nhất → ghi thông tin định dạng/phiên bản.
/// </summary>
public static class MaQR
{
    /// <summary>Số byte tối đa mã hóa được (phiên bản 10, mức M).</summary>
    public const int SoByteToiDa = 213;

    /// <summary>
    /// Bảng mức M cho phiên bản 1–10: (số mã sửa lỗi mỗi khối, số khối nhóm 1, số byte dữ liệu/khối nhóm 1, số khối nhóm 2).
    /// Khối nhóm 2 có nhiều hơn nhóm 1 đúng 1 byte dữ liệu.
    /// </summary>
    private static readonly (int EcMoiKhoi, int SoKhoi1, int DuLieuKhoi1, int SoKhoi2)[] BangMucM =
    {
        (0, 0, 0, 0),     // chỉ số 0 không dùng
        (10, 1, 16, 0), (16, 1, 28, 0), (26, 1, 44, 0), (18, 2, 32, 0), (24, 2, 43, 0),
        (16, 4, 27, 0), (18, 4, 31, 0), (22, 2, 38, 2), (22, 3, 36, 2), (26, 4, 43, 1)
    };

    /// <summary>Tọa độ tâm các mẫu căn chỉnh (alignment) của phiên bản 1–10.</summary>
    private static readonly int[][] ViTriCanChinh =
    {
        Array.Empty<int>(), Array.Empty<int>(), new[] { 6, 18 }, new[] { 6, 22 }, new[] { 6, 26 }, new[] { 6, 30 },
        new[] { 6, 34 }, new[] { 6, 22, 38 }, new[] { 6, 24, 42 }, new[] { 6, 26, 46 }, new[] { 6, 28, 50 }
    };

    /// <summary>
    /// Tạo ma trận QR cho chuỗi (UTF-8). Phần tử [hàng, cột] = true là ô đen. Không gồm vùng trắng viền.
    /// </summary>
    /// <exception cref="ArgumentException">Chuỗi dài hơn <see cref="SoByteToiDa"/> byte.</exception>
    public static bool[,] TaoMaTran(string noiDung)
    {
        byte[] duLieu = Encoding.UTF8.GetBytes(noiDung);
        int phienBan = ChonPhienBan(duLieu.Length);
        byte[] maTu = ThemSuaLoi(MaHoaDuLieu(duLieu, phienBan), phienBan);

        int kichThuoc = 17 + 4 * phienBan;
        var o = new bool[kichThuoc, kichThuoc];
        var coDinh = new bool[kichThuoc, kichThuoc];
        VeMauCoDinh(o, coDinh, phienBan);
        DatDuLieu(o, coDinh, maTu);

        // Thử 8 mặt nạ, giữ mặt nạ ít điểm phạt nhất
        bool[,]? totNhat = null;
        int diemNhoNhat = int.MaxValue;
        for (int matNa = 0; matNa < 8; matNa++)
        {
            var thu = (bool[,])o.Clone();
            ApMatNa(thu, coDinh, matNa);
            GhiDinhDang(thu, matNa);
            int diem = DiemPhat(thu);
            if (diem < diemNhoNhat)
            {
                diemNhoNhat = diem;
                totNhat = thu;
            }
        }

        return totNhat!;
    }

    /// <summary>
    /// Vẽ mã QR vào hình vuông <paramref name="vung"/> (đã gồm vùng trắng viền 4 ô theo chuẩn).
    /// Dùng được cho cả Graphics máy in lẫn Bitmap.
    /// </summary>
    public static void Ve(Graphics g, bool[,] maTran, RectangleF vung)
    {
        int n = maTran.GetLength(0);
        const int vienTrang = 4;
        float o = Math.Min(vung.Width, vung.Height) / (n + 2 * vienTrang);
        float x0 = vung.Left + (vung.Width - o * n) / 2;
        float y0 = vung.Top + (vung.Height - o * n) / 2;

        g.FillRectangle(Brushes.White, vung);
        var cu = g.SmoothingMode;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
        for (int hang = 0; hang < n; hang++)
        {
            for (int cot = 0; cot < n; cot++)
            {
                if (maTran[hang, cot])
                {
                    // Phủ dư 0,02 ô để không hở khe trắng giữa các ô khi phóng to
                    g.FillRectangle(Brushes.Black, x0 + cot * o, y0 + hang * o, o + 0.02F, o + 0.02F);
                }
            }
        }

        g.SmoothingMode = cu;
    }

    // ===================== 1. Mã hóa dữ liệu =====================

    private static int SoMaTuDuLieu(int v) => BangMucM[v].SoKhoi1 * BangMucM[v].DuLieuKhoi1 + BangMucM[v].SoKhoi2 * (BangMucM[v].DuLieuKhoi1 + 1);

    /// <summary>Phiên bản nhỏ nhất chứa đủ: 4 bit chế độ + độ dài (8 bit, từ v10 là 16 bit) + 8 bit/byte.</summary>
    private static int ChonPhienBan(int soByte)
    {
        for (int v = 1; v <= 10; v++)
        {
            int bitDoDai = v < 10 ? 8 : 16;
            if (4 + bitDoDai + 8 * soByte <= SoMaTuDuLieu(v) * 8)
            {
                return v;
            }
        }

        throw new ArgumentException($"Nội dung mã QR quá dài ({soByte} byte, tối đa {SoByteToiDa}).");
    }

    /// <summary>Chế độ byte 0100, độ dài, dữ liệu, kết thúc 0000, đệm 0xEC/0x11 cho đủ dung lượng.</summary>
    private static byte[] MaHoaDuLieu(byte[] duLieu, int v)
    {
        var bit = new List<bool>();
        void Them(int giaTri, int soBit)
        {
            for (int i = soBit - 1; i >= 0; i--)
            {
                bit.Add(((giaTri >> i) & 1) != 0);
            }
        }

        int dungLuong = SoMaTuDuLieu(v) * 8;
        Them(0b0100, 4);
        Them(duLieu.Length, v < 10 ? 8 : 16);
        foreach (byte b in duLieu)
        {
            Them(b, 8);
        }

        Them(0, Math.Min(4, dungLuong - bit.Count));
        Them(0, (8 - bit.Count % 8) % 8);
        for (int dem = 0; bit.Count < dungLuong; dem++)
        {
            Them(dem % 2 == 0 ? 0xEC : 0x11, 8);
        }

        var ketQua = new byte[bit.Count / 8];
        for (int i = 0; i < bit.Count; i++)
        {
            if (bit[i])
            {
                ketQua[i / 8] |= (byte)(0x80 >> (i % 8));
            }
        }

        return ketQua;
    }

    // ===================== 2. Reed-Solomon + xếp xen kẽ =====================

    /// <summary>Chia khối, tính mã sửa lỗi từng khối, rồi xếp xen kẽ dữ liệu và mã sửa lỗi theo cột.</summary>
    private static byte[] ThemSuaLoi(byte[] duLieu, int v)
    {
        (int ec, int soKhoi1, int dl1, int soKhoi2) = BangMucM[v];
        byte[] daThuc = DaThucSinh(ec);
        var khoi = new List<byte[]>();
        var maSua = new List<byte[]>();
        int viTri = 0;
        for (int k = 0; k < soKhoi1 + soKhoi2; k++)
        {
            int doDai = k < soKhoi1 ? dl1 : dl1 + 1;
            byte[] phan = duLieu.Skip(viTri).Take(doDai).ToArray();
            viTri += doDai;
            khoi.Add(phan);
            maSua.Add(PhanDu(phan, daThuc));
        }

        var ketQua = new List<byte>();
        for (int i = 0; i <= dl1; i++)
        {
            ketQua.AddRange(khoi.Where(b => i < b.Length).Select(b => b[i]));
        }

        for (int i = 0; i < ec; i++)
        {
            ketQua.AddRange(maSua.Select(b => b[i]));
        }

        return ketQua.ToArray();
    }

    /// <summary>Phép nhân trong trường GF(256) với đa thức nguyên thủy 0x11D.</summary>
    private static byte Nhan(byte a, byte b)
    {
        int kq = 0;
        for (int i = 7; i >= 0; i--)
        {
            kq = (kq << 1) ^ ((kq >> 7) * 0x11D);
            kq ^= ((b >> i) & 1) * a;
        }

        return (byte)kq;
    }

    /// <summary>Hệ số đa thức sinh (x − α⁰)(x − α¹)…(x − α^(n−1)), bỏ hệ số bậc cao nhất.</summary>
    private static byte[] DaThucSinh(int bac)
    {
        var kq = new byte[bac];
        kq[bac - 1] = 1;
        byte goc = 1;
        for (int i = 0; i < bac; i++)
        {
            for (int j = 0; j < bac; j++)
            {
                kq[j] = Nhan(kq[j], goc);
                if (j + 1 < bac)
                {
                    kq[j] ^= kq[j + 1];
                }
            }

            goc = Nhan(goc, 0x02);
        }

        return kq;
    }

    /// <summary>Phần dư khi chia đa thức dữ liệu cho đa thức sinh = các byte sửa lỗi.</summary>
    private static byte[] PhanDu(byte[] duLieu, byte[] daThuc)
    {
        var kq = new byte[daThuc.Length];
        foreach (byte b in duLieu)
        {
            byte heSo = (byte)(b ^ kq[0]);
            Array.Copy(kq, 1, kq, 0, kq.Length - 1);
            kq[^1] = 0;
            for (int i = 0; i < kq.Length; i++)
            {
                kq[i] ^= Nhan(daThuc[i], heSo);
            }
        }

        return kq;
    }

    // ===================== 3. Mẫu cố định =====================

    private static void Dat(bool[,] o, bool[,] coDinh, int hang, int cot, bool den)
    {
        o[hang, cot] = den;
        coDinh[hang, cot] = true;
    }

    /// <summary>Mẫu định vị (3 góc), vạch thời gian, mẫu căn chỉnh, ô đen cố định, chỗ cho định dạng/phiên bản.</summary>
    private static void VeMauCoDinh(bool[,] o, bool[,] coDinh, int v)
    {
        int n = o.GetLength(0);

        // Vạch thời gian (hàng 6, cột 6)
        for (int i = 0; i < n; i++)
        {
            Dat(o, coDinh, 6, i, i % 2 == 0);
            Dat(o, coDinh, i, 6, i % 2 == 0);
        }

        // Mẫu định vị 7×7 + viền trắng
        foreach ((int h0, int c0) in new[] { (3, 3), (3, n - 4), (n - 4, 3) })
        {
            for (int dh = -4; dh <= 4; dh++)
            {
                for (int dc = -4; dc <= 4; dc++)
                {
                    int h = h0 + dh, c = c0 + dc;
                    if (h >= 0 && h < n && c >= 0 && c < n)
                    {
                        int d = Math.Max(Math.Abs(dh), Math.Abs(dc));
                        Dat(o, coDinh, h, c, d != 2 && d != 4);
                    }
                }
            }
        }

        // Mẫu căn chỉnh 5×5 (bỏ 3 vị trí trùng mẫu định vị)
        int[] vt = ViTriCanChinh[v];
        for (int i = 0; i < vt.Length; i++)
        {
            for (int j = 0; j < vt.Length; j++)
            {
                bool trungGoc = (i == 0 && j == 0) || (i == 0 && j == vt.Length - 1) || (i == vt.Length - 1 && j == 0);
                if (trungGoc)
                {
                    continue;
                }

                for (int dh = -2; dh <= 2; dh++)
                {
                    for (int dc = -2; dc <= 2; dc++)
                    {
                        Dat(o, coDinh, vt[i] + dh, vt[j] + dc, Math.Max(Math.Abs(dh), Math.Abs(dc)) != 1);
                    }
                }
            }
        }

        // Giữ chỗ thông tin định dạng (ghi thật sau khi chọn mặt nạ) + ô đen cố định
        GhiDinhDang(o, 0, coDinh);

        // Thông tin phiên bản (từ phiên bản 7): 18 bit, BCH với đa thức 0x1F25
        if (v >= 7)
        {
            int du = v;
            for (int i = 0; i < 12; i++)
            {
                du = (du << 1) ^ ((du >> 11) * 0x1F25);
            }

            int bits = (v << 12) | du;
            for (int i = 0; i < 18; i++)
            {
                bool den = ((bits >> i) & 1) != 0;
                int a = n - 11 + i % 3, b = i / 3;
                Dat(o, coDinh, b, a, den);
                Dat(o, coDinh, a, b, den);
            }
        }
    }

    /// <summary>
    /// Ghi 15 bit định dạng (mức M = 00, 3 bit mặt nạ, BCH 0x537, XOR 0x5412) vào 2 vị trí, kèm ô đen cố định.
    /// </summary>
    private static void GhiDinhDang(bool[,] o, int matNa, bool[,]? coDinh = null)
    {
        int n = o.GetLength(0);
        int duLieu = (0b00 << 3) | matNa;
        int du = duLieu;
        for (int i = 0; i < 10; i++)
        {
            du = (du << 1) ^ ((du >> 9) * 0x537);
        }

        int bits = ((duLieu << 10) | du) ^ 0x5412;
        void Ghi(int hang, int cot, int i)
        {
            o[hang, cot] = ((bits >> i) & 1) != 0;
            if (coDinh != null)
            {
                coDinh[hang, cot] = true;
            }
        }

        // Bản 1: quanh mẫu định vị góc trên-trái
        for (int i = 0; i <= 5; i++)
        {
            Ghi(i, 8, i);
        }

        Ghi(7, 8, 6);
        Ghi(8, 8, 7);
        Ghi(8, 7, 8);
        for (int i = 9; i < 15; i++)
        {
            Ghi(8, 14 - i, i);
        }

        // Bản 2: tách đôi ở góc trên-phải và dưới-trái
        for (int i = 0; i < 8; i++)
        {
            Ghi(8, n - 1 - i, i);
        }

        for (int i = 8; i < 15; i++)
        {
            Ghi(n - 15 + i, 8, i);
        }

        o[n - 8, 8] = true;
        if (coDinh != null)
        {
            coDinh[n - 8, 8] = true;
        }
    }

    // ===================== 4. Đặt dữ liệu + mặt nạ =====================

    /// <summary>Đặt bit theo đường zic-zắc từng cặp cột từ góc dưới-phải, bỏ cột thời gian số 6.</summary>
    private static void DatDuLieu(bool[,] o, bool[,] coDinh, byte[] maTu)
    {
        int n = o.GetLength(0);
        int i = 0;
        for (int phai = n - 1; phai >= 1; phai -= 2)
        {
            if (phai == 6)
            {
                phai = 5;
            }

            bool diLen = ((phai + 1) & 2) == 0;
            for (int buoc = 0; buoc < n; buoc++)
            {
                int hang = diLen ? n - 1 - buoc : buoc;
                for (int j = 0; j < 2; j++)
                {
                    int cot = phai - j;
                    if (!coDinh[hang, cot] && i < maTu.Length * 8)
                    {
                        o[hang, cot] = ((maTu[i >> 3] >> (7 - (i & 7))) & 1) != 0;
                        i++;
                    }
                }
            }
        }
    }

    /// <summary>Đảo các ô dữ liệu theo công thức mặt nạ (x = cột, y = hàng).</summary>
    private static void ApMatNa(bool[,] o, bool[,] coDinh, int matNa)
    {
        int n = o.GetLength(0);
        for (int y = 0; y < n; y++)
        {
            for (int x = 0; x < n; x++)
            {
                bool dao = matNa switch
                {
                    0 => (x + y) % 2 == 0,
                    1 => y % 2 == 0,
                    2 => x % 3 == 0,
                    3 => (x + y) % 3 == 0,
                    4 => (x / 3 + y / 2) % 2 == 0,
                    5 => x * y % 2 + x * y % 3 == 0,
                    6 => (x * y % 2 + x * y % 3) % 2 == 0,
                    _ => ((x + y) % 2 + x * y % 3) % 2 == 0
                };
                if (dao && !coDinh[y, x])
                {
                    o[y, x] = !o[y, x];
                }
            }
        }
    }

    /// <summary>
    /// Điểm phạt theo chuẩn: (1) chuỗi ≥5 ô cùng màu, (2) khối 2×2 cùng màu,
    /// (3) mẫu giống mẫu định vị 1:1:3:1:1, (4) lệch tỉ lệ đen/trắng.
    /// </summary>
    private static int DiemPhat(bool[,] o)
    {
        int n = o.GetLength(0);
        int diem = 0;

        for (int luot = 0; luot < 2; luot++)
        {
            for (int a = 0; a < n; a++)
            {
                int chuoi = 1;
                for (int b = 1; b < n; b++)
                {
                    bool truoc = luot == 0 ? o[a, b - 1] : o[b - 1, a];
                    bool nay = luot == 0 ? o[a, b] : o[b, a];
                    if (nay == truoc)
                    {
                        chuoi++;
                        if (chuoi == 5)
                        {
                            diem += 3;
                        }
                        else if (chuoi > 5)
                        {
                            diem++;
                        }
                    }
                    else
                    {
                        chuoi = 1;
                    }
                }

                // Quy tắc 3: 1011101 kèm 4 ô trắng ở một bên
                for (int b = 0; b + 11 <= n; b++)
                {
                    bool O(int k) => luot == 0 ? o[a, b + k] : o[b + k, a];
                    // 0000 1011101  hoặc  1011101 0000
                    bool trangTruoc = !O(0) && !O(1) && !O(2) && !O(3) && O(4) && !O(5) && O(6) && O(7) && O(8) && !O(9) && O(10);
                    bool trangSau = O(0) && !O(1) && O(2) && O(3) && O(4) && !O(5) && O(6) && !O(7) && !O(8) && !O(9) && !O(10);
                    if (trangTruoc || trangSau)
                    {
                        diem += 40;
                    }
                }
            }
        }

        int soDen = 0;
        for (int h = 0; h < n; h++)
        {
            for (int c = 0; c < n; c++)
            {
                if (o[h, c])
                {
                    soDen++;
                }

                if (h + 1 < n && c + 1 < n && o[h, c] == o[h, c + 1] && o[h, c] == o[h + 1, c] && o[h, c] == o[h + 1, c + 1])
                {
                    diem += 3;
                }
            }
        }

        int phanTram = soDen * 100 / (n * n);
        diem += Math.Abs(phanTram - 50) / 5 * 10;
        return diem;
    }
}
