// File: QuanLyBanSach.GUI/Printing/NoiDungQRHoaDon.cs
using System.Configuration;
using System.Globalization;
using System.Text;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Printing;

/// <summary>
/// Dựng nội dung mã QR in trên hóa đơn, mang số tiền TỔNG THANH TOÁN của hóa đơn.
/// - Có khai báo tài khoản ngân hàng trong App.config (QR_MaNganHang + QR_SoTaiKhoan) → chuỗi VietQR
///   (chuẩn EMVCo của NAPAS): ứng dụng ngân hàng quét sẽ tự điền số tài khoản, số tiền, nội dung = mã hóa đơn.
/// - Chưa khai báo → chuỗi chữ thường (tên cửa hàng, mã hóa đơn, tổng tiền): camera điện thoại quét đọc được ngay.
/// </summary>
public static class NoiDungQRHoaDon
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    /// <summary>true nếu App.config đã khai báo đủ mã ngân hàng và số tài khoản (in VietQR).</summary>
    public static bool CoTaiKhoanNganHang =>
        !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["QR_MaNganHang"]) &&
        !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["QR_SoTaiKhoan"]);

    /// <summary>Chuỗi đưa vào mã QR cho hóa đơn.</summary>
    public static string Tao(HoaDon hd, string tenCuaHang)
    {
        if (CoTaiKhoanNganHang)
        {
            return TaoVietQR(
                ConfigurationManager.AppSettings["QR_MaNganHang"]!.Trim(),
                ConfigurationManager.AppSettings["QR_SoTaiKhoan"]!.Trim(),
                hd.TongThanhToan,
                hd.MaHD);
        }

        // Chữ không dấu: QR không khai báo bảng mã (ECI) nên một số máy quét đọc sai tiếng Việt có dấu
        return $"{BoDau(tenCuaHang, 60)}\nHoa don: {hd.MaHD}\nNgay: {hd.NgayLap:dd/MM/yyyy HH:mm}\n" +
               $"Tong thanh toan: {hd.TongThanhToan.ToString("#,##0", VanHoaVN)} VND";
    }

    /// <summary>Dòng chú thích in dưới mã QR.</summary>
    public static string ChuThich(HoaDon hd) =>
        (CoTaiKhoanNganHang ? "Quét bằng app ngân hàng để chuyển khoản " : "Quét mã để xem tổng tiền ") +
        hd.TongThanhToan.ToString("#,##0", VanHoaVN) + "đ";

    /// <summary>
    /// Chuỗi VietQR (EMVCo Merchant-Presented QR): mỗi trường = mã 2 số + độ dài 2 số + giá trị.
    /// 00 phiên bản | 01 "12" = QR động (có số tiền) | 38 tài khoản thụ hưởng NAPAS (BIN ngân hàng + số TK + dịch vụ QRIBFTTA)
    /// | 53 tiền tệ 704 (VND) | 54 số tiền | 58 quốc gia VN | 62/08 nội dung chuyển khoản | 63 mã kiểm tra CRC-16.
    /// </summary>
    /// <param name="maNganHang">Mã BIN 6 số của ngân hàng (ví dụ Vietcombank 970436).</param>
    public static string TaoVietQR(string maNganHang, string soTaiKhoan, decimal soTien, string noiDung)
    {
        string thuHuong = Truong("00", maNganHang) + Truong("01", soTaiKhoan);
        string taiKhoan = Truong("00", "A000000727") + Truong("01", thuHuong) + Truong("02", "QRIBFTTA");
        string tien = decimal.Round(soTien, 0, MidpointRounding.AwayFromZero).ToString("0", CultureInfo.InvariantCulture);

        var sb = new StringBuilder();
        sb.Append(Truong("00", "01"));
        sb.Append(Truong("01", "12"));
        sb.Append(Truong("38", taiKhoan));
        sb.Append(Truong("53", "704"));
        sb.Append(Truong("54", tien));
        sb.Append(Truong("58", "VN"));
        sb.Append(Truong("62", Truong("08", BoDau(noiDung, 25))));
        sb.Append("6304");
        sb.Append(Crc16(sb.ToString()).ToString("X4"));
        return sb.ToString();
    }

    private static string Truong(string ma, string giaTri) => ma + giaTri.Length.ToString("00") + giaTri;

    /// <summary>CRC-16/CCITT-FALSE (đa thức 0x1021, giá trị đầu 0xFFFF) theo chuẩn EMVCo.</summary>
    private static int Crc16(string s)
    {
        int crc = 0xFFFF;
        foreach (byte b in Encoding.ASCII.GetBytes(s))
        {
            crc ^= b << 8;
            for (int i = 0; i < 8; i++)
            {
                crc = (crc & 0x8000) != 0 ? ((crc << 1) ^ 0x1021) & 0xFFFF : (crc << 1) & 0xFFFF;
            }
        }

        return crc;
    }

    /// <summary>Bỏ dấu tiếng Việt, chỉ giữ chữ cái, chữ số, khoảng trắng, gạch ngang; cắt tối đa <paramref name="toiDa"/> ký tự.</summary>
    private static string BoDau(string s, int toiDa)
    {
        string chuan = s.Replace('đ', 'd').Replace('Đ', 'D').Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (char c in chuan)
        {
            if (c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or (>= '0' and <= '9') or ' ' or '-')
            {
                sb.Append(c);
            }
        }

        return sb.Length > toiDa ? sb.ToString(0, toiDa) : sb.ToString();
    }
}
