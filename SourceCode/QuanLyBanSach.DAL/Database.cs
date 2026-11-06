// File: QuanLyBanSach.DAL/Database.cs
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Lớp tĩnh dùng chung cho mọi lớp *DAL: đọc chuỗi kết nối, tạo kết nối,
/// thực thi câu lệnh có tham số và tạo SqlParameter.
/// Mọi câu lệnh đều phải dùng SqlParameter, tuyệt đối không nối chuỗi SQL.
/// </summary>
public static class Database
{
    /// <summary>Tên chuỗi kết nối trong App.config (mục connectionStrings).</summary>
    public const string TenChuoiKetNoi = "QuanLyBanSachDB";

    /// <summary>Thời gian chờ (giây) khi kiểm tra kết nối lúc khởi động.</summary>
    private const int ThoiGianChoKiemTra = 10;

    private static string? _connectionString;

    /// <summary>
    /// Chuỗi kết nối đọc từ App.config của project GUI.
    /// Ném <see cref="InvalidOperationException"/> với thông báo rõ ràng nếu thiếu.
    /// </summary>
    public static string ConnectionString
    {
        get
        {
            if (_connectionString != null)
            {
                return _connectionString;
            }

            ConnectionStringSettings? cauHinh = ConfigurationManager.ConnectionStrings[TenChuoiKetNoi];
            if (cauHinh == null || string.IsNullOrWhiteSpace(cauHinh.ConnectionString))
            {
                throw new InvalidOperationException(
                    $"Không tìm thấy chuỗi kết nối \"{TenChuoiKetNoi}\" trong App.config " +
                    "(mục <connectionStrings>). Kiểm tra file App.config của project QuanLyBanSach.GUI.");
            }

            _connectionString = cauHinh.ConnectionString;
            return _connectionString;
        }
    }

    /// <summary>Tạo một SqlConnection mới (chưa mở). Người gọi phải đặt trong using.</summary>
    public static SqlConnection TaoKetNoi() => new SqlConnection(ConnectionString);

    /// <summary>
    /// Thử mở kết nối và chạy SELECT 1 (chờ tối đa 10 giây).
    /// </summary>
    /// <param name="loi">Thông báo lỗi tiếng Việt khi thất bại; chuỗi rỗng khi thành công.</param>
    /// <returns>true nếu kết nối được CSDL.</returns>
    public static bool KiemTraKetNoi(out string loi)
    {
        loi = string.Empty;
        try
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString)
            {
                ConnectTimeout = ThoiGianChoKiemTra
            };

