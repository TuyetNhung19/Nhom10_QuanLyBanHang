// File: QuanLyBanSach.DAL/SachDAL.cs
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Truy cập bảng Sach (JOIN LoaiSach để lấy TenLoai).
/// </summary>
public class SachDAL
{
    /// <summary>Danh sách cột dùng chung cho các câu SELECT sách (khớp cột của sp_TimSach).</summary>
    private const string CotSach = @"s.MaSach, s.ISBN, s.TenSach, s.TacGia, s.NhaXuatBan, s.MaLoai, l.TenLoai,
                                     s.GiaBan, s.SoLuongTon, s.TonToiThieu, s.AnhBia, s.TrangThai";

    /// <summary>
    /// Tìm sách theo mã, ISBN, tên, tác giả (không phân biệt hoa thường/dấu) – sp_TimSach.
    /// </summary>
    /// <param name="tuKhoa">Từ khóa; rỗng = tất cả.</param>
    /// <param name="maLoai">Lọc theo loại; null = tất cả.</param>
    /// <param name="chiDangBan">true chỉ lấy sách đang bán.</param>
    public List<Sach> TimSach(string? tuKhoa, int? maLoai, bool chiDangBan)
    {
        return Database.ExecuteReader("sp_TimSach", DocSach, CommandType.StoredProcedure,
            Database.TaoThamSo("@TuKhoa", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(tuKhoa) ? null : tuKhoa.Trim(), 255),
            Database.TaoThamSo("@MaLoai", SqlDbType.Int, maLoai),
            Database.TaoThamSo("@ChiDangBan", SqlDbType.Bit, chiDangBan));
    }

    /// <summary>Lấy một sách theo mã (kể cả sách đã ẩn); null nếu không có.</summary>
    public Sach? LayTheoMa(string maSach)
    {
        string sql = $@"SELECT {CotSach}
                        FROM dbo.Sach AS s INNER JOIN dbo.LoaiSach AS l ON l.MaLoai = s.MaLoai
                        WHERE s.MaSach = @MaSach";
        return Database.ExecuteReader(sql, DocSach, CommandType.Text,
            Database.TaoThamSo("@MaSach", SqlDbType.VarChar, maSach, 20)).FirstOrDefault();
    }

    /// <summary>
    /// ĐT1: tìm đúng một sách ĐANG BÁN theo mã sách hoặc ISBN (gõ/quét mã + Enter) – sp_LaySachTheoMaHoacISBN.
    /// </summary>
    public Sach? LayTheoMaHoacISBN(string ma)
    {
        return Database.ExecuteReader("sp_LaySachTheoMaHoacISBN", DocSach, CommandType.StoredProcedure,
            Database.TaoThamSo("@Ma", SqlDbType.VarChar, ma.Trim(), 20)).FirstOrDefault();
    }

