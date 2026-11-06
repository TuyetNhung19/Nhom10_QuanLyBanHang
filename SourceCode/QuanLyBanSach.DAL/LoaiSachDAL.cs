// File: QuanLyBanSach.DAL/LoaiSachDAL.cs
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Truy cập bảng LoaiSach.
/// </summary>
public class LoaiSachDAL
{
    /// <summary>Lấy toàn bộ loại sách, sắp theo tên.</summary>
    public List<LoaiSach> LayDanhSach()
    {
        const string sql = "SELECT MaLoai, TenLoai, MoTa FROM dbo.LoaiSach ORDER BY TenLoai";
        return Database.ExecuteReader(sql, DocLoaiSach);
    }

    /// <summary>
    /// Tìm loại sách theo tên hoặc mô tả (không phân biệt hoa thường, dấu).
    /// </summary>
    /// <param name="tuKhoa">Từ khóa; rỗng = lấy tất cả.</param>
    public List<LoaiSach> TimKiem(string? tuKhoa)
    {
        const string sql = @"SELECT MaLoai, TenLoai, MoTa FROM dbo.LoaiSach
                             WHERE @Mau IS NULL
                                OR TenLoai COLLATE Vietnamese_CI_AI LIKE @Mau
                                OR MoTa    COLLATE Vietnamese_CI_AI LIKE @Mau
                             ORDER BY TenLoai";
        return Database.ExecuteReader(sql, DocLoaiSach, CommandType.Text,
            Database.TaoThamSo("@Mau", SqlDbType.NVarChar, TaoMauTimKiem(tuKhoa), 400));
    }

    /// <summary>
    /// Kiểm tra tên loại đã tồn tại chưa.
    /// </summary>
    /// <param name="ten">Tên loại.</param>
    /// <param name="boQuaMaLoai">Mã loại đang sửa; null khi thêm mới.</param>
    public bool KiemTraTrungTen(string ten, int? boQuaMaLoai)
    {
        const string sql = @"SELECT COUNT(*) FROM dbo.LoaiSach
                             WHERE TenLoai = @TenLoai AND (@BoQuaMaLoai IS NULL OR MaLoai <> @BoQuaMaLoai)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@TenLoai", SqlDbType.NVarChar, ten, 100),
            Database.TaoThamSo("@BoQuaMaLoai", SqlDbType.Int, boQuaMaLoai));
        return Convert.ToInt32(kq ?? 0) > 0;
    }

    /// <summary>Đếm số sách (kể cả sách ẩn) thuộc loại – loại còn sách thì không được xóa (SP-04).</summary>
    public int DemSachTheoLoai(int maLoai)
    {
        const string sql = "SELECT COUNT(*) FROM dbo.Sach WHERE MaLoai = @MaLoai";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@MaLoai", SqlDbType.Int, maLoai));
        return Convert.ToInt32(kq ?? 0);
    }

    /// <summary>Thêm loại sách; trả về MaLoai mới.</summary>
    public int Them(LoaiSach loai)
    {
        const string sql = @"INSERT INTO dbo.LoaiSach (TenLoai, MoTa)
                             OUTPUT INSERTED.MaLoai
                             VALUES (@TenLoai, @MoTa)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@TenLoai", SqlDbType.NVarChar, loai.TenLoai, 100),
            Database.TaoThamSo("@MoTa", SqlDbType.NVarChar, loai.MoTa, 255));
        return Convert.ToInt32(kq);
    }

    /// <summary>Sửa tên và mô tả loại sách.</summary>
    public bool Sua(LoaiSach loai)
    {
        const string sql = "UPDATE dbo.LoaiSach SET TenLoai = @TenLoai, MoTa = @MoTa WHERE MaLoai = @MaLoai";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@TenLoai", SqlDbType.NVarChar, loai.TenLoai, 100),
            Database.TaoThamSo("@MoTa", SqlDbType.NVarChar, loai.MoTa, 255),
            Database.TaoThamSo("@MaLoai", SqlDbType.Int, loai.MaLoai)) > 0;
    }

    /// <summary>Xóa loại sách (BLL phải kiểm tra DemSachTheoLoai = 0 trước – SP-04).</summary>
    public bool Xoa(int maLoai)
    {
        const string sql = "DELETE FROM dbo.LoaiSach WHERE MaLoai = @MaLoai";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@MaLoai", SqlDbType.Int, maLoai)) > 0;
    }

    /// <summary>Ánh xạ một dòng thành LoaiSach.</summary>
    private static LoaiSach DocLoaiSach(SqlDataReader r) => new()
    {
        MaLoai = r.GetIntOrZero("MaLoai"),
        TenLoai = r.GetStringOrEmpty("TenLoai"),
        MoTa = r.GetStringOrNull("MoTa")
    };

    /// <summary>
    /// Tạo mẫu LIKE "%từ khóa%" và thoát ký tự đặc biệt [, %, _; trả null nếu từ khóa rỗng.
    /// Chỉ ghép chuỗi vào GIÁ TRỊ tham số, không ghép vào câu SQL.
    /// </summary>
    private static string? TaoMauTimKiem(string? tuKhoa)
    {
        if (string.IsNullOrWhiteSpace(tuKhoa))
        {
            return null;
        }

        string daThoat = tuKhoa.Trim().Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
        return "%" + daThoat + "%";
    }
}