            using var ketNoi = new SqlConnection(builder.ConnectionString);
            ketNoi.Open();
            using var lenh = new SqlCommand("SELECT 1", ketNoi);
            lenh.ExecuteScalar();
            return true;
        }
        catch (SqlException ex)
        {
            loi = MoTaLoiSql(ex);
        }
        catch (ConfigurationErrorsException ex)
        {
            loi = "File cấu hình App.config bị lỗi: " + ex.Message;
        }
        catch (InvalidOperationException ex)
        {
            loi = ex.Message;
        }
        catch (ArgumentException ex)
        {
            loi = "Chuỗi kết nối trong App.config không hợp lệ: " + ex.Message;
        }

        return false;
    }

    /// <summary>
    /// Chuyển SqlException thành thông báo tiếng Việt dễ hiểu cho người dùng.
    /// Lỗi nghiệp vụ do stored procedure THROW (mã 50001–50008) trả nguyên thông báo tiếng Việt.
    /// </summary>
    /// <param name="ex">Lỗi SQL cần mô tả.</param>
    public static string MoTaLoiSql(SqlException ex)
    {
        if (ex.Number >= 50000)
        {
            return ex.Message;
        }

        return ex.Number switch
        {
            -2 => "Hết thời gian chờ SQL Server phản hồi. Vui lòng thử lại.",
            2 or 53 or -1 or 26 or 40 =>
                "Không kết nối được CSDL: không tìm thấy SQL Server. " +
                "Kiểm tra dịch vụ SQL Server đã chạy chưa và mục Server=... trong App.config.",
            4060 =>
                "Không kết nối được CSDL: SQL Server đang chạy nhưng chưa có CSDL QuanLyBanSachDB. " +
                "Hãy chạy Database/01_TaoCSDL.sql và Database/02_NapDuLieuMau.sql trong SSMS.",
            18456 =>
                "Không kết nối được CSDL: đăng nhập SQL Server thất bại. Kiểm tra quyền của tài khoản Windows.",
            547 => "Dữ liệu đang được sử dụng ở nơi khác nên không thể thực hiện thao tác này.",
            2627 or 2601 => "Dữ liệu bị trùng với một bản ghi đã có.",
            _ => $"Lỗi CSDL (mã {ex.Number}): {ex.Message}"
        };
    }

    /// <summary>
    /// Thực thi câu truy vấn và trả về DataTable.
    /// </summary>
    /// <param name="sql">Câu SQL có tham số hoặc tên stored procedure.</param>
    /// <param name="type">CommandType.Text hoặc CommandType.StoredProcedure.</param>
    /// <param name="ps">Danh sách tham số.</param>
    public static DataTable ExecuteQuery(string sql, CommandType type = CommandType.Text, params SqlParameter[] ps)
    {
        using SqlConnection ketNoi = TaoKetNoi();
        using SqlCommand lenh = TaoLenh(ketNoi, sql, type, ps);
        try
        {
            using var adapter = new SqlDataAdapter(lenh);
            var bang = new DataTable();
            adapter.Fill(bang);
            return bang;
        }
        finally
        {
            lenh.Parameters.Clear();
        }
    }

    /// <summary>
    /// Thực thi INSERT/UPDATE/DELETE hoặc stored procedure không trả dữ liệu.
    /// Tham số OUTPUT đọc lại qua thuộc tính Value sau khi gọi.
    /// </summary>
    /// <returns>Số dòng bị ảnh hưởng (-1 nếu SP có SET NOCOUNT ON).</returns>
    public static int ExecuteNonQuery(string sql, CommandType type = CommandType.Text, params SqlParameter[] ps)
    {
        using SqlConnection ketNoi = TaoKetNoi();
        using SqlCommand lenh = TaoLenh(ketNoi, sql, type, ps);
        try
        {
            ketNoi.Open();
            return lenh.ExecuteNonQuery();
        }
        finally
        {
            lenh.Parameters.Clear();
        }
    }

    /// <summary>
    /// Thực thi và trả về giá trị ô đầu tiên; trả null nếu không có dòng hoặc giá trị là NULL.
    /// </summary>
    public static object? ExecuteScalar(string sql, CommandType type = CommandType.Text, params SqlParameter[] ps)
    {
        using SqlConnection ketNoi = TaoKetNoi();
        using SqlCommand lenh = TaoLenh(ketNoi, sql, type, ps);
        try
        {
            ketNoi.Open();
            object? ketQua = lenh.ExecuteScalar();
            return ketQua == null || ketQua == DBNull.Value ? null : ketQua;
        }
        finally
        {
            lenh.Parameters.Clear();
        }
    }

    /// <summary>
    /// Thực thi và ánh xạ từng dòng của result set đầu tiên sang đối tượng <typeparamref name="T"/>.
    /// </summary>
    /// <param name="sql">Câu SQL hoặc tên stored procedure.</param>
    /// <param name="map">Hàm chuyển một dòng reader thành đối tượng.</param>
    /// <param name="type">Kiểu lệnh.</param>
    /// <param name="ps">Danh sách tham số.</param>
    public static List<T> ExecuteReader<T>(string sql, Func<SqlDataReader, T> map,
        CommandType type = CommandType.Text, params SqlParameter[] ps)
    {
        var ketQua = new List<T>();
        using SqlConnection ketNoi = TaoKetNoi();
        using SqlCommand lenh = TaoLenh(ketNoi, sql, type, ps);
        try
        {
            ketNoi.Open();
            using SqlDataReader reader = lenh.ExecuteReader();
            while (reader.Read())
            {
                ketQua.Add(map(reader));
            }
        }
        finally
        {
            lenh.Parameters.Clear();
        }

        return ketQua;
    }

    /// <summary>
    /// Thực thi lệnh trả về NHIỀU result set (sp_LayChiTietHoaDon, sp_BaoCaoCuoiNgay).
    /// Hàm <paramref name="xuLy"/> tự đọc và gọi reader.NextResult() để sang result set kế tiếp.
    /// </summary>
    public static void ExecuteReaderNhieuKetQua(string sql, Action<SqlDataReader> xuLy,
        CommandType type = CommandType.StoredProcedure, params SqlParameter[] ps)
    {
        using SqlConnection ketNoi = TaoKetNoi();
        using SqlCommand lenh = TaoLenh(ketNoi, sql, type, ps);
        try
        {
            ketNoi.Open();
            using SqlDataReader reader = lenh.ExecuteReader();
            xuLy(reader);
        }
        finally
        {
            lenh.Parameters.Clear();
        }
    }

    /// <summary>
    /// Tạo tham số đầu vào; giá trị null tự đổi thành DBNull.Value.
    /// Kiểu Decimal được đặt Precision 18, Scale 0 (tiền VNĐ không có phần lẻ).
    /// </summary>
    /// <param name="ten">Tên tham số, có hoặc không có '@'.</param>
    /// <param name="kieu">Kiểu SQL.</param>
    /// <param name="giaTri">Giá trị (null → DBNull.Value).</param>
    /// <param name="size">Độ dài cho chuỗi (0 = không đặt, -1 = MAX).</param>
    public static SqlParameter TaoThamSo(string ten, SqlDbType kieu, object? giaTri, int size = 0)
    {
        var thamSo = new SqlParameter(ChuanHoaTen(ten), kieu)
        {
            Value = giaTri ?? DBNull.Value
        };

        if (size != 0)
        {
            thamSo.Size = size;
        }

        if (kieu == SqlDbType.Decimal)
        {
            thamSo.Precision = 18;
            thamSo.Scale = 0;
        }

        return thamSo;
    }

    /// <summary>
    /// Tạo tham số OUTPUT (ví dụ @MaHD của sp_LapHoaDon); đọc lại bằng thamSo.Value sau khi thực thi.
    /// </summary>
    public static SqlParameter TaoThamSoOutput(string ten, SqlDbType kieu, int size = 0)
    {
        var thamSo = new SqlParameter(ChuanHoaTen(ten), kieu)
        {
            Direction = ParameterDirection.Output
        };

        if (size != 0)
        {
            thamSo.Size = size;
        }

        if (kieu == SqlDbType.Decimal)
        {
            thamSo.Precision = 18;
            thamSo.Scale = 0;
        }

        return thamSo;
    }

    /// <summary>
    /// Tạo tham số kiểu bảng (table-valued parameter), ví dụ @ChiTiet dbo.ChiTietHoaDonType.
    /// </summary>
    /// <param name="ten">Tên tham số.</param>
    /// <param name="tenKieuBang">Tên kiểu bảng trong CSDL, ví dụ "dbo.ChiTietHoaDonType".</param>
    /// <param name="bang">DataTable có các cột đúng thứ tự với kiểu bảng.</param>
    public static SqlParameter TaoThamSoBang(string ten, string tenKieuBang, DataTable bang)
    {
        return new SqlParameter(ChuanHoaTen(ten), SqlDbType.Structured)
        {
            TypeName = tenKieuBang,
            Value = bang
        };
    }

    /// <summary>Tạo SqlCommand gắn với kết nối và danh sách tham số.</summary>
    private static SqlCommand TaoLenh(SqlConnection ketNoi, string sql, CommandType type, SqlParameter[]? ps)
    {
        var lenh = new SqlCommand(sql, ketNoi)
        {
            CommandType = type,
            CommandTimeout = 30
        };

        if (ps != null && ps.Length > 0)
        {
            lenh.Parameters.AddRange(ps);
        }

        return lenh;
    }

    /// <summary>Thêm '@' vào đầu tên tham số nếu thiếu.</summary>
    private static string ChuanHoaTen(string ten) => ten.StartsWith('@') ? ten : "@" + ten;
}

