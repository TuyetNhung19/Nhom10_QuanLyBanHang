// File: QuanLyBanSach.GUI/Program.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.GUI.Forms;

namespace QuanLyBanSach.GUI;

/// <summary>
/// Điểm khởi động của BookSPT: cấu hình WinForms, bắt mọi lỗi chưa xử lý,
/// kiểm tra kết nối CSDL rồi chạy vòng lặp Đăng nhập → Form chính → (Đăng xuất) → Đăng nhập.
/// </summary>
internal static class Program
{
    private const string TieuDe = "BookSPT";

    /// <summary>Hàm Main của ứng dụng.</summary>
    [STAThread]
    private static void Main()
    {
        // Bắt lỗi chưa xử lý: phải đăng ký trước khi tạo bất kỳ Form nào
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += Application_ThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        // Font Segoe UI 10, DPI, visual styles: cấu hình trong file .csproj
        ApplicationConfiguration.Initialize();

        // Bảng màu chung cho menu/thanh công cụ/thanh trạng thái (Helpers/GiaoDien.cs)
        Helpers.GiaoDien.KhoiTao();

        // Kiểm tra kết nối CSDL trước khi vào chương trình
        if (!HeThongBLL.KiemTraKetNoi(out string loi))
        {
            MessageBox.Show(
                "Không kết nối được CSDL.\n\n" + loi +
                "\n\nCách khắc phục:\n" +
                "1. Mở Services (services.msc), bật dịch vụ SQL Server.\n" +
                "2. Kiểm tra mục Server=... trong App.config cho đúng tên máy chủ.\n" +
                "3. Đảm bảo đã chạy Database/01_TaoCSDL.sql và 02_NapDuLieuMau.sql.",
                TieuDe + " – Lỗi kết nối",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        // Vòng lặp đăng nhập: Đăng xuất thì quay lại màn hình đăng nhập, Thoát thì kết thúc
        while (true)
        {
            using (var frmDangNhap = new FrmDangNhap())
            {
                if (frmDangNhap.ShowDialog() != DialogResult.OK)
                {
                    break;
                }
            }

            var frmMain = new FrmMain();
            Application.Run(frmMain);

            if (!frmMain.DangXuat)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Lỗi chưa xử lý trên luồng giao diện: hiển thông báo thân thiện, chương trình chạy tiếp.
    /// </summary>
    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        HienLoi(e.Exception, chuongTrinhSeDong: false);
    }

    /// <summary>
    /// Lỗi chưa xử lý trên luồng nền: hiển thông báo trước khi chương trình buộc phải đóng.
    /// </summary>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            HienLoi(ex, e.IsTerminating);
        }
    }

    /// <summary>Hiển MessageBox lỗi; lỗi kết nối CSDL có tiêu đề riêng.</summary>
    private static void HienLoi(Exception ex, bool chuongTrinhSeDong)
    {
        string thongBao = HeThongBLL.LaLoiKetNoi(ex)
            ? "Không kết nối được CSDL.\n\n" + HeThongBLL.MoTaLoi(ex)
            : HeThongBLL.MoTaLoi(ex);

        if (chuongTrinhSeDong)
        {
            thongBao += "\n\nChương trình sẽ đóng. Vui lòng mở lại.";
        }

        MessageBox.Show(
            thongBao,
            TieuDe + " – Lỗi",
            MessageBoxButtons.OK,
            ex is BusinessException ? MessageBoxIcon.Warning : MessageBoxIcon.Error);
    }
}
