// File: QuanLyBanSach.DTO/TrangThaiHoaDon.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Trạng thái hóa đơn, khớp cột HoaDon.TrangThai (TINYINT).
/// </summary>
public enum TrangThaiHoaDon : byte
{
    /// <summary>Hóa đơn đã lưu, được tính doanh thu (TK-03).</summary>
    HoanTat = 1,

    /// <summary>Hóa đơn đã bị Quản lý hủy, tồn kho đã hoàn lại (HD-06).</summary>
    DaHuy = 2
}
