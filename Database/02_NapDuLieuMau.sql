USE QuanLyBanSachDB;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;   -- bắt buộc khi ghi vào bảng có filtered index (UX_Sach_ISBN)
GO

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Hom DATE     = CAST(GETDATE() AS DATE);   -- hôm nay
    DECLARE @Now DATETIME = GETDATE();

    /* =================================================================
       0. XÓA DỮ LIỆU CŨ (theo thứ tự khóa ngoại) VÀ RESEED IDENTITY
       ================================================================= */
    DELETE FROM dbo.ChiTietHoaDon;
    DELETE FROM dbo.HoaDon;
    DELETE FROM dbo.KhuyenMai;
    DELETE FROM dbo.Sach;
    DELETE FROM dbo.LoaiSach;
    DELETE FROM dbo.KhachHang;
    DELETE FROM dbo.TaiKhoan;

    -- Bảng chưa từng có dòng: RESEED 1 thì dòng đầu = 1.
    -- Bảng đã từng có dòng:   RESEED 0 thì dòng kế tiếp = 1.
    IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = OBJECT_ID(N'dbo.TaiKhoan') AND last_value IS NOT NULL)
         DBCC CHECKIDENT (N'dbo.TaiKhoan', RESEED, 0) WITH NO_INFOMSGS;
    ELSE DBCC CHECKIDENT (N'dbo.TaiKhoan', RESEED, 1) WITH NO_INFOMSGS;

    IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = OBJECT_ID(N'dbo.LoaiSach') AND last_value IS NOT NULL)
         DBCC CHECKIDENT (N'dbo.LoaiSach', RESEED, 0) WITH NO_INFOMSGS;
    ELSE DBCC CHECKIDENT (N'dbo.LoaiSach', RESEED, 1) WITH NO_INFOMSGS;

    IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = OBJECT_ID(N'dbo.KhachHang') AND last_value IS NOT NULL)
         DBCC CHECKIDENT (N'dbo.KhachHang', RESEED, 0) WITH NO_INFOMSGS;
    ELSE DBCC CHECKIDENT (N'dbo.KhachHang', RESEED, 1) WITH NO_INFOMSGS;

    IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = OBJECT_ID(N'dbo.KhuyenMai') AND last_value IS NOT NULL)
         DBCC CHECKIDENT (N'dbo.KhuyenMai', RESEED, 0) WITH NO_INFOMSGS;
    ELSE DBCC CHECKIDENT (N'dbo.KhuyenMai', RESEED, 1) WITH NO_INFOMSGS;

    /* =================================================================
       1. TÀI KHOẢN
       Mật khẩu băm SHA-256 hex in hoa (TK-02). Dùng chuỗi VARCHAR (không phải N'...')
       để byte giống Encoding.UTF8 bên C# → hai bên ra cùng một chuỗi băm.
       ================================================================= */
    INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai)
    VALUES
        ('admin',    CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'admin123'),   2), N'Võ Ngọc Tuyết Nhung',    3, 1),
        ('quanly',   CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'quanly123'),  2), N'Huỳnh Thị Hồng Ân', 2, 1),
        ('thungan1', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'thungan123'), 2), N'Lê Thị Kim Loan',       1, 1),
        ('thungan2', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'thungan123'), 2), N'Võ Nhật Song Hân',  1, 1),
		('thungan3', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'thungan123'), 2), N'Nguyễn Văn A',  1, 0);
    /* =================================================================
       2. LOẠI SÁCH (8 loại có sách + "Truyện tranh" chưa có sách để demo xóa)
       ================================================================= */
    INSERT INTO dbo.LoaiSach (TenLoai, MoTa)
    VALUES
        (N'Văn học',      N'Tiểu thuyết, truyện ngắn, văn học Việt Nam và nước ngoài'),
        (N'Kỹ năng sống', N'Phát triển bản thân, giao tiếp, tư duy tích cực'),
        (N'Thiếu nhi',    N'Sách dành cho thiếu nhi và tuổi mới lớn'),
        (N'Kinh tế',      N'Kinh doanh, quản trị, tài chính cá nhân'),
        (N'Khoa học',     N'Khoa học phổ thông, vũ trụ, tự nhiên'),
        (N'Ngoại ngữ',    N'Giáo trình và sách luyện thi ngoại ngữ'),
        (N'Tin học',      N'Lập trình, công nghệ thông tin'),
        (N'Lịch sử',      N'Lịch sử Việt Nam và thế giới'),
        (N'Truyện tranh', N'Truyện tranh, manga (chưa có sách – dùng để demo xóa loại)');

    /* =================================================================
       3. SÁCH (40 cuốn). ISBN MẪU = '893604000' + 4 số cuối của mã sách.
       Tồn kho đặc biệt:
         Sắp hết (tồn <= TonToiThieu 5): S0010 (4), S0012 (3), S0016 (5), S0023 (2), S0028 (3)
         Tồn 1 : S0011 Chí Phèo (TC08, TC21)       Tồn 0 : S0025 Bí mật tư duy triệu phú
         Ẩn    : S0039, S0040 (đã từng bán → chỉ được ẩn, SP-03)
         Không ISBN: S0029, S0030, S0033
       ================================================================= */
    INSERT INTO dbo.Sach (MaSach, ISBN, TenSach, TacGia, NhaXuatBan, MaLoai,
                          GiaBan, SoLuongTon, TonToiThieu, AnhBia, TrangThai)
    SELECT v.MaSach, v.ISBN, v.TenSach, v.TacGia, v.NhaXuatBan, l.MaLoai,
           v.GiaBan, v.SoLuongTon, 5, NULL, v.TrangThai
    FROM (VALUES
        ('S0001', '8936040000001', N'Nhà giả kim',                     N'Paulo Coelho',               N'NXB Hội Nhà Văn',          N'Văn học',       79000, 45, 1),
        ('S0002', '8936040000002', N'Đắc nhân tâm',                    N'Dale Carnegie',              N'NXB Tổng hợp TP.HCM',      N'Kỹ năng sống',  86000, 60, 1),
        ('S0003', '8936040000003', N'Tuổi trẻ đáng giá bao nhiêu',     N'Rosie Nguyễn',               N'NXB Hội Nhà Văn',          N'Kỹ năng sống',  90000, 40, 1),
        ('S0004', '8936040000004', N'Dế Mèn phiêu lưu ký',             N'Tô Hoài',                    N'NXB Kim Đồng',             N'Thiếu nhi',     45000, 50, 1),
        ('S0005', '8936040000005', N'Kính vạn hoa - Tập 1',            N'Nguyễn Nhật Ánh',            N'NXB Kim Đồng',             N'Thiếu nhi',     55000, 38, 1),
        ('S0006', '8936040000006', N'Mắt biếc',                        N'Nguyễn Nhật Ánh',            N'NXB Trẻ',                  N'Văn học',      110000, 30, 1),
        ('S0007', '8936040000007', N'Tôi thấy hoa vàng trên cỏ xanh',  N'Nguyễn Nhật Ánh',            N'NXB Trẻ',                  N'Văn học',      125000, 26, 1),
        ('S0008', '8936040000008', N'Cho tôi xin một vé đi tuổi thơ',  N'Nguyễn Nhật Ánh',            N'NXB Trẻ',                  N'Văn học',       72000, 33, 1),
        ('S0009', '8936040000009', N'Số đỏ',                           N'Vũ Trọng Phụng',             N'NXB Văn Học',              N'Văn học',       55000, 20, 1),
        ('S0010', '8936040000010', N'Rừng Na Uy',                      N'Haruki Murakami',            N'NXB Hội Nhà Văn',          N'Văn học',      150000,  4, 1),
        ('S0011', '8936040000011', N'Chí Phèo',                        N'Nam Cao',                    N'NXB Văn Học',              N'Văn học',       40000,  1, 1),
        ('S0012', '8936040000012', N'Cây cam ngọt của tôi',            N'José Mauro de Vasconcelos',  N'NXB Hội Nhà Văn',          N'Văn học',      108000,  3, 1),
        ('S0013', '8936040000013', N'Nhà lãnh đạo không chức danh',    N'Robin Sharma',               N'NXB Trẻ',                  N'Kỹ năng sống',  98000, 25, 1),
        ('S0014', '8936040000014', N'Đời ngắn đừng ngủ dài',           N'Robin Sharma',               N'NXB Trẻ',                  N'Kỹ năng sống',  75000, 22, 1),
        ('S0015', '8936040000015', N'Quẳng gánh lo đi và vui sống',    N'Dale Carnegie',              N'NXB Tổng hợp TP.HCM',      N'Kỹ năng sống',  88000, 18, 1),
        ('S0016', '8936040000016', N'7 thói quen hiệu quả',            N'Stephen R. Covey',           N'NXB Tổng hợp TP.HCM',      N'Kỹ năng sống', 145000,  5, 1),
        ('S0017', '8936040000017', N'Hoàng tử bé',                     N'Antoine de Saint-Exupéry',   N'NXB Hội Nhà Văn',          N'Thiếu nhi',     68000, 30, 1),
        ('S0018', '8936040000018', N'Totto-chan bên cửa sổ',           N'Kuroyanagi Tetsuko',         N'NXB Hội Nhà Văn',          N'Thiếu nhi',     98000, 24, 1),
        ('S0019', '8936040000019', N'Những tấm lòng cao cả',           N'Edmondo De Amicis',          N'NXB Văn Học',              N'Thiếu nhi',     80000, 15, 1),
        ('S0020', '8936040000020', N'Không gia đình',                  N'Hector Malot',               N'NXB Kim Đồng',             N'Thiếu nhi',    120000, 12, 1),
        ('S0021', '8936040000021', N'Chuyện con mèo dạy hải âu bay',   N'Luis Sepúlveda',             N'NXB Hội Nhà Văn',          N'Thiếu nhi',     49000, 27, 1),
        ('S0022', '8936040000022', N'Cha giàu cha nghèo',              N'Robert T. Kiyosaki',         N'NXB Trẻ',                  N'Kinh tế',      105000, 32, 1),
        ('S0023', '8936040000023', N'Nghĩ giàu và làm giàu',           N'Napoleon Hill',              N'NXB Tổng hợp TP.HCM',      N'Kinh tế',      110000,  2, 1),
        ('S0024', '8936040000024', N'Từ tốt đến vĩ đại',               N'Jim Collins',                N'NXB Trẻ',                  N'Kinh tế',      135000, 16, 1),
        ('S0025', '8936040000025', N'Bí mật tư duy triệu phú',         N'T. Harv Eker',               N'NXB Tổng hợp TP.HCM',      N'Kinh tế',      108000,  0, 1),
        ('S0026', '8936040000026', N'Lược sử thời gian',               N'Stephen Hawking',            N'NXB Trẻ',                  N'Khoa học',     115000, 14, 1),
        ('S0027', '8936040000027', N'Vũ trụ trong vỏ hạt dẻ',          N'Stephen Hawking',            N'NXB Trẻ',                  N'Khoa học',     165000, 10, 1),
        ('S0028', '8936040000028', N'Súng, vi trùng và thép',          N'Jared Diamond',              N'NXB Tri Thức',             N'Khoa học',     220000,  3, 1),
        ('S0029', NULL,            N'English Grammar in Use',          N'Raymond Murphy',             N'Cambridge University Press', N'Ngoại ngữ',  185000, 20, 1),
        ('S0030', NULL,            N'Destination B1',                  N'Malcolm Mann, Steve Taylore-Knowles', N'Macmillan',      N'Ngoại ngữ',    160000, 15, 1),
        ('S0031', '8936040000031', N'600 Essential Words for the TOEIC', N'Lin Lougheed',             N'Barron''s',                N'Ngoại ngữ',    150000, 18, 1),
        ('S0032', '8936040000032', N'Clean Code',                      N'Robert C. Martin',           N'Prentice Hall',            N'Tin học',      350000,  8, 1),
        ('S0033', NULL,            N'Head First C#',                   N'Andrew Stellman, Jennifer Greene', N'O''Reilly',         N'Tin học',      320000,  6, 1),
        ('S0034', '8936040000034', N'The Pragmatic Programmer',        N'Andrew Hunt, David Thomas',  N'Addison-Wesley',           N'Tin học',      340000,  7, 1),
        ('S0035', '8936040000035', N'Sapiens: Lược sử loài người',     N'Yuval Noah Harari',          N'NXB Tri Thức',             N'Lịch sử',      259000, 20, 1),
        ('S0036', '8936040000036', N'Việt Nam sử lược',                N'Trần Trọng Kim',             N'NXB Văn Học',              N'Lịch sử',      165000, 15, 1),
        ('S0037', '8936040000037', N'Đại Việt sử ký toàn thư',         N'Ngô Sĩ Liên',                N'NXB Văn Học',              N'Lịch sử',      350000,  6, 1),
        ('S0038', '8936040000038', N'Ngàn năm áo mũ',                  N'Trần Quang Đức',             N'NXB Thế Giới',             N'Lịch sử',      250000,  9, 1),
        ('S0039', '8936040000039', N'Tuổi thơ dữ dội',                 N'Phùng Quán',                 N'NXB Kim Đồng',             N'Văn học',      150000, 10, 0),
        ('S0040', '8936040000040', N'Bố già',                          N'Mario Puzo',                 N'NXB Văn Học',              N'Văn học',      120000,  8, 0)
    ) AS v (MaSach, ISBN, TenSach, TacGia, NhaXuatBan, TenLoai, GiaBan, SoLuongTon, TrangThai)
    INNER JOIN dbo.LoaiSach AS l ON l.TenLoai = v.TenLoai;

    IF (SELECT COUNT(*) FROM dbo.Sach) <> 40
        THROW 51001, N'Dữ liệu mẫu: số sách khác 40 (sai tên loại sách trong VALUES?).', 1;

    /* =================================================================
       4. KHÁCH HÀNG (15). KH14, KH15 chưa mua lần nào → demo xóa được.
       ================================================================= */
    INSERT INTO dbo.KhachHang (HoTen, SDT, Email, DiaChi)
    VALUES
        (N'Trần Thị Bích Ngọc', '0901234567', 'bichngoc.tran@example.com', N'12 Nguyễn Đình Chiểu, Quận 3, TP.HCM'),
        (N'Lê Văn Hùng',        '0912345678', NULL,                         NULL),
        (N'Phạm Minh Châu',     '0938123456', 'minhchau.pham@example.com',  N'45 Lê Văn Sỹ, Quận Phú Nhuận, TP.HCM'),
        (N'Nguyễn Hoàng Long',  '0977456123', 'hoanglong.dev@example.com',  NULL),
        (N'Võ Thị Thu Trang',   '0365789012', 'thutrang.vo@example.com',    N'88 Cách Mạng Tháng Tám, Quận 10, TP.HCM'),
        (N'Đặng Quốc Bảo',      '0398234567', NULL,                         NULL),
        (N'Bùi Thanh Tâm',      '0703456789', NULL,                         N'21 An Dương Vương, Quận 5, TP.HCM'),
        (N'Huỳnh Gia Hân',      '0789123456', 'giahan.huynh@example.com',   NULL),
        (N'Phan Anh Tuấn',      '0332678901', NULL,                         NULL),
        (N'Đỗ Ngọc Ánh',        '0963555777', NULL,                         NULL),
        (N'Hồ Minh Khang',      '0708999111', NULL,                         NULL),
        (N'Ngô Thị Mỹ Linh',    '0945112233', 'mylinh.ngo@example.com',     NULL),
        (N'Trương Đức Thắng',   '0356778899', NULL,                         NULL),
        (N'Lý Thị Kim Yến',     '0918246813', NULL,                         NULL),   -- chưa mua
        (N'Mai Văn Phúc',       '0767135792', NULL,                         NULL);   -- chưa mua

    /* =================================================================
       5. KHUYẾN MÃI (5)
       ================================================================= */
    DECLARE @MaLoaiThieuNhi INT = (SELECT MaLoai FROM dbo.LoaiSach WHERE TenLoai = N'Thiếu nhi');

    INSERT INTO dbo.KhuyenMai (TenKM, LoaiKM, NguongTien, MaLoai, NguongSoLuong, PhanTramGiam, SoTienGiam,
                               NgayBatDau, NgayKetThuc, TrangThai)
    VALUES
        -- KM1: đang hiệu lực
        (N'Giảm 10% đơn từ 200.000đ',   1, 200000, NULL,            NULL, 10, NULL,
            DATEADD(DAY, -30, @Hom), DATEADD(DAY, 180, @Hom), 1),
        -- KM2: đang hiệu lực
        (N'Sách Thiếu nhi giảm 15%',    2, NULL,   @MaLoaiThieuNhi, NULL, 15, NULL,
            DATEADD(DAY, -30, @Hom), DATEADD(DAY, 180, @Hom), 1),
        -- KM3: đang hiệu lực
        (N'Mua từ 5 cuốn giảm 30.000đ', 3, NULL,   NULL,            5,    NULL, 30000,
            DATEADD(DAY, -30, @Hom), DATEADD(DAY, 180, @Hom), 1),
        -- KM4: ĐÃ HẾT HẠN (kết thúc hôm qua − 10 ngày) – TC19
        (N'Giảm 5% đơn từ 100.000đ',    1, 100000, NULL,            NULL, 5,  NULL,
            DATEADD(DAY, -60, @Hom), DATEADD(DAY, -11, @Hom), 1),
        -- KM5: còn hạn nhưng ĐANG TẮT – TC19
        (N'Giảm 20% đơn từ 300.000đ',   1, 300000, NULL,            NULL, 20, NULL,
            DATEADD(DAY, -30, @Hom), DATEADD(DAY, 180, @Hom), 0);

    DECLARE @MaKM1 INT = (SELECT MaKM FROM dbo.KhuyenMai WHERE TenKM = N'Giảm 10% đơn từ 200.000đ');

    /* =================================================================
       6. HÓA ĐƠN LỊCH SỬ (60 hóa đơn trong 14 ngày gần nhất)
       NgayLui = số ngày lùi so với hôm nay (0 = hôm nay).
       Với NgayLui = 0, Gio/Phut bị bỏ qua: giờ lập được rải đều trong khoảng
       [08:00 hoặc 00:00, GETDATE()) để luôn sớm hơn thời điểm chạy script.
       Khách: SĐT (NULL = khách lẻ). Cặp mua kèm cố ý lặp:
         (S0001, S0002) ×6   (S0004, S0005) ×5   (S0003, S0002) ×4
       ================================================================= */
    DECLARE @HD TABLE
    (
        STT         INT           NOT NULL PRIMARY KEY,
        NgayLui     INT           NOT NULL,
        Gio         TINYINT       NOT NULL,
        Phut        TINYINT       NOT NULL,
        TenDangNhap VARCHAR(50)   NOT NULL,
        SDTKhach    VARCHAR(15)   NULL,
        TrangThai   TINYINT       NOT NULL,
        LyDoHuy     NVARCHAR(255) NULL,
        NgayLap     DATETIME      NULL,
        MaHD        VARCHAR(20)   NULL
    );

    DECLARE @CT TABLE
    (
        STT     INT         NOT NULL,
        MaSach  VARCHAR(20) NOT NULL,
        SoLuong INT         NOT NULL,
        PRIMARY KEY (STT, MaSach)
    );

    INSERT INTO @HD (STT, NgayLui, Gio, Phut, TenDangNhap, SDTKhach, TrangThai, LyDoHuy)
    VALUES
        -- 13 ngày trước
        ( 1, 13,  9, 15, 'thungan1', NULL,         1, NULL),
        ( 2, 13, 11, 40, 'thungan2', '0901234567', 1, NULL),
        ( 3, 13, 15,  5, 'thungan1', '0938123456', 1, NULL),
        ( 4, 13, 19, 30, 'quanly',   NULL,         1, NULL),
        -- 12 ngày trước
        ( 5, 12,  8, 45, 'thungan2', '0912345678', 1, NULL),
        ( 6, 12, 10, 20, 'thungan1', NULL,         1, NULL),
        ( 7, 12, 14, 10, 'thungan2', '0398234567', 1, NULL),
        ( 8, 12, 20,  5, 'thungan1', NULL,         1, NULL),
        -- 11 ngày trước
        ( 9, 11,  9, 30, 'quanly',   '0977456123', 1, NULL),
        (10, 11, 12, 15, 'thungan1', NULL,         1, NULL),
        (11, 11, 16, 40, 'thungan2', '0365789012', 1, NULL),
        (12, 11, 18, 55, 'thungan1', NULL,         1, NULL),
        -- 10 ngày trước
        (13, 10,  9,  5, 'thungan2', '0938123456', 1, NULL),
        (14, 10, 11, 50, 'thungan1', '0703456789', 1, NULL),
        (15, 10, 15, 35, 'thungan2', NULL,         1, NULL),
        (16, 10, 20, 40, 'quanly',   NULL,         1, NULL),
        -- 9 ngày trước
        (17,  9,  8, 30, 'thungan1', '0901234567', 1, NULL),
        (18,  9, 10, 45, 'thungan2', NULL,         1, NULL),
        (19,  9, 14, 25, 'thungan1', '0789123456', 1, NULL),
        (20,  9, 19, 10, 'thungan2', NULL,         1, NULL),
        -- 8 ngày trước
        (21,  8,  9, 20, 'thungan1', '0912345678', 1, NULL),
        (22,  8, 13,  5, 'thungan2', NULL,         1, NULL),
        (23,  8, 16, 30, 'quanly',   '0332678901', 1, NULL),
        (24,  8, 20, 50, 'thungan1', NULL,         2, N'Khách đổi ý, trả lại sách ngay tại quầy'),
        -- 7 ngày trước
        (25,  7,  9, 10, 'thungan2', '0365789012', 1, NULL),
        (26,  7, 11, 35, 'thungan1', NULL,         1, NULL),
        (27,  7, 15, 20, 'thungan2', '0963555777', 1, NULL),
        (28,  7, 18, 45, 'thungan1', NULL,         1, NULL),
        -- 6 ngày trước
        (29,  6,  8, 50, 'thungan1', '0901234567', 1, NULL),
        (30,  6, 12, 30, 'quanly',   '0977456123', 1, NULL),
        (31,  6, 16, 15, 'thungan2', NULL,         1, NULL),
        (32,  6, 19, 40, 'thungan1', '0708999111', 1, NULL),
        -- 5 ngày trước
        (33,  5,  9, 25, 'thungan2', '0938123456', 1, NULL),
        (34,  5, 11, 10, 'thungan1', NULL,         1, NULL),
        (35,  5, 14, 55, 'thungan2', '0945112233', 1, NULL),
        (36,  5, 20, 20, 'thungan1', NULL,         1, NULL),
        -- 4 ngày trước
        (37,  4,  9, 40, 'thungan1', '0912345678', 1, NULL),
        (38,  4, 13, 20, 'thungan2', NULL,         1, NULL),
        (39,  4, 17,  5, 'quanly',   '0398234567', 1, NULL),
        (40,  4, 20, 35, 'thungan2', NULL,         1, NULL),
        -- 3 ngày trước
        (41,  3,  8, 40, 'thungan2', '0365789012', 1, NULL),
        (42,  3, 10, 55, 'thungan1', NULL,         2, N'Thu ngân quét nhầm sách, lập lại hóa đơn khác'),
        (43,  3, 15, 30, 'thungan1', '0356778899', 1, NULL),
        (44,  3, 19, 15, 'thungan2', NULL,         1, NULL),
        -- 2 ngày trước
        (45,  2,  9,  0, 'thungan1', '0901234567', 1, NULL),
        (46,  2, 11, 25, 'thungan2', NULL,         1, NULL),
        (47,  2, 14, 40, 'quanly',   '0789123456', 1, NULL),
        (48,  2, 17, 50, 'thungan1', NULL,         1, NULL),
        (49,  2, 20, 55, 'thungan2', '0703456789', 1, NULL),
        -- hôm qua
        (50,  1,  8, 35, 'thungan2', '0938123456', 1, NULL),
        (51,  1, 10, 50, 'thungan1', NULL,         1, NULL),
        (52,  1, 13, 35, 'thungan2', '0332678901', 1, NULL),
        (53,  1, 16, 25, 'thungan1', '0963555777', 1, NULL),
        (54,  1, 20, 10, 'quanly',   NULL,         1, NULL),
        -- HÔM NAY (Gio/Phut bỏ qua)
        (55,  0,  0,  0, 'thungan1', '0901234567', 1, NULL),
        (56,  0,  0,  0, 'thungan2', NULL,         1, NULL),
        (57,  0,  0,  0, 'quanly',   '0977456123', 1, NULL),
        (58,  0,  0,  0, 'thungan1', NULL,         2, N'Khách không mang đủ tiền, hủy theo yêu cầu của khách'),
        (59,  0,  0,  0, 'thungan2', '0708999111', 1, NULL),
        (60,  0,  0,  0, 'thungan1', '0945112233', 1, NULL);

    INSERT INTO @CT (STT, MaSach, SoLuong)
    VALUES
        ( 1, 'S0001', 1), ( 1, 'S0002', 1),
        ( 2, 'S0006', 1), ( 2, 'S0007', 1),
        ( 3, 'S0004', 1), ( 3, 'S0005', 1),
        ( 4, 'S0026', 1),
        ( 5, 'S0022', 1), ( 5, 'S0024', 1),
        ( 6, 'S0003', 1), ( 6, 'S0002', 1),
        ( 7, 'S0017', 2),
        ( 8, 'S0009', 1),
        ( 9, 'S0032', 1),
        (10, 'S0001', 1), (10, 'S0002', 1),
        (11, 'S0013', 1), (11, 'S0014', 1),
        (12, 'S0021', 2),
        (13, 'S0004', 2), (13, 'S0005', 1),
        (14, 'S0035', 1),
        (15, 'S0029', 1),
        (16, 'S0001', 1),
        (17, 'S0008', 1), (17, 'S0005', 1),
        (18, 'S0003', 1), (18, 'S0002', 1),
        (19, 'S0018', 1), (19, 'S0016', 1),
        (20, 'S0039', 1),
        (21, 'S0023', 1),
        (22, 'S0001', 1), (22, 'S0002', 2),
        (23, 'S0027', 1), (23, 'S0026', 1),
        (24, 'S0040', 1),
        (25, 'S0015', 1), (25, 'S0002', 1),
        (26, 'S0031', 1),
        (27, 'S0020', 1), (27, 'S0019', 1),
        (28, 'S0004', 1), (28, 'S0005', 1),
        (29, 'S0012', 1), (29, 'S0001', 1),
        (30, 'S0034', 1), (30, 'S0033', 1),
        (31, 'S0014', 1),
        (32, 'S0036', 1),
        (33, 'S0017', 1), (33, 'S0021', 1), (33, 'S0004', 1),
        (34, 'S0003', 1), (34, 'S0002', 1),
        (35, 'S0006', 1),
        (36, 'S0009', 2),
        (37, 'S0022', 1), (37, 'S0025', 1),
        (38, 'S0001', 1), (38, 'S0002', 1),
        (39, 'S0035', 1),
        (40, 'S0030', 1),
        (41, 'S0013', 1), (41, 'S0003', 1),
        (42, 'S0010', 1),
        (43, 'S0004', 1), (43, 'S0005', 2),
        (44, 'S0028', 1),
        (45, 'S0007', 1), (45, 'S0008', 1), (45, 'S0001', 1),
        (46, 'S0003', 1), (46, 'S0002', 1),
        (47, 'S0019', 1),
        (48, 'S0016', 1),
        (49, 'S0037', 1),
        (50, 'S0004', 1), (50, 'S0005', 1), (50, 'S0018', 1),
        (51, 'S0001', 1), (51, 'S0002', 1),
        (52, 'S0024', 1),
        (53, 'S0020', 1),
        (54, 'S0032', 1), (54, 'S0034', 1),
        (55, 'S0006', 1), (55, 'S0001', 1),
        (56, 'S0001', 1), (56, 'S0002', 2),
        (57, 'S0033', 1),
        (58, 'S0014', 1),
        (59, 'S0026', 1), (59, 'S0027', 1),
        (60, 'S0021', 1), (60, 'S0017', 1);

    -- Kiểm tra dữ liệu khai báo trước khi ghi (tránh JOIN âm thầm bỏ sót dòng)
    IF EXISTS (SELECT 1 FROM @HD AS h
               WHERE NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan AS tk WHERE tk.TenDangNhap = h.TenDangNhap))
        THROW 51002, N'Dữ liệu mẫu: có hóa đơn dùng tên đăng nhập không tồn tại.', 1;

    IF EXISTS (SELECT 1 FROM @HD AS h
               WHERE h.SDTKhach IS NOT NULL
                 AND NOT EXISTS (SELECT 1 FROM dbo.KhachHang AS kh WHERE kh.SDT = h.SDTKhach))
        THROW 51003, N'Dữ liệu mẫu: có hóa đơn dùng SĐT khách không tồn tại.', 1;

    IF EXISTS (SELECT 1 FROM @CT AS c
               WHERE NOT EXISTS (SELECT 1 FROM dbo.Sach AS s WHERE s.MaSach = c.MaSach)
                  OR NOT EXISTS (SELECT 1 FROM @HD AS h WHERE h.STT = c.STT))
        THROW 51004, N'Dữ liệu mẫu: chi tiết tham chiếu sách hoặc hóa đơn không tồn tại.', 1;

    IF EXISTS (SELECT 1 FROM @HD AS h WHERE NOT EXISTS (SELECT 1 FROM @CT AS c WHERE c.STT = h.STT))
        THROW 51005, N'Dữ liệu mẫu: có hóa đơn không có dòng chi tiết (vi phạm HD-01).', 1;

    -- 6a. Giờ lập các ngày trước: ngày lùi + Gio:Phut
    UPDATE @HD
    SET NgayLap = DATEADD(MINUTE, Gio * 60 + Phut, CAST(DATEADD(DAY, -NgayLui, @Hom) AS DATETIME))
    WHERE NgayLui > 0;

    -- 6b. Giờ lập hôm nay: rải đều trong [BatDau, @Now), luôn sớm hơn GETDATE()
    DECLARE @BatDauHomNay DATETIME =
        CASE WHEN @Now >= DATEADD(HOUR, 9, CAST(@Hom AS DATETIME))
             THEN DATEADD(HOUR, 8, CAST(@Hom AS DATETIME))
             ELSE CAST(@Hom AS DATETIME)
        END;
    DECLARE @SoGiayHomNay BIGINT = DATEDIFF(SECOND, @BatDauHomNay, @Now);
    DECLARE @SoHDHomNay   INT    = (SELECT COUNT(*) FROM @HD WHERE NgayLui = 0);

    WITH HomNay AS
    (
        SELECT NgayLap, ROW_NUMBER() OVER (ORDER BY STT) AS ThuTu
        FROM @HD
        WHERE NgayLui = 0
    )
    UPDATE HomNay
    SET NgayLap = DATEADD(SECOND, CAST(@SoGiayHomNay * ThuTu / (@SoHDHomNay + 1) AS INT), @BatDauHomNay);

    -- 6c. Mã hóa đơn: HD + yyyyMMdd + số thứ tự trong ngày theo giờ lập (HD-07)
    WITH DanhSo AS
    (
        SELECT MaHD, NgayLap,
               ROW_NUMBER() OVER (PARTITION BY NgayLui ORDER BY NgayLap, STT) AS SoThuTu
        FROM @HD
    )
    UPDATE DanhSo
    SET MaHD = 'HD' + FORMAT(NgayLap, 'yyyyMMdd') + RIGHT('000' + CAST(SoThuTu AS VARCHAR(3)), 3);

    -- 6d. Ghi HoaDon với các cột tiền = 0 (vẫn thỏa mọi CHECK)
    INSERT INTO dbo.HoaDon (MaHD, NgayLap, MaTK, MaKH, TamTinh, GiamGia, MaKM,
                            TongThanhToan, TienKhachDua, TienThoi, TrangThai, LyDoHuy, GhiChu)
    SELECT h.MaHD, h.NgayLap, tk.MaTK, kh.MaKH, 0, 0, NULL,
           0, 0, 0, h.TrangThai, h.LyDoHuy, NULL
    FROM @HD AS h
    INNER JOIN dbo.TaiKhoan  AS tk ON tk.TenDangNhap = h.TenDangNhap
    LEFT  JOIN dbo.KhachHang AS kh ON kh.SDT = h.SDTKhach;

    -- 6e. Ghi chi tiết: DonGia = giá bán hiện tại của sách (HD-04)
    INSERT INTO dbo.ChiTietHoaDon (MaHD, MaSach, SoLuong, DonGia, ThanhTien)
    SELECT h.MaHD, c.MaSach, c.SoLuong, s.GiaBan, c.SoLuong * s.GiaBan
    FROM @CT AS c
    INNER JOIN @HD     AS h ON h.STT = c.STT
    INNER JOIN dbo.Sach AS s ON s.MaSach = c.MaSach;

    -- 6f. Tính tiền từ chi tiết. Lịch sử chỉ dùng KM1: TamTinh >= 200.000 → giảm 10%.
    --     Tiền khách đưa = làm tròn LÊN bội số 50.000 của tổng thanh toán.
    WITH TongCT AS
    (
        SELECT MaHD, SUM(ThanhTien) AS TamTinh
        FROM dbo.ChiTietHoaDon
        GROUP BY MaHD
    ),
    TinhGiam AS
    (
        SELECT t.MaHD,
               CAST(t.TamTinh AS DECIMAL(14,0)) AS TamTinh,
               CAST(CASE WHEN t.TamTinh >= 200000
                         THEN ROUND(CAST(t.TamTinh AS DECIMAL(14,0)) * 0.10, 0)
                         ELSE 0 END
                    AS DECIMAL(14,0)) AS GiamGia
        FROM TongCT AS t
    ),
    TinhTong AS
    (
        SELECT g.MaHD, g.TamTinh, g.GiamGia,
               g.TamTinh - g.GiamGia AS TongThanhToan,
               CAST(CEILING((g.TamTinh - g.GiamGia) / 50000.0) * 50000 AS DECIMAL(14,0)) AS TienKhachDua
        FROM TinhGiam AS g
    )
    UPDATE hd
    SET hd.TamTinh       = x.TamTinh,
        hd.GiamGia       = x.GiamGia,
        hd.MaKM          = CASE WHEN x.GiamGia > 0 THEN @MaKM1 ELSE NULL END,
        hd.TongThanhToan = x.TongThanhToan,
        hd.TienKhachDua  = x.TienKhachDua,
        hd.TienThoi      = x.TienKhachDua - x.TongThanhToan
    FROM dbo.HoaDon AS hd
    INNER JOIN TinhTong AS x ON x.MaHD = hd.MaHD;

    -- 6g. Ghi chú cho hóa đơn đã hủy (người hủy là Quản lý)
    DECLARE @TenQuanLy NVARCHAR(100) = (SELECT HoTen FROM dbo.TaiKhoan WHERE TenDangNhap = 'quanly');

    UPDATE dbo.HoaDon
    SET GhiChu = N'Hủy bởi ' + @TenQuanLy + N' lúc '
               + FORMAT(CASE WHEN DATEADD(MINUTE, 15, NgayLap) > @Now THEN @Now
                             ELSE DATEADD(MINUTE, 15, NgayLap) END, 'HH:mm dd/MM/yyyy')
    WHERE TrangThai = 2;

    IF (SELECT COUNT(*) FROM dbo.HoaDon) <> 60
        THROW 51006, N'Dữ liệu mẫu: số hóa đơn khác 60.', 1;

    COMMIT TRANSACTION;
    PRINT N'Nạp dữ liệu mẫu BookPOS thành công';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

