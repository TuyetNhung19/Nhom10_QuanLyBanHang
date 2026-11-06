// File: QuanLyBanSach.DTO/KhuyenMai.cs
using System.Globalization;

namespace QuanLyBanSach.DTO;

/// <summary>
/// Chương trình khuyến mãi (ĐT3) – ánh xạ bảng KhuyenMai.
/// </summary>
public class KhuyenMai
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    /// <summary>Mã khuyến mãi (IDENTITY).</summary>
    public int MaKM { get; set; }

    /// <summary>Tên chương trình, in trên hóa đơn.</summary>
    public string TenKM { get; set; } = string.Empty;

    /// <summary>Loại khuyến mãi (1 tổng đơn, 2 loại sách, 3 số cuốn).</summary>
    public LoaiKhuyenMai LoaiKM { get; set; } = LoaiKhuyenMai.TheoTongDon;

    /// <summary>Ngưỡng tổng đơn (loại 1).</summary>
    public decimal? NguongTien { get; set; }

    /// <summary>Mã loại sách được giảm (loại 2).</summary>
    public int? MaLoai { get; set; }

    /// <summary>Tên loại sách được giảm (loại 2, hiển thị).</summary>
    public string? TenLoaiSach { get; set; }

    /// <summary>Số cuốn tối thiểu (loại 3).</summary>
    public int? NguongSoLuong { get; set; }

    /// <summary>Phần trăm giảm 1–50 (loại 1, 2 – KM-03).</summary>
    public int? PhanTramGiam { get; set; }

    /// <summary>Số tiền giảm cố định (loại 3).</summary>
    public decimal? SoTienGiam { get; set; }

    /// <summary>Ngày bắt đầu hiệu lực.</summary>
    public DateTime NgayBatDau { get; set; } = DateTime.Today;

    /// <summary>Ngày kết thúc hiệu lực (&gt;= NgayBatDau).</summary>
    public DateTime NgayKetThuc { get; set; } = DateTime.Today;

    /// <summary>true = đang bật, false = đã tắt (KM-04: đã dùng thì chỉ được tắt).</summary>
    public bool TrangThai { get; set; } = true;

    /// <summary>Tên loại khuyến mãi hiển thị.</summary>
    public string TenLoaiKM => LoaiKM switch
    {
        LoaiKhuyenMai.TheoTongDon => "Theo tổng đơn",
        LoaiKhuyenMai.TheoLoaiSach => "Theo loại sách",
        LoaiKhuyenMai.TheoSoLuong => "Theo số cuốn",
        _ => "Không xác định"
    };

    /// <summary>
    /// Mô tả điều kiện dễ đọc, ví dụ "Đơn từ 200.000đ giảm 10%",
    /// "Sách Thiếu nhi giảm 15%", "Mua từ 5 cuốn giảm 30.000đ".
    /// </summary>
    public string MoTaDieuKien => LoaiKM switch
    {
        LoaiKhuyenMai.TheoTongDon =>
            $"Đơn từ {(NguongTien ?? 0).ToString("#,##0", VanHoaVN)}đ giảm {PhanTramGiam ?? 0}%",
        LoaiKhuyenMai.TheoLoaiSach =>
            $"Sách {(string.IsNullOrWhiteSpace(TenLoaiSach) ? "loại #" + MaLoai : TenLoaiSach)} giảm {PhanTramGiam ?? 0}%",
        LoaiKhuyenMai.TheoSoLuong =>
            $"Mua từ {NguongSoLuong ?? 0} cuốn giảm {(SoTienGiam ?? 0).ToString("#,##0", VanHoaVN)}đ",
        _ => string.Empty
    };

    /// <summary>Thời gian hiệu lực dạng "dd/MM/yyyy – dd/MM/yyyy".</summary>
    public string ThoiGianText => $"{NgayBatDau:dd/MM/yyyy} – {NgayKetThuc:dd/MM/yyyy}";

    /// <summary>Trạng thái hiển thị: "Đang bật" / "Đã tắt".</summary>
    public string TrangThaiText => TrangThai ? "Đang bật" : "Đã tắt";

    /// <summary>
    /// Khuyến mãi áp dụng được tại ngày <paramref name="ngay"/> khi đang bật và
    /// ngày nằm trong [NgayBatDau, NgayKetThuc] (KM-02). Chỉ so phần ngày.
    /// </summary>
    /// <param name="ngay">Ngày lập hóa đơn.</param>
    public bool ConHieuLuc(DateTime ngay) =>
        TrangThai && ngay.Date >= NgayBatDau.Date && ngay.Date <= NgayKetThuc.Date;

    /// <summary>Trả về tên khuyến mãi.</summary>
    public override string ToString() => TenKM;
}