    /// <summary>
    /// Lấy thông tin hiện hành (giá, tồn, trạng thái) của nhiều sách một lần – dùng khi gọi lại
    /// đơn treo để kiểm tra lại tồn và cập nhật giá (BH-01). Mỗi mã là một tham số @p0..@pn,
    /// chỉ ghép TÊN tham số vào câu SQL, không ghép giá trị.
    /// </summary>
    public List<Sach> LayTheoDanhSachMa(IEnumerable<string> dsMa)
    {
        List<string> maKhongTrung = dsMa
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Select(m => m.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (maKhongTrung.Count == 0)
        {
            return new List<Sach>();
        }

        var thamSo = new SqlParameter[maKhongTrung.Count];
        var tenThamSo = new string[maKhongTrung.Count];
        for (int i = 0; i < maKhongTrung.Count; i++)
        {
            tenThamSo[i] = "@p" + i;
            thamSo[i] = Database.TaoThamSo(tenThamSo[i], SqlDbType.VarChar, maKhongTrung[i], 20);
        }

        string sql = $@"SELECT {CotSach}
                        FROM dbo.Sach AS s INNER JOIN dbo.LoaiSach AS l ON l.MaLoai = s.MaLoai
                        WHERE s.MaSach IN ({string.Join(", ", tenThamSo)})";
        return Database.ExecuteReader(sql, DocSach, CommandType.Text, thamSo);
    }

    /// <summary>
    /// Sinh mã sách mới dạng S + 4 số = số lớn nhất hiện có + 1 (ví dụ S0041).
    /// </summary>
    public string TaoMaSachMoi()
    {
        const string sql = @"SELECT MAX(CAST(SUBSTRING(MaSach, 2, 4) AS INT))
                             FROM dbo.Sach
                             WHERE MaSach LIKE @Mau";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@Mau", SqlDbType.VarChar, "S[0-9][0-9][0-9][0-9]", 30));
        int soCuoi = kq == null ? 0 : Convert.ToInt32(kq);
        return "S" + (soCuoi + 1).ToString("0000");
    }

    /// <summary>Kiểm tra mã sách đã tồn tại chưa (SP-01).</summary>
    public bool KiemTraTrungMa(string maSach)
    {
        const string sql = "SELECT COUNT(*) FROM dbo.Sach WHERE MaSach = @MaSach";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@MaSach", SqlDbType.VarChar, maSach, 20));
        return Convert.ToInt32(kq ?? 0) > 0;
    }

    /// <summary>
    /// Kiểm tra ISBN đã được sách khác dùng chưa.
    /// </summary>
    /// <param name="isbn">ISBN cần kiểm tra.</param>
    /// <param name="boQuaMaSach">Mã sách đang sửa; null khi thêm mới.</param>
    public bool KiemTraTrungISBN(string isbn, string? boQuaMaSach)
    {
        const string sql = @"SELECT COUNT(*) FROM dbo.Sach
                             WHERE ISBN = @ISBN AND (@BoQuaMaSach IS NULL OR MaSach <> @BoQuaMaSach)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@ISBN", SqlDbType.VarChar, isbn, 13),
            Database.TaoThamSo("@BoQuaMaSach", SqlDbType.VarChar, boQuaMaSach, 20));
        return Convert.ToInt32(kq ?? 0) > 0;
    }

    /// <summary>Sách đã xuất hiện trong hóa đơn nào chưa (SP-03: đã bán thì chỉ được ẩn).</summary>
    public bool DaCoTrongHoaDon(string maSach)
    {
        const string sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.ChiTietHoaDon WHERE MaSach = @MaSach) THEN 1 ELSE 0 END";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@MaSach", SqlDbType.VarChar, maSach, 20));
        return Convert.ToInt32(kq ?? 0) == 1;
    }

    /// <summary>Thêm sách mới.</summary>
    public bool Them(Sach s)
    {
        const string sql = @"INSERT INTO dbo.Sach (MaSach, ISBN, TenSach, TacGia, NhaXuatBan, MaLoai,
                                                   GiaBan, SoLuongTon, TonToiThieu, AnhBia, TrangThai)
                             VALUES (@MaSach, @ISBN, @TenSach, @TacGia, @NhaXuatBan, @MaLoai,
                                     @GiaBan, @SoLuongTon, @TonToiThieu, @AnhBia, @TrangThai)";
        return Database.ExecuteNonQuery(sql, CommandType.Text, ThamSoSach(s)) > 0;
    }

    /// <summary>Sửa thông tin sách theo MaSach (BLL kiểm tra quyền sửa giá).</summary>
    public bool Sua(Sach s)
    {
        const string sql = @"UPDATE dbo.Sach
                             SET ISBN = @ISBN, TenSach = @TenSach, TacGia = @TacGia, NhaXuatBan = @NhaXuatBan,
                                 MaLoai = @MaLoai, GiaBan = @GiaBan, SoLuongTon = @SoLuongTon,
                                 TonToiThieu = @TonToiThieu, AnhBia = @AnhBia, TrangThai = @TrangThai
                             WHERE MaSach = @MaSach";
        return Database.ExecuteNonQuery(sql, CommandType.Text, ThamSoSach(s)) > 0;
    }

    /// <summary>Ẩn (false) hoặc hiện lại (true) sách (SP-03).</summary>
    public bool CapNhatTrangThai(string maSach, bool dangBan)
    {
        const string sql = "UPDATE dbo.Sach SET TrangThai = @TrangThai WHERE MaSach = @MaSach";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@TrangThai", SqlDbType.Bit, dangBan),
            Database.TaoThamSo("@MaSach", SqlDbType.VarChar, maSach, 20)) > 0;
    }

    /// <summary>Sách đang bán có tồn &lt;= tồn tối thiểu – sp_SachSapHet.</summary>
    /// <param name="maLoai">Lọc theo loại; null = tất cả.</param>
    public List<Sach> LaySachSapHet(int? maLoai)
    {
        return Database.ExecuteReader("sp_SachSapHet", DocSach, CommandType.StoredProcedure,
            Database.TaoThamSo("@MaLoai", SqlDbType.Int, maLoai));
    }

    /// <summary>Tạo bộ tham số đủ 11 cột của Sach (dùng cho Them và Sua).</summary>
    private static SqlParameter[] ThamSoSach(Sach s) => new[]
    {
        Database.TaoThamSo("@MaSach", SqlDbType.VarChar, s.MaSach, 20),
        Database.TaoThamSo("@ISBN", SqlDbType.VarChar, string.IsNullOrWhiteSpace(s.ISBN) ? null : s.ISBN, 13),
        Database.TaoThamSo("@TenSach", SqlDbType.NVarChar, s.TenSach, 255),
        Database.TaoThamSo("@TacGia", SqlDbType.NVarChar, s.TacGia, 100),
        Database.TaoThamSo("@NhaXuatBan", SqlDbType.NVarChar, s.NhaXuatBan, 100),
        Database.TaoThamSo("@MaLoai", SqlDbType.Int, s.MaLoai),
        Database.TaoThamSo("@GiaBan", SqlDbType.Decimal, s.GiaBan),
        Database.TaoThamSo("@SoLuongTon", SqlDbType.Int, s.SoLuongTon),
        Database.TaoThamSo("@TonToiThieu", SqlDbType.Int, s.TonToiThieu),
        Database.TaoThamSo("@AnhBia", SqlDbType.NVarChar, s.AnhBia, 255),
        Database.TaoThamSo("@TrangThai", SqlDbType.Bit, s.TrangThai)
    };

    /// <summary>
    /// Ánh xạ một dòng thành Sach. Đọc được kết quả của sp_TimSach, sp_LaySachTheoMaHoacISBN,
    /// sp_SachSapHet (thiếu NhaXuatBan, AnhBia, TrangThai) và các câu SELECT trong lớp này.
    /// </summary>
    private static Sach DocSach(SqlDataReader r) => new()
    {
        MaSach = r.GetStringOrEmpty("MaSach"),
        ISBN = r.GetStringOrNull("ISBN"),
        TenSach = r.GetStringOrEmpty("TenSach"),
        TacGia = r.GetStringOrNull("TacGia"),
        NhaXuatBan = r.CoCot("NhaXuatBan") ? r.GetStringOrNull("NhaXuatBan") : null,
        MaLoai = r.GetIntOrZero("MaLoai"),
        TenLoai = r.GetStringOrEmpty("TenLoai"),
        GiaBan = r.GetDecimalOrZero("GiaBan"),
        SoLuongTon = r.GetIntOrZero("SoLuongTon"),
        TonToiThieu = r.GetIntOrZero("TonToiThieu"),
        AnhBia = r.CoCot("AnhBia") ? r.GetStringOrNull("AnhBia") : null,
        TrangThai = !r.CoCot("TrangThai") || r.GetBoolOrFalse("TrangThai")
    };
}