/* =====================================================================
   7. CÂU LỆNH KIỂM TRA
   ===================================================================== */

-- 7.1 Số dòng mỗi bảng (mong đợi: 5, 9, 40, 15, 5, 60, 96)
SELECT N'TaiKhoan' AS Bang, COUNT(*) AS SoDong FROM dbo.TaiKhoan
UNION ALL SELECT N'LoaiSach',      COUNT(*) FROM dbo.LoaiSach
UNION ALL SELECT N'Sach',          COUNT(*) FROM dbo.Sach
UNION ALL SELECT N'KhachHang',     COUNT(*) FROM dbo.KhachHang
UNION ALL SELECT N'KhuyenMai',     COUNT(*) FROM dbo.KhuyenMai
UNION ALL SELECT N'HoaDon',        COUNT(*) FROM dbo.HoaDon
UNION ALL SELECT N'ChiTietHoaDon', COUNT(*) FROM dbo.ChiTietHoaDon;

-- 7.2 Hóa đơn hôm nay (mong đợi: 6 hóa đơn, 5 hoàn tất, 1 đã hủy; tất cả NgayLap < GETDATE())
SELECT COUNT(*)                                          AS SoHoaDonHomNay,
       SUM(CASE WHEN TrangThai = 1 THEN 1 ELSE 0 END)    AS HoanTat,
       SUM(CASE WHEN TrangThai = 2 THEN 1 ELSE 0 END)    AS DaHuy,
       MAX(NgayLap)                                      AS GioLapMuonNhat,
       GETDATE()                                         AS ThoiDiemHienTai