/// <summary>
/// Các hàm mở rộng đọc cột của SqlDataReader theo TÊN cột một cách an toàn với NULL.
/// Dùng Convert nên đọc được cả TINYINT/SMALLINT/INT và DECIMAL với mọi độ chính xác.
/// (Đặt cùng file Database.cs để giữ đúng cấu trúc thư mục DAL đã chốt.)
/// </summary>
public static class SqlDataReaderExtensions
{
    /// <summary>Đọc chuỗi; NULL → null.</summary>
    public static string? GetStringOrNull(this SqlDataReader reader, string cot)
    {
        int i = reader.GetOrdinal(cot);
        return reader.IsDBNull(i) ? null : Convert.ToString(reader.GetValue(i));
    }

    /// <summary>Đọc chuỗi; NULL → chuỗi rỗng.</summary>
    public static string GetStringOrEmpty(this SqlDataReader reader, string cot)
        => reader.GetStringOrNull(cot) ?? string.Empty;

    /// <summary>Đọc số nguyên (INT, SMALLINT, TINYINT); NULL → null.</summary>
    public static int? GetIntOrNull(this SqlDataReader reader, string cot)
    {
        int i = reader.GetOrdinal(cot);
        return reader.IsDBNull(i) ? null : Convert.ToInt32(reader.GetValue(i));
    }