FROM dbo.HoaDon
WHERE NgayLap >= CAST(CAST(GETDATE() AS DATE) AS DATETIME);

-- 7.3 Hóa đơn có TamTinh <> SUM(ThanhTien) – PHẢI RỖNG
SELECT hd.MaHD, hd.TamTinh, ct.TongThanhTien
FROM dbo.HoaDon AS hd
INNER JOIN (SELECT MaHD, SUM(ThanhTien) AS TongThanhTien
            FROM dbo.ChiTietHoaDon GROUP BY MaHD) AS ct ON ct.MaHD = hd.MaHD
WHERE hd.TamTinh <> ct.TongThanhTien;

-- 7.4 Số hóa đơn theo ngày trong 14 ngày (mỗi ngày phải >= 2)
DECLARE @TuNgay DATE = DATEADD(DAY, -13, CAST(GETDATE() AS DATE));
DECLARE @DenNgay DATE = CAST(GETDATE() AS DATE);
EXEC dbo.sp_DoanhThuTheoNgay @TuNgay = @TuNgay, @DenNgay = @DenNgay;

-- 7.5 Đăng nhập admin (phải ra 1 dòng)
DECLARE @MatKhauAdmin VARCHAR(64) = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'admin123'), 2);
EXEC dbo.sp_DangNhap 'admin', @MatKhauAdmin;

-- 7.6 Sách mua kèm Nhà giả kim (dòng đầu phải là S0002 Đắc nhân tâm)
EXEC dbo.sp_SachMuaKem 'S0001';
GO