    /// <summary>Đọc số nguyên; NULL → 0.</summary>
    public static int GetIntOrZero(this SqlDataReader reader, string cot)
        => reader.GetIntOrNull(cot) ?? 0;

    /// <summary>Đọc số thập phân (DECIMAL, MONEY, INT); NULL → null.</summary>
    public static decimal? GetDecimalOrNull(this SqlDataReader reader, string cot)
    {
        int i = reader.GetOrdinal(cot);
        return reader.IsDBNull(i) ? null : Convert.ToDecimal(reader.GetValue(i));
    }

    /// <summary>Đọc số thập phân; NULL → 0.</summary>
    public static decimal GetDecimalOrZero(this SqlDataReader reader, string cot)
        => reader.GetDecimalOrNull(cot) ?? 0m;

    /// <summary>Đọc BIT; NULL → false.</summary>
    public static bool GetBoolOrFalse(this SqlDataReader reader, string cot)
    {
        int i = reader.GetOrdinal(cot);
        return !reader.IsDBNull(i) && Convert.ToBoolean(reader.GetValue(i));
    }

    /// <summary>Đọc DATE/DATETIME; NULL → null.</summary>
    public static DateTime? GetDateTimeOrNull(this SqlDataReader reader, string cot)
    {
        int i = reader.GetOrdinal(cot);
        return reader.IsDBNull(i) ? null : Convert.ToDateTime(reader.GetValue(i));
    }

    /// <summary>Đọc DATE/DATETIME; NULL → DateTime.MinValue.</summary>
    public static DateTime GetDateTimeOrMin(this SqlDataReader reader, string cot)
        => reader.GetDateTimeOrNull(cot) ?? DateTime.MinValue;

    /// <summary>Kiểm tra result set có cột tên <paramref name="cot"/> không (dùng cho hàm map dùng chung).</summary>
    public static bool CoCot(this SqlDataReader reader, string cot)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (string.Equals(reader.GetName(i), cot, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
