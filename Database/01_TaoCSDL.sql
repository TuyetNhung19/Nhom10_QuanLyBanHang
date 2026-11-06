USE master;
GO

DECLARE @TenSP TABLE (TenSP SYSNAME COLLATE DATABASE_DEFAULT PRIMARY KEY);
INSERT INTO @TenSP (TenSP)
VALUES
        (N'sp_DangNhap'),
        (N'sp_TimSach'),
        (N'sp_LaySachTheoMaHoacISBN'),
        (N'sp_TaoMaHoaDon'),
        (N'sp_LapHoaDon'),
        (N'sp_HuyHoaDon'),
        (N'sp_DoanhThuTheoNgay'),
        (N'sp_DoanhThuTheoThang'),
        (N'sp_SachBanChay'),
        (N'sp_SachSapHet'),
        (N'sp_DoanhThuTheoThuNgan'),
        (N'sp_HieuQuaKhuyenMai'),
        (N'sp_KhuyenMaiHieuLuc'),
        (N'sp_SachMuaKem'),
        (N'sp_TongQuanTrongNgay'),
        (N'sp_BaoCaoCuoiNgay'),
        (N'sp_TraCuuHoaDon'),
        (N'sp_LayChiTietHoaDon');

DECLARE @Sql NVARCHAR(MAX);

-- master
SET @Sql = N'';
SELECT @Sql = @Sql + N'DROP PROCEDURE dbo.' + QUOTENAME(o.name) + N'; '
              + N'PRINT N''Đã xóa master.dbo.' + o.name + N' (trùng tên)''; '
FROM master.sys.objects AS o
INNER JOIN @TenSP AS t ON t.TenSP = o.name COLLATE DATABASE_DEFAULT
WHERE o.type = 'P' AND o.schema_id = 1;
IF @Sql <> N'' EXEC master.sys.sp_executesql @Sql;

-- model (CSDL mẫu: mọi CSDL mới đều sao chép từ đây)
SET @Sql = N'';
SELECT @Sql = @Sql + N'DROP PROCEDURE dbo.' + QUOTENAME(o.name) + N'; '
              + N'PRINT N''Đã xóa model.dbo.' + o.name + N' (trùng tên)''; '
FROM model.sys.objects AS o
INNER JOIN @TenSP AS t ON t.TenSP = o.name COLLATE DATABASE_DEFAULT
WHERE o.type = 'P' AND o.schema_id = 1;
IF @Sql <> N'' EXEC model.sys.sp_executesql @Sql;

-- Đối tượng trùng tên nhưng KHÔNG phải SP (bảng, view, synonym...) thì không tự xóa, chỉ cảnh báo
DECLARE @CanhBao NVARCHAR(MAX) = NULL;
SELECT @CanhBao = ISNULL(@CanhBao + N', ', N'') + x.CSDL + N'.' + x.name + N' (' + x.type_desc + N')'
FROM (
    SELECT N'master' AS CSDL, o.name COLLATE DATABASE_DEFAULT AS name, o.type_desc COLLATE DATABASE_DEFAULT AS type_desc
    FROM master.sys.objects AS o INNER JOIN @TenSP AS t ON t.TenSP = o.name COLLATE DATABASE_DEFAULT
    WHERE o.type <> 'P'
    UNION ALL
    SELECT N'model', o.name COLLATE DATABASE_DEFAULT, o.type_desc COLLATE DATABASE_DEFAULT
    FROM model.sys.objects AS o INNER JOIN @TenSP AS t ON t.TenSP = o.name COLLATE DATABASE_DEFAULT
    WHERE o.type <> 'P'
) AS x;
IF @CanhBao IS NOT NULL
    PRINT N'CẢNH BÁO – có đối tượng trùng tên SP cần xóa tay: ' + @CanhBao;
GO

IF DB_ID(N'QuanLyBanSachDB') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyBanSachDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyBanSachDB;
END
GO

CREATE DATABASE QuanLyBanSachDB;
GO

USE QuanLyBanSachDB;
GO

-- Filtered index và các SP yêu cầu hai thiết lập này (sqlcmd mặc định QUOTED_IDENTIFIER OFF)
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* =====================================================================
   1. BẢNG
   (Khóa chính PK_ và giá trị mặc định DF_ khai báo cùng bảng;
    UQ_, CK_, FK_ và index khai báo ở phần 2)
   ===================================================================== */

-- Tài khoản đăng nhập. MatKhau là chuỗi băm SHA-256 dạng hex IN HOA (TK-02)
CREATE TABLE dbo.TaiKhoan
(
    MaTK        INT IDENTITY(1,1) NOT NULL,
    TenDangNhap VARCHAR(50)       NOT NULL,
    MatKhau     VARCHAR(100)      NOT NULL,
    HoTen       NVARCHAR(100)     NOT NULL,
    VaiTro      TINYINT           NOT NULL,   -- 1 Thu ngân, 2 Quản lý, 3 Admin
    TrangThai   BIT               NOT NULL CONSTRAINT DF_TaiKhoan_TrangThai DEFAULT (1),
    CONSTRAINT PK_TaiKhoan PRIMARY KEY CLUSTERED (MaTK)
);
GO

-- Loại sách (thể loại)
CREATE TABLE dbo.LoaiSach
(
    MaLoai  INT IDENTITY(1,1) NOT NULL,
    TenLoai NVARCHAR(100)     NOT NULL,
    MoTa    NVARCHAR(255)     NULL,
    CONSTRAINT PK_LoaiSach PRIMARY KEY CLUSTERED (MaLoai)
);
GO

-- Sách (sản phẩm). TrangThai: 1 đang bán, 0 ẩn (SP-03)
CREATE TABLE dbo.Sach
(
    MaSach      VARCHAR(20)    NOT NULL,
    ISBN        VARCHAR(13)    NULL,
    TenSach     NVARCHAR(255)  NOT NULL,
    TacGia      NVARCHAR(100)  NULL,
    NhaXuatBan  NVARCHAR(100)  NULL,
    MaLoai      INT            NOT NULL,
    GiaBan      DECIMAL(12,0)  NOT NULL,
    SoLuongTon  INT            NOT NULL CONSTRAINT DF_Sach_SoLuongTon  DEFAULT (0),
    TonToiThieu INT            NOT NULL CONSTRAINT DF_Sach_TonToiThieu DEFAULT (5),
    AnhBia      NVARCHAR(255)  NULL,      -- chỉ lưu tên file, ảnh nằm trong thư mục Images
    TrangThai   BIT            NOT NULL CONSTRAINT DF_Sach_TrangThai   DEFAULT (1),
    CONSTRAINT PK_Sach PRIMARY KEY CLUSTERED (MaSach)
);
GO

-- Khách hàng (khách quen)
CREATE TABLE dbo.KhachHang
(
    MaKH   INT IDENTITY(1,1) NOT NULL,
    HoTen  NVARCHAR(100)     NOT NULL,
    SDT    VARCHAR(15)       NOT NULL,
    Email  VARCHAR(100)      NULL,
    DiaChi NVARCHAR(255)     NULL,
    CONSTRAINT PK_KhachHang PRIMARY KEY CLUSTERED (MaKH)
);
GO

-- Chương trình khuyến mãi (ĐT3)
-- LoaiKM: 1 theo tổng đơn (NguongTien + PhanTramGiam)
--         2 theo loại sách (MaLoai + PhanTramGiam)
--         3 theo số cuốn   (NguongSoLuong + SoTienGiam)
CREATE TABLE dbo.KhuyenMai
(
    MaKM          INT IDENTITY(1,1) NOT NULL,
    TenKM         NVARCHAR(100)     NOT NULL,
    LoaiKM        TINYINT           NOT NULL,
    NguongTien    DECIMAL(14,0)     NULL,
    MaLoai        INT               NULL,
    NguongSoLuong INT               NULL,
    PhanTramGiam  TINYINT           NULL,
    SoTienGiam    DECIMAL(14,0)     NULL,
    NgayBatDau    DATE              NOT NULL,
    NgayKetThuc   DATE              NOT NULL,
    TrangThai     BIT               NOT NULL CONSTRAINT DF_KhuyenMai_TrangThai DEFAULT (1),
    CONSTRAINT PK_KhuyenMai PRIMARY KEY CLUSTERED (MaKM)
);
GO

-- Hóa đơn (master). MaKH NULL = khách lẻ. TrangThai: 1 hoàn tất, 2 đã hủy
CREATE TABLE dbo.HoaDon
(
    MaHD          VARCHAR(20)    NOT NULL,   -- HD + yyyyMMdd + 3 số (HD-07)
    NgayLap       DATETIME       NOT NULL CONSTRAINT DF_HoaDon_NgayLap   DEFAULT (GETDATE()),
    MaTK          INT            NOT NULL,
    MaKH          INT            NULL,
    TamTinh       DECIMAL(14,0)  NOT NULL,
    GiamGia       DECIMAL(14,0)  NOT NULL CONSTRAINT DF_HoaDon_GiamGia   DEFAULT (0),
    MaKM          INT            NULL,
    TongThanhToan DECIMAL(14,0)  NOT NULL,
    TienKhachDua  DECIMAL(14,0)  NOT NULL,
    TienThoi      DECIMAL(14,0)  NOT NULL,
    TrangThai     TINYINT        NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT (1),
    LyDoHuy       NVARCHAR(255)  NULL,
    GhiChu        NVARCHAR(255)  NULL,
    CONSTRAINT PK_HoaDon PRIMARY KEY CLUSTERED (MaHD)
);
GO

-- Chi tiết hóa đơn (detail). DonGia là giá tại thời điểm bán (HD-04)
CREATE TABLE dbo.ChiTietHoaDon
(
    MaHD      VARCHAR(20)    NOT NULL,
    MaSach    VARCHAR(20)    NOT NULL,
    SoLuong   INT            NOT NULL,
    DonGia    DECIMAL(12,0)  NOT NULL,
    ThanhTien DECIMAL(14,0)  NOT NULL,
    CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY CLUSTERED (MaHD, MaSach)
);
GO

/* =====================================================================
   2. RÀNG BUỘC & INDEX
   ===================================================================== */

-- ---------- TaiKhoan (TK-01, TK-02) ----------
ALTER TABLE dbo.TaiKhoan ADD
    CONSTRAINT UQ_TaiKhoan_TenDangNhap UNIQUE (TenDangNhap),
    CONSTRAINT CK_TaiKhoan_TenDangNhap CHECK (TenDangNhap <> ''),
    CONSTRAINT CK_TaiKhoan_HoTen       CHECK (HoTen <> N''),
    CONSTRAINT CK_TaiKhoan_VaiTro      CHECK (VaiTro IN (1, 2, 3)),
    -- TK-02: chỉ chấp nhận chuỗi băm SHA-256 dạng hex 64 ký tự, chặn lưu mật khẩu thô
    CONSTRAINT CK_TaiKhoan_MatKhauBam  CHECK (LEN(MatKhau) = 64 AND MatKhau NOT LIKE '%[^0-9A-F]%');
GO

-- ---------- LoaiSach ----------
ALTER TABLE dbo.LoaiSach ADD
    CONSTRAINT UQ_LoaiSach_TenLoai UNIQUE (TenLoai),
    CONSTRAINT CK_LoaiSach_TenLoai CHECK (TenLoai <> N'');
GO

-- ---------- Sach (SP-01, SP-02) ----------
ALTER TABLE dbo.Sach ADD
    CONSTRAINT FK_Sach_LoaiSach FOREIGN KEY (MaLoai) REFERENCES dbo.LoaiSach (MaLoai)
        ON DELETE NO ACTION ON UPDATE NO ACTION,              -- SP-04: loại đang có sách không xóa được
    CONSTRAINT CK_Sach_MaSach      CHECK (MaSach <> ''),
    CONSTRAINT CK_Sach_TenSach     CHECK (TenSach <> N''),
    CONSTRAINT CK_Sach_GiaBan      CHECK (GiaBan >= 0),
    CONSTRAINT CK_Sach_SoLuongTon  CHECK (SoLuongTon >= 0),   -- HD-02: tồn không bao giờ âm
    CONSTRAINT CK_Sach_TonToiThieu CHECK (TonToiThieu >= 0),
    -- ISBN chỉ gồm chữ số, dài đúng 10 hoặc 13 ký tự (NULL được phép)
    CONSTRAINT CK_Sach_ISBN CHECK (ISBN IS NULL
                                   OR (DATALENGTH(ISBN) IN (10, 13) AND ISBN NOT LIKE '%[^0-9]%'));
GO

-- ISBN duy nhất khi có giá trị (nhiều sách được phép để trống ISBN)
CREATE UNIQUE NONCLUSTERED INDEX UX_Sach_ISBN ON dbo.Sach (ISBN) WHERE ISBN IS NOT NULL;
GO
CREATE NONCLUSTERED INDEX IX_Sach_MaLoai ON dbo.Sach (MaLoai);
GO

-- ---------- KhachHang (KH-01) ----------
ALTER TABLE dbo.KhachHang ADD
    CONSTRAINT UQ_KhachHang_SDT  UNIQUE (SDT),
    CONSTRAINT CK_KhachHang_HoTen CHECK (HoTen <> N''),
    CONSTRAINT CK_KhachHang_SDT  CHECK (SDT LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]');
GO

-- ---------- KhuyenMai (KM-02, KM-03) ----------
ALTER TABLE dbo.KhuyenMai ADD
    CONSTRAINT FK_KhuyenMai_LoaiSach FOREIGN KEY (MaLoai) REFERENCES dbo.LoaiSach (MaLoai)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CK_KhuyenMai_TenKM         CHECK (TenKM <> N''),
    CONSTRAINT CK_KhuyenMai_LoaiKM        CHECK (LoaiKM BETWEEN 1 AND 3),
    CONSTRAINT CK_KhuyenMai_PhanTramGiam  CHECK (PhanTramGiam IS NULL OR PhanTramGiam BETWEEN 1 AND 50),
    CONSTRAINT CK_KhuyenMai_SoTienGiam    CHECK (SoTienGiam IS NULL OR SoTienGiam > 0),
    CONSTRAINT CK_KhuyenMai_NguongTien    CHECK (NguongTien IS NULL OR NguongTien >= 0),
    CONSTRAINT CK_KhuyenMai_NguongSoLuong CHECK (NguongSoLuong IS NULL OR NguongSoLuong > 0),
    CONSTRAINT CK_KhuyenMai_Ngay          CHECK (NgayKetThuc >= NgayBatDau),
    -- Mỗi loại khuyến mãi bắt buộc có đủ tham số của loại đó
    CONSTRAINT CK_KhuyenMai_ThamSoTheoLoai CHECK (
           (LoaiKM = 1 AND NguongTien    IS NOT NULL AND PhanTramGiam IS NOT NULL)
        OR (LoaiKM = 2 AND MaLoai        IS NOT NULL AND PhanTramGiam IS NOT NULL)
        OR (LoaiKM = 3 AND NguongSoLuong IS NOT NULL AND SoTienGiam   IS NOT NULL));
GO

-- ---------- HoaDon (HD-03, HD-06, HD-07, KM-03) ----------
ALTER TABLE dbo.HoaDon ADD
    CONSTRAINT FK_HoaDon_TaiKhoan  FOREIGN KEY (MaTK) REFERENCES dbo.TaiKhoan (MaTK)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang (MaKH)
        ON DELETE NO ACTION ON UPDATE NO ACTION,              -- KH-02: khách có hóa đơn không xóa được
    CONSTRAINT FK_HoaDon_KhuyenMai FOREIGN KEY (MaKM) REFERENCES dbo.KhuyenMai (MaKM)
        ON DELETE NO ACTION ON UPDATE NO ACTION,              -- KM-04: KM đã dùng không xóa được
    CONSTRAINT CK_HoaDon_MaHD          CHECK (MaHD LIKE 'HD[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    CONSTRAINT CK_HoaDon_TrangThai     CHECK (TrangThai IN (1, 2)),
    CONSTRAINT CK_HoaDon_TamTinh       CHECK (TamTinh >= 0),
    CONSTRAINT CK_HoaDon_GiamGia       CHECK (GiamGia >= 0 AND GiamGia <= TamTinh),
    CONSTRAINT CK_HoaDon_TongThanhToan CHECK (TongThanhToan = TamTinh - GiamGia),
    CONSTRAINT CK_HoaDon_TienKhachDua  CHECK (TienKhachDua >= TongThanhToan),
    CONSTRAINT CK_HoaDon_TienThoi      CHECK (TienThoi = TienKhachDua - TongThanhToan),
    CONSTRAINT CK_HoaDon_LyDoHuy       CHECK (TrangThai = 1 OR LyDoHuy IS NOT NULL);
GO

CREATE NONCLUSTERED INDEX IX_HoaDon_NgayLap
    ON dbo.HoaDon (NgayLap)
    INCLUDE (MaTK, TrangThai, TamTinh, GiamGia, TongThanhToan, MaKM);
GO
CREATE NONCLUSTERED INDEX IX_HoaDon_MaTK_NgayLap
    ON dbo.HoaDon (MaTK, NgayLap)
    INCLUDE (TrangThai, TamTinh, GiamGia, TongThanhToan, MaKM);
GO
CREATE NONCLUSTERED INDEX IX_HoaDon_MaKH
    ON dbo.HoaDon (MaKH)
    INCLUDE (NgayLap, TrangThai, TongThanhToan);
GO

-- ---------- ChiTietHoaDon (HD-01) – FK KHÔNG cascade delete ----------
ALTER TABLE dbo.ChiTietHoaDon ADD
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHD) REFERENCES dbo.HoaDon (MaHD)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_ChiTietHoaDon_Sach   FOREIGN KEY (MaSach) REFERENCES dbo.Sach (MaSach)
        ON DELETE NO ACTION ON UPDATE NO ACTION,              -- SP-03: sách đã bán chỉ được ẩn
    CONSTRAINT CK_ChiTietHoaDon_SoLuong   CHECK (SoLuong > 0),
    CONSTRAINT CK_ChiTietHoaDon_DonGia    CHECK (DonGia >= 0),
    CONSTRAINT CK_ChiTietHoaDon_ThanhTien CHECK (ThanhTien = SoLuong * DonGia);
GO

CREATE NONCLUSTERED INDEX IX_ChiTietHoaDon_MaSach
    ON dbo.ChiTietHoaDon (MaSach)
    INCLUDE (SoLuong, ThanhTien);
GO

/* =====================================================================
   3. KIỂU BẢNG (table-valued parameter)
   ===================================================================== */

-- Danh sách dòng chi tiết truyền từ C# vào sp_LapHoaDon
-- (C#: SqlDbType.Structured, TypeName = "dbo.ChiTietHoaDonType", giá trị là DataTable 2 cột MaSach, SoLuong)
CREATE TYPE dbo.ChiTietHoaDonType AS TABLE
(
    MaSach  VARCHAR(20) NOT NULL PRIMARY KEY,
    SoLuong INT         NOT NULL
);
GO

/* =====================================================================
   4. STORED PROCEDURE
   ===================================================================== */

/* ---------------------------------------------------------------------
   sp_DangNhap
   Mục đích : Kiểm tra đăng nhập.
   Tham số  : @TenDangNhap – tên đăng nhập; @MatKhau – mật khẩu ĐÃ BĂM SHA-256 (hex in hoa).
   Kết quả  : 1 dòng (MaTK, TenDangNhap, HoTen, VaiTro, TrangThai) nếu đúng tên + mật khẩu,
              kể cả tài khoản bị khóa (TrangThai = 0) để BLL báo "Tài khoản đã bị khóa".
              Sai tên hoặc mật khẩu: không trả dòng nào.
   Quy tắc  : TK-01, TK-02.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_DangNhap
    @TenDangNhap VARCHAR(50),
    @MatKhau     VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT tk.MaTK, tk.TenDangNhap, tk.HoTen, tk.VaiTro, tk.TrangThai
    FROM dbo.TaiKhoan AS tk
    WHERE tk.TenDangNhap = @TenDangNhap
      AND tk.MatKhau     = @MatKhau;
END
GO

/* ---------------------------------------------------------------------
   sp_TimSach
   Mục đích : Tìm sách theo từ khóa (mã, ISBN, tên, tác giả) và loại.
   Tham số  : @TuKhoa – chuỗi tìm (NULL/rỗng = tất cả); @MaLoai – lọc loại (NULL = tất cả);
              @ChiDangBan – 1 chỉ lấy sách đang bán, 0 lấy cả sách đã ẩn.
   Kết quả  : Danh sách sách kèm TenLoai và cờ SapHet (SoLuongTon <= TonToiThieu), sắp theo TenSach.
   Ghi chú  : So khớp không phân biệt hoa thường/dấu bằng COLLATE Vietnamese_CI_AI.
              Ký tự đặc biệt của LIKE ([, %, _) trong từ khóa được thoát để tìm đúng nghĩa đen.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_TimSach
    @TuKhoa     NVARCHAR(255) = NULL,
    @MaLoai     INT           = NULL,
    @ChiDangBan BIT           = 1
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Mau NVARCHAR(800) = NULL;
    SET @TuKhoa = LTRIM(RTRIM(@TuKhoa));
    IF @TuKhoa IS NOT NULL AND @TuKhoa <> N''
        SET @Mau = N'%' + REPLACE(REPLACE(REPLACE(@TuKhoa, N'[', N'[[]'), N'%', N'[%]'), N'_', N'[_]') + N'%';

    SELECT s.MaSach, s.ISBN, s.TenSach, s.TacGia, s.NhaXuatBan,
           s.MaLoai, l.TenLoai, s.GiaBan, s.SoLuongTon, s.TonToiThieu,
           s.AnhBia, s.TrangThai,
           CAST(CASE WHEN s.SoLuongTon <= s.TonToiThieu THEN 1 ELSE 0 END AS BIT) AS SapHet
    FROM dbo.Sach AS s
    INNER JOIN dbo.LoaiSach AS l ON l.MaLoai = s.MaLoai
    WHERE (@ChiDangBan = 0 OR s.TrangThai = 1)
      AND (@MaLoai IS NULL OR s.MaLoai = @MaLoai)
      AND (@Mau IS NULL
           OR s.MaSach  COLLATE Vietnamese_CI_AI LIKE @Mau
           OR s.ISBN    COLLATE Vietnamese_CI_AI LIKE @Mau
           OR s.TenSach COLLATE Vietnamese_CI_AI LIKE @Mau
           OR s.TacGia  COLLATE Vietnamese_CI_AI LIKE @Mau)
    ORDER BY s.TenSach
    OPTION (RECOMPILE);
END
GO

/* ---------------------------------------------------------------------
   sp_LaySachTheoMaHoacISBN
   Mục đích : ĐT1 – thu ngân gõ/quét mã rồi Enter: tìm đúng 1 cuốn theo MaSach hoặc ISBN.
   Tham số  : @Ma – mã sách hoặc ISBN (khớp CHÍNH XÁC).
   Kết quả  : 0 hoặc 1 dòng, cùng cấu trúc cột với sp_TimSach; chỉ sách đang bán (TrangThai = 1).
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_LaySachTheoMaHoacISBN
    @Ma VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Ma = LTRIM(RTRIM(@Ma));

    SELECT TOP (1)
           s.MaSach, s.ISBN, s.TenSach, s.TacGia, s.NhaXuatBan,
           s.MaLoai, l.TenLoai, s.GiaBan, s.SoLuongTon, s.TonToiThieu,
           s.AnhBia, s.TrangThai,
           CAST(CASE WHEN s.SoLuongTon <= s.TonToiThieu THEN 1 ELSE 0 END AS BIT) AS SapHet
    FROM dbo.Sach AS s
    INNER JOIN dbo.LoaiSach AS l ON l.MaLoai = s.MaLoai
    WHERE s.TrangThai = 1
      AND (s.MaSach = @Ma OR s.ISBN = @Ma)
    ORDER BY CASE WHEN s.MaSach = @Ma THEN 0 ELSE 1 END;   -- ưu tiên khớp mã sách
END
GO

/* ---------------------------------------------------------------------
   sp_TaoMaHoaDon
   Mục đích : Sinh mã hóa đơn 'HD' + yyyyMMdd + số thứ tự 3 chữ số
              (= số lớn nhất đã dùng trong ngày + 1).
   Tham số  : @Ngay – ngày lập (NULL = hôm nay); @MaHD OUTPUT – mã sinh ra.
   Kết quả  : @MaHD, ví dụ HD20261028001.
   Quy tắc  : HD-07. Khi được gọi TRONG transaction của sp_LapHoaDon, khóa UPDLOCK + HOLDLOCK
              giữ dải mã của ngày đến khi COMMIT nên 2 máy lưu cùng lúc không trùng mã.
              Khi gọi riêng (hiển thị mã dự kiến lúc mở Form) khóa chỉ giữ trong 1 câu lệnh.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_TaoMaHoaDon
    @Ngay DATE = NULL,
    @MaHD VARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF @Ngay IS NULL SET @Ngay = CAST(GETDATE() AS DATE);

    DECLARE @TienTo VARCHAR(10) = 'HD' + CONVERT(CHAR(8), @Ngay, 112);   -- 112 = yyyyMMdd
    DECLARE @SoCuoi INT;

    SELECT @SoCuoi = MAX(CAST(RIGHT(hd.MaHD, 3) AS INT))
    FROM dbo.HoaDon AS hd WITH (UPDLOCK, HOLDLOCK)
    WHERE hd.MaHD LIKE @TienTo + '[0-9][0-9][0-9]';

    SET @SoCuoi = ISNULL(@SoCuoi, 0) + 1;

    IF @SoCuoi > 999
        THROW 50008, N'Đã hết số thứ tự hóa đơn trong ngày (tối đa 999 hóa đơn/ngày).', 1;

    SET @MaHD = @TienTo + RIGHT('000' + CAST(@SoCuoi AS VARCHAR(10)), 3);
END
GO

/* ---------------------------------------------------------------------
   sp_LapHoaDon  (SP quan trọng nhất)
   Mục đích : Lưu hóa đơn + chi tiết + trừ tồn kho trong MỘT transaction.
   Tham số  : @MaTK – thu ngân lập; @MaKH – khách (NULL = khách lẻ); @MaKM – khuyến mãi (NULL = không);
              @GiamGia – tiền giảm do BLL tính; @TamTinhDuKien – tạm tính đang hiển thị trên Form;
              @TienKhachDua; @GhiChu; @ChiTiet – danh sách (MaSach, SoLuong);
              @MaHD OUTPUT – mã hóa đơn đã lưu.
   Kết quả  : @MaHD và 1 dòng HoaDon vừa lưu. Lỗi nghiệp vụ: THROW 50001..50006, 50008.
   Quy tắc  : HD-01..HD-07, KM-01..KM-03, TK-01.
   Công thức tiền giảm SP kiểm tra lại (BLL phải tính giống hệt, làm tròn nửa lên – AwayFromZero):
              Loại 1: TamTinh >= NguongTien        → ROUND(TamTinh * PhanTramGiam / 100, 0)
              Loại 2: có sách thuộc MaLoai          → ROUND(Σ ThanhTien của loại đó * PhanTramGiam / 100, 0)
              Loại 3: tổng số cuốn >= NguongSoLuong → MIN(SoTienGiam, TamTinh)
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_LapHoaDon
    @MaTK          INT,
    @MaKH          INT           = NULL,
    @MaKM          INT           = NULL,
    @GiamGia       DECIMAL(14,0),
    @TamTinhDuKien DECIMAL(14,0),
    @TienKhachDua  DECIMAL(14,0),
    @GhiChu        NVARCHAR(255) = NULL,
    @ChiTiet       dbo.ChiTietHoaDonType READONLY,
    @MaHD          VARCHAR(20)   OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;   -- mọi lỗi (kể cả mất kết nối/timeout) đều hủy toàn bộ transaction

    DECLARE @Now  DATETIME = GETDATE();
    DECLARE @Ngay DATE     = CAST(@Now AS DATE);
    DECLARE @Msg  NVARCHAR(2048) = NULL;

    DECLARE @TamTinh       DECIMAL(14,0),
            @TongThanhToan DECIMAL(14,0),
            @TienThoi      DECIMAL(14,0),
            @TongSoCuon    INT,
            @TamTinhLoai   DECIMAL(14,0),
            @GiamGiaHopLe  DECIMAL(14,0);

    DECLARE @LoaiKM     TINYINT,
            @PhanTram   TINYINT,
            @NguongTien DECIMAL(14,0),
            @MaLoaiKM   INT,
            @NguongSL   INT,
            @SoTienGiam DECIMAL(14,0);

    -- Bản sao dữ liệu sách tại thời điểm lưu (đã khóa)
    DECLARE @Sach TABLE
    (
        MaSach     VARCHAR(20)   NOT NULL PRIMARY KEY,
        TenSach    NVARCHAR(255) NULL,
        MaLoai     INT           NULL,
        GiaBan     DECIMAL(12,0) NULL,
        SoLuongTon INT           NULL,
        TrangThai  BIT           NULL,
        SoLuong    INT           NOT NULL
    );

    SET @MaHD = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Tài khoản phải tồn tại, đang hoạt động, là Thu ngân hoặc Quản lý (TK-01, phân quyền)
        IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan
                       WHERE MaTK = @MaTK AND TrangThai = 1 AND VaiTro IN (1, 2))
            THROW 50001, N'Tài khoản lập hóa đơn không hợp lệ, đã bị khóa hoặc không có quyền bán hàng.', 1;

        -- 2. Hóa đơn phải có ít nhất 1 dòng, số lượng > 0 (HD-01)
        IF NOT EXISTS (SELECT 1 FROM @ChiTiet)
            THROW 50006, N'Hóa đơn phải có ít nhất một cuốn sách.', 1;

        IF EXISTS (SELECT 1 FROM @ChiTiet WHERE SoLuong IS NULL OR SoLuong <= 0)
            THROW 50006, N'Số lượng của mỗi dòng trong hóa đơn phải lớn hơn 0.', 1;

        -- 3. Khóa các dòng sách liên quan đến hết transaction để không ai bán chen (HD-02)
        INSERT INTO @Sach (MaSach, TenSach, MaLoai, GiaBan, SoLuongTon, TrangThai, SoLuong)
        SELECT ct.MaSach, s.TenSach, s.MaLoai, s.GiaBan, s.SoLuongTon, s.TrangThai, ct.SoLuong
        FROM @ChiTiet AS ct
        LEFT JOIN dbo.Sach AS s WITH (UPDLOCK, HOLDLOCK, ROWLOCK) ON s.MaSach = ct.MaSach;

        SELECT TOP (1) @Msg =
               CASE
                   WHEN x.TenSach IS NULL THEN
                        N'Không tìm thấy sách có mã ' + x.MaSach + N'.'
                   WHEN x.TrangThai = 0 THEN
                        N'Sách "' + x.TenSach + N'" đã ngừng bán.'
                   ELSE N'Sách "' + x.TenSach + N'" chỉ còn ' + CAST(x.SoLuongTon AS NVARCHAR(10))
                        + N' cuốn, không đủ bán ' + CAST(x.SoLuong AS NVARCHAR(10)) + N' cuốn.'
               END
        FROM @Sach AS x
        WHERE x.TenSach IS NULL OR x.TrangThai = 0 OR x.SoLuongTon < x.SoLuong
        ORDER BY x.MaSach;

        IF @Msg IS NOT NULL
            THROW 50002, @Msg, 1;

        -- 4. Đơn giá lấy từ Sach.GiaBan tại thời điểm lưu (HD-04); so với tạm tính trên Form
        SELECT @TamTinh    = SUM(x.GiaBan * x.SoLuong),
               @TongSoCuon = SUM(x.SoLuong)
        FROM @Sach AS x;

        IF @TamTinhDuKien IS NULL OR @TamTinh <> @TamTinhDuKien
        BEGIN
            SET @Msg = N'Giá sách đã thay đổi: tạm tính hiện hành là '
                     + FORMAT(@TamTinh, N'#,##0', 'vi-VN')
                     + N' đ, khác với hóa đơn đang hiển thị. Vui lòng tải lại hóa đơn.';
            THROW 50005, @Msg, 1;
        END

        -- 5. Khuyến mãi (KM-01, KM-02, KM-03)
        IF @GiamGia IS NULL OR @GiamGia < 0
            THROW 50004, N'Tiền giảm giá không hợp lệ.', 1;

        IF @MaKM IS NULL
        BEGIN
            IF @GiamGia <> 0
                THROW 50004, N'Hóa đơn không áp dụng khuyến mãi thì tiền giảm phải bằng 0.', 1;
        END
        ELSE
        BEGIN
            SELECT @LoaiKM     = km.LoaiKM,
                   @PhanTram   = km.PhanTramGiam,
                   @NguongTien = km.NguongTien,
                   @MaLoaiKM   = km.MaLoai,
                   @NguongSL   = km.NguongSoLuong,
                   @SoTienGiam = km.SoTienGiam
            FROM dbo.KhuyenMai AS km
            WHERE km.MaKM = @MaKM
              AND km.TrangThai = 1
              AND @Ngay BETWEEN km.NgayBatDau AND km.NgayKetThuc;   -- KM-02

            IF @LoaiKM IS NULL
                THROW 50004, N'Khuyến mãi không tồn tại, đã tắt hoặc không còn hiệu lực hôm nay.', 1;

            IF @GiamGia > @TamTinh                                   -- KM-03
                THROW 50004, N'Tiền giảm không được vượt quá tạm tính.', 1;

            SELECT @TamTinhLoai = ISNULL(SUM(CASE WHEN x.MaLoai = @MaLoaiKM THEN x.GiaBan * x.SoLuong END), 0)
            FROM @Sach AS x;

            -- Tính lại tiền giảm hợp lệ để thu ngân không thể nhập tay (KM-01)
            SET @GiamGiaHopLe =
                CASE
                    WHEN @LoaiKM = 1 AND @TamTinh >= @NguongTien
                        THEN CAST(ROUND(@TamTinh * @PhanTram / 100.0, 0) AS DECIMAL(14,0))
                    WHEN @LoaiKM = 2 AND @TamTinhLoai > 0
                        THEN CAST(ROUND(@TamTinhLoai * @PhanTram / 100.0, 0) AS DECIMAL(14,0))
                    WHEN @LoaiKM = 3 AND @TongSoCuon >= @NguongSL
                        THEN CASE WHEN @SoTienGiam < @TamTinh THEN @SoTienGiam ELSE @TamTinh END
                    ELSE NULL
                END;

            IF @GiamGiaHopLe IS NULL
                THROW 50004, N'Hóa đơn chưa đạt điều kiện của khuyến mãi đã chọn.', 1;

            IF @GiamGia <> @GiamGiaHopLe
            BEGIN
                SET @Msg = N'Tiền giảm không đúng với khuyến mãi (đúng phải là '
                         + FORMAT(@GiamGiaHopLe, N'#,##0', 'vi-VN') + N' đ).';
                THROW 50004, @Msg, 1;
            END
        END

        -- 6. Thanh toán (HD-03)
        SET @TongThanhToan = @TamTinh - @GiamGia;

        IF @TienKhachDua IS NULL OR @TienKhachDua < @TongThanhToan
        BEGIN
            SET @Msg = N'Tiền khách đưa chưa đủ. Cần thanh toán '
                     + FORMAT(@TongThanhToan, N'#,##0', 'vi-VN') + N' đ.';
            THROW 50003, @Msg, 1;
        END

        SET @TienThoi = @TienKhachDua - @TongThanhToan;

        -- 7. Sinh mã hóa đơn bên trong transaction (HD-07)
        EXEC dbo.sp_TaoMaHoaDon @Ngay = @Ngay, @MaHD = @MaHD OUTPUT;

        -- 8. Ghi dữ liệu (HD-05)
        INSERT INTO dbo.HoaDon (MaHD, NgayLap, MaTK, MaKH, TamTinh, GiamGia, MaKM,
                                TongThanhToan, TienKhachDua, TienThoi, TrangThai, LyDoHuy, GhiChu)
        VALUES (@MaHD, @Now, @MaTK, @MaKH, @TamTinh, @GiamGia, @MaKM,
                @TongThanhToan, @TienKhachDua, @TienThoi, 1, NULL,
                NULLIF(LTRIM(RTRIM(@GhiChu)), N''));

        INSERT INTO dbo.ChiTietHoaDon (MaHD, MaSach, SoLuong, DonGia, ThanhTien)
        SELECT @MaHD, x.MaSach, x.SoLuong, x.GiaBan, x.GiaBan * x.SoLuong
        FROM @Sach AS x;

        UPDATE s
        SET s.SoLuongTon = s.SoLuongTon - x.SoLuong
        FROM dbo.Sach AS s
        INNER JOIN @Sach AS x ON x.MaSach = s.MaSach;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SET @MaHD = NULL;
        THROW;
    END CATCH;

    -- Trả lại hóa đơn vừa lưu
    SELECT hd.MaHD, hd.NgayLap, hd.MaTK, hd.MaKH, hd.TamTinh, hd.GiamGia, hd.MaKM,
           hd.TongThanhToan, hd.TienKhachDua, hd.TienThoi, hd.TrangThai, hd.LyDoHuy, hd.GhiChu
    FROM dbo.HoaDon AS hd
    WHERE hd.MaHD = @MaHD;
END
GO

/* ---------------------------------------------------------------------
   sp_HuyHoaDon
   Mục đích : Hủy hóa đơn đã lưu và hoàn lại tồn kho.
   Tham số  : @MaHD – hóa đơn cần hủy; @MaTK – người hủy; @LyDo – lý do (bắt buộc).
   Kết quả  : 1 dòng HoaDon sau khi hủy. Lỗi: THROW 50007.
   Quy tắc  : HD-06 (chỉ Quản lý, hóa đơn đang hoàn tất, có lý do, hủy thì hoàn tồn), HD-05, TK-03.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_HuyHoaDon
    @MaHD VARCHAR(20),
    @MaTK INT,
    @LyDo NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @HoTen     NVARCHAR(100) = NULL;
    DECLARE @TrangThai TINYINT       = NULL;
    DECLARE @Msg       NVARCHAR(2048);

    SET @LyDo = LTRIM(RTRIM(@LyDo));

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @HoTen = tk.HoTen
        FROM dbo.TaiKhoan AS tk
        WHERE tk.MaTK = @MaTK AND tk.VaiTro = 2 AND tk.TrangThai = 1;

        IF @HoTen IS NULL
            THROW 50007, N'Chỉ tài khoản Quản lý đang hoạt động mới được hủy hóa đơn.', 1;

        IF @LyDo IS NULL OR @LyDo = N''
            THROW 50007, N'Phải nhập lý do hủy hóa đơn.', 1;

        SELECT @TrangThai = hd.TrangThai
        FROM dbo.HoaDon AS hd WITH (UPDLOCK, HOLDLOCK)
        WHERE hd.MaHD = @MaHD;

        IF @TrangThai IS NULL
        BEGIN
            SET @Msg = N'Không tìm thấy hóa đơn ' + ISNULL(@MaHD, N'') + N'.';
            THROW 50007, @Msg, 1;
        END

        IF @TrangThai <> 1
            THROW 50007, N'Hóa đơn này đã bị hủy trước đó.', 1;

        UPDATE dbo.HoaDon
        SET TrangThai = 2,
            LyDoHuy   = @LyDo,
            GhiChu    = LEFT(
                            CASE WHEN GhiChu IS NULL OR GhiChu = N'' THEN N'' ELSE GhiChu + N' | ' END
                            + N'Hủy bởi ' + @HoTen + N' lúc ' + FORMAT(GETDATE(), N'HH:mm dd/MM/yyyy'),
                        255)
        WHERE MaHD = @MaHD;

        -- Hoàn tồn kho theo chi tiết hóa đơn
        UPDATE s
        SET s.SoLuongTon = s.SoLuongTon + ct.SoLuong
        FROM dbo.Sach AS s
        INNER JOIN dbo.ChiTietHoaDon AS ct ON ct.MaSach = s.MaSach
        WHERE ct.MaHD = @MaHD;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;

    SELECT hd.MaHD, hd.NgayLap, hd.MaTK, hd.MaKH, hd.TamTinh, hd.GiamGia, hd.MaKM,
           hd.TongThanhToan, hd.TienKhachDua, hd.TienThoi, hd.TrangThai, hd.LyDoHuy, hd.GhiChu
    FROM dbo.HoaDon AS hd
    WHERE hd.MaHD = @MaHD;
END
GO

/* ---------------------------------------------------------------------
   sp_DoanhThuTheoNgay
   Mục đích : Doanh thu theo từng ngày trong khoảng (biểu đồ 7 ngày ĐT5, thống kê).
   Tham số  : @TuNgay, @DenNgay; @MaTK – lọc thu ngân (NULL = tất cả).
   Kết quả  : Mỗi ngày 1 dòng (Ngay, SoHoaDon, TongTamTinh, TongGiamGia, DoanhThu);
              ngày không có hóa đơn vẫn có dòng giá trị 0.
   Quy tắc  : TK-03 – chỉ tính hóa đơn TrangThai = 1.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_DoanhThuTheoNgay
    @TuNgay  DATE,
    @DenNgay DATE,
    @MaTK    INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    WITH DayNgay AS
    (
        SELECT @TuNgay AS Ngay
        WHERE @TuNgay <= @DenNgay
        UNION ALL
        SELECT DATEADD(DAY, 1, d.Ngay)
        FROM DayNgay AS d
        WHERE d.Ngay < @DenNgay
    ),
    TongHop AS
    (
        SELECT CAST(hd.NgayLap AS DATE) AS Ngay,
               COUNT(*)                 AS SoHoaDon,
               SUM(hd.TamTinh)          AS TongTamTinh,
               SUM(hd.GiamGia)          AS TongGiamGia,
               SUM(hd.TongThanhToan)    AS DoanhThu
        FROM dbo.HoaDon AS hd
        WHERE hd.TrangThai = 1
          AND hd.NgayLap >= @TuNgay
          AND hd.NgayLap <  DATEADD(DAY, 1, @DenNgay)
          AND (@MaTK IS NULL OR hd.MaTK = @MaTK)
        GROUP BY CAST(hd.NgayLap AS DATE)
    )
    SELECT d.Ngay,
           ISNULL(t.SoHoaDon, 0)                               AS SoHoaDon,
           CAST(ISNULL(t.TongTamTinh, 0) AS DECIMAL(18,0))     AS TongTamTinh,
           CAST(ISNULL(t.TongGiamGia, 0) AS DECIMAL(18,0))     AS TongGiamGia,
           CAST(ISNULL(t.DoanhThu, 0)    AS DECIMAL(18,0))     AS DoanhThu
    FROM DayNgay AS d
    LEFT JOIN TongHop AS t ON t.Ngay = d.Ngay
    ORDER BY d.Ngay
    OPTION (MAXRECURSION 0, RECOMPILE);
END
GO

/* ---------------------------------------------------------------------
   sp_DoanhThuTheoThang
   Mục đích : Doanh thu gộp theo tháng.
   Tham số  : @TuNgay, @DenNgay.
   Kết quả  : Nam, Thang, SoHoaDon, TongGiamGia, DoanhThu (chỉ tháng có hóa đơn).
   Quy tắc  : TK-03.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_DoanhThuTheoThang
    @TuNgay  DATE,
    @DenNgay DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT YEAR(hd.NgayLap)                              AS Nam,
           MONTH(hd.NgayLap)                             AS Thang,
           COUNT(*)                                      AS SoHoaDon,
           CAST(SUM(hd.GiamGia) AS DECIMAL(18,0))        AS TongGiamGia,
           CAST(SUM(hd.TongThanhToan) AS DECIMAL(18,0))  AS DoanhThu
    FROM dbo.HoaDon AS hd
    WHERE hd.TrangThai = 1
      AND hd.NgayLap >= @TuNgay
      AND hd.NgayLap <  DATEADD(DAY, 1, @DenNgay)
    GROUP BY YEAR(hd.NgayLap), MONTH(hd.NgayLap)
    ORDER BY Nam, Thang;
END
GO

/* ---------------------------------------------------------------------
   sp_SachBanChay
   Mục đích : Top sách bán chạy trong khoảng ngày (thống kê, top 5 trên bảng điều khiển ĐT5).
   Tham số  : @TuNgay, @DenNgay; @Top – số dòng; @MaTK – lọc thu ngân (NULL = tất cả).
   Kết quả  : MaSach, TenSach, SoLuongBan, DoanhThu – sắp SoLuongBan giảm dần.
   Quy tắc  : TK-03.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_SachBanChay
    @TuNgay  DATE,
    @DenNgay DATE,
    @Top     INT = 10,
    @MaTK    INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Top IS NULL OR @Top < 1 SET @Top = 10;

    SELECT TOP (@Top)
           s.MaSach,
           s.TenSach,
           SUM(ct.SoLuong)                             AS SoLuongBan,
           CAST(SUM(ct.ThanhTien) AS DECIMAL(18,0))    AS DoanhThu
    FROM dbo.ChiTietHoaDon AS ct
    INNER JOIN dbo.HoaDon  AS hd ON hd.MaHD = ct.MaHD
    INNER JOIN dbo.Sach    AS s  ON s.MaSach = ct.MaSach
    WHERE hd.TrangThai = 1
      AND hd.NgayLap >= @TuNgay
      AND hd.NgayLap <  DATEADD(DAY, 1, @DenNgay)
      AND (@MaTK IS NULL OR hd.MaTK = @MaTK)
    GROUP BY s.MaSach, s.TenSach
    ORDER BY SoLuongBan DESC, DoanhThu DESC, s.TenSach
    OPTION (RECOMPILE);
END
GO

/* ---------------------------------------------------------------------
   sp_SachSapHet
   Mục đích : Cảnh báo sách sắp hết.
   Tham số  : @MaLoai – lọc loại (NULL = tất cả).
   Kết quả  : Sách đang bán có SoLuongTon <= TonToiThieu, tồn thấp nhất trước.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_SachSapHet
    @MaLoai INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT s.MaSach, s.ISBN, s.TenSach, s.TacGia, s.MaLoai, l.TenLoai,
           s.GiaBan, s.SoLuongTon, s.TonToiThieu
    FROM dbo.Sach AS s
    INNER JOIN dbo.LoaiSach AS l ON l.MaLoai = s.MaLoai
    WHERE s.TrangThai = 1
      AND s.SoLuongTon <= s.TonToiThieu
      AND (@MaLoai IS NULL OR s.MaLoai = @MaLoai)
    ORDER BY s.SoLuongTon, s.TenSach;
END
GO

/* ---------------------------------------------------------------------
   sp_DoanhThuTheoThuNgan
   Mục đích : So sánh doanh thu giữa các thu ngân.
   Tham số  : @TuNgay, @DenNgay.
   Kết quả  : MaTK, HoTen, SoHoaDon, DoanhThu – doanh thu giảm dần.
   Quy tắc  : TK-03.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_DoanhThuTheoThuNgan
    @TuNgay  DATE,
    @DenNgay DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT tk.MaTK,
           tk.HoTen,
           COUNT(*)                                      AS SoHoaDon,
           CAST(SUM(hd.TongThanhToan) AS DECIMAL(18,0))  AS DoanhThu
    FROM dbo.HoaDon AS hd
    INNER JOIN dbo.TaiKhoan AS tk ON tk.MaTK = hd.MaTK
    WHERE hd.TrangThai = 1
      AND hd.NgayLap >= @TuNgay
      AND hd.NgayLap <  DATEADD(DAY, 1, @DenNgay)
    GROUP BY tk.MaTK, tk.HoTen
    ORDER BY DoanhThu DESC, tk.HoTen;
END
GO

/* ---------------------------------------------------------------------
   sp_HieuQuaKhuyenMai
   Mục đích : Đánh giá hiệu quả từng chương trình khuyến mãi (ĐT3).
   Tham số  : @TuNgay, @DenNgay.
   Kết quả  : MaKM, TenKM, SoHoaDonApDung, TongTienGiam, DoanhThu.
   Quy tắc  : TK-03.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_HieuQuaKhuyenMai
    @TuNgay  DATE,
    @DenNgay DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT km.MaKM,
           km.TenKM,
           COUNT(*)                                      AS SoHoaDonApDung,
           CAST(SUM(hd.GiamGia) AS DECIMAL(18,0))        AS TongTienGiam,
           CAST(SUM(hd.TongThanhToan) AS DECIMAL(18,0))  AS DoanhThu
    FROM dbo.HoaDon AS hd
    INNER JOIN dbo.KhuyenMai AS km ON km.MaKM = hd.MaKM
    WHERE hd.TrangThai = 1
      AND hd.NgayLap >= @TuNgay
      AND hd.NgayLap <  DATEADD(DAY, 1, @DenNgay)
    GROUP BY km.MaKM, km.TenKM
    ORDER BY TongTienGiam DESC, km.TenKM;
END
GO

/* ---------------------------------------------------------------------
   sp_KhuyenMaiHieuLuc
   Mục đích : Danh sách khuyến mãi áp dụng được tại ngày bán (BLL chọn KM giảm nhiều nhất).
   Tham số  : @Ngay – ngày lập hóa đơn (NULL = hôm nay).
   Kết quả  : Các KM đang bật và @Ngay nằm trong [NgayBatDau, NgayKetThuc], kèm TenLoai (loại 2).
   Quy tắc  : KM-01, KM-02.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_KhuyenMaiHieuLuc
    @Ngay DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Ngay IS NULL SET @Ngay = CAST(GETDATE() AS DATE);

    SELECT km.MaKM, km.TenKM, km.LoaiKM, km.NguongTien, km.MaLoai, l.TenLoai,
           km.NguongSoLuong, km.PhanTramGiam, km.SoTienGiam,
           km.NgayBatDau, km.NgayKetThuc, km.TrangThai
    FROM dbo.KhuyenMai AS km
    LEFT JOIN dbo.LoaiSach AS l ON l.MaLoai = km.MaLoai
    WHERE km.TrangThai = 1
      AND @Ngay BETWEEN km.NgayBatDau AND km.NgayKetThuc
    ORDER BY km.LoaiKM, km.MaKM;
END
GO

/* ---------------------------------------------------------------------
   sp_SachMuaKem
   Mục đích : ĐT4 – gợi ý sách "thường được mua kèm" với một cuốn.
   Tham số  : @MaSach – sách gốc; @Top – số gợi ý.
   Kết quả  : MaSach, TenSach, GiaBan, SoLuongTon, SoHoaDonChung – sách cùng xuất hiện với @MaSach
              trong hóa đơn hoàn tất, chỉ sách đang bán và còn tồn, loại trừ chính nó.
   Quy tắc  : TK-03. (BLL có bản LINQ tương đương.)
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_SachMuaKem
    @MaSach VARCHAR(20),
    @Top    INT = 3
AS
BEGIN
    SET NOCOUNT ON;

    IF @Top IS NULL OR @Top < 1 SET @Top = 3;

    SELECT TOP (@Top)
           s.MaSach, s.TenSach, s.GiaBan, s.SoLuongTon,
           COUNT(*) AS SoHoaDonChung          -- PK (MaHD, MaSach) nên mỗi hóa đơn đếm 1 lần
    FROM dbo.ChiTietHoaDon AS ctGoc
    INNER JOIN dbo.HoaDon        AS hd   ON hd.MaHD = ctGoc.MaHD AND hd.TrangThai = 1
    INNER JOIN dbo.ChiTietHoaDon AS ctKem ON ctKem.MaHD = ctGoc.MaHD AND ctKem.MaSach <> ctGoc.MaSach
    INNER JOIN dbo.Sach          AS s    ON s.MaSach = ctKem.MaSach
    WHERE ctGoc.MaSach = @MaSach
      AND s.TrangThai = 1
      AND s.SoLuongTon > 0
    GROUP BY s.MaSach, s.TenSach, s.GiaBan, s.SoLuongTon
    ORDER BY SoHoaDonChung DESC, s.TenSach;
END
GO

/* ---------------------------------------------------------------------
   sp_TongQuanTrongNgay
   Mục đích : ĐT5 – số liệu các thẻ trên bảng điều khiển.
   Tham số  : @Ngay (NULL = hôm nay); @MaTK – thu ngân chỉ xem của mình (NULL = tất cả).
   Kết quả  : 1 dòng DoanhThu, SoHoaDon, SoCuonBan, SoSachSapHet.
   Quy tắc  : TK-03.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_TongQuanTrongNgay
    @Ngay DATE = NULL,
    @MaTK INT  = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Ngay IS NULL SET @Ngay = CAST(GETDATE() AS DATE);

    DECLARE @Tu  DATETIME = CAST(@Ngay AS DATETIME);
    DECLARE @Den DATETIME = DATEADD(DAY, 1, @Tu);

    SELECT
        CAST(ISNULL((SELECT SUM(hd.TongThanhToan)
                     FROM dbo.HoaDon AS hd
                     WHERE hd.TrangThai = 1 AND hd.NgayLap >= @Tu AND hd.NgayLap < @Den
                       AND (@MaTK IS NULL OR hd.MaTK = @MaTK)), 0) AS DECIMAL(18,0)) AS DoanhThu,
        (SELECT COUNT(*)
         FROM dbo.HoaDon AS hd
         WHERE hd.TrangThai = 1 AND hd.NgayLap >= @Tu AND hd.NgayLap < @Den
           AND (@MaTK IS NULL OR hd.MaTK = @MaTK))                              AS SoHoaDon,
        ISNULL((SELECT SUM(ct.SoLuong)
                FROM dbo.ChiTietHoaDon AS ct
                INNER JOIN dbo.HoaDon AS hd ON hd.MaHD = ct.MaHD
                WHERE hd.TrangThai = 1 AND hd.NgayLap >= @Tu AND hd.NgayLap < @Den
                  AND (@MaTK IS NULL OR hd.MaTK = @MaTK)), 0)                   AS SoCuonBan,
        (SELECT COUNT(*)
         FROM dbo.Sach AS s
         WHERE s.TrangThai = 1 AND s.SoLuongTon <= s.TonToiThieu)              AS SoSachSapHet;
END
GO

/* ---------------------------------------------------------------------
   sp_BaoCaoCuoiNgay
   Mục đích : ĐT6 – báo cáo cuối ngày theo thu ngân (in 1 trang).
   Tham số  : @Ngay (NULL = hôm nay); @MaTK – thu ngân (NULL = tất cả, dành cho Quản lý).
   Kết quả  : 3 result set
              (1) Ngay, TenThuNgan, SoHoaDon, SoHoaDonHuy, TongTamTinh, TongGiamGia,
                  DoanhThuThuc, TienMatPhaiCo (= DoanhThuThuc vì chỉ thu tiền mặt)
              (2) Hóa đơn bị hủy: MaHD, NgayLap, TongThanhToan, LyDoHuy
              (3) Giảm giá theo khuyến mãi: TenKM, SoHoaDon, TongGiam
   Quy tắc  : TK-03 – số liệu tiền chỉ tính hóa đơn TrangThai = 1.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_BaoCaoCuoiNgay
    @Ngay DATE = NULL,
    @MaTK INT  = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Ngay IS NULL SET @Ngay = CAST(GETDATE() AS DATE);

    DECLARE @Tu  DATETIME = CAST(@Ngay AS DATETIME);
    DECLARE @Den DATETIME = DATEADD(DAY, 1, @Tu);

    -- (1) Tổng hợp
    SELECT @Ngay AS Ngay,
           CASE WHEN @MaTK IS NULL THEN N'Tất cả thu ngân'
                ELSE ISNULL((SELECT tk.HoTen FROM dbo.TaiKhoan AS tk WHERE tk.MaTK = @MaTK), N'')
           END AS TenThuNgan,
           ISNULL(SUM(CASE WHEN hd.TrangThai = 1 THEN 1 ELSE 0 END), 0)                          AS SoHoaDon,
           ISNULL(SUM(CASE WHEN hd.TrangThai = 2 THEN 1 ELSE 0 END), 0)                          AS SoHoaDonHuy,
           CAST(ISNULL(SUM(CASE WHEN hd.TrangThai = 1 THEN hd.TamTinh END), 0) AS DECIMAL(18,0))       AS TongTamTinh,
           CAST(ISNULL(SUM(CASE WHEN hd.TrangThai = 1 THEN hd.GiamGia END), 0) AS DECIMAL(18,0))       AS TongGiamGia,
           CAST(ISNULL(SUM(CASE WHEN hd.TrangThai = 1 THEN hd.TongThanhToan END), 0) AS DECIMAL(18,0)) AS DoanhThuThuc,
           CAST(ISNULL(SUM(CASE WHEN hd.TrangThai = 1 THEN hd.TongThanhToan END), 0) AS DECIMAL(18,0)) AS TienMatPhaiCo
    FROM dbo.HoaDon AS hd
    WHERE hd.NgayLap >= @Tu AND hd.NgayLap < @Den
      AND (@MaTK IS NULL OR hd.MaTK = @MaTK);

    -- (2) Hóa đơn đã hủy kèm lý do
    SELECT hd.MaHD, hd.NgayLap, hd.TongThanhToan, hd.LyDoHuy
    FROM dbo.HoaDon AS hd
    WHERE hd.TrangThai = 2
      AND hd.NgayLap >= @Tu AND hd.NgayLap < @Den
      AND (@MaTK IS NULL OR hd.MaTK = @MaTK)
    ORDER BY hd.NgayLap;

    -- (3) Giảm giá theo từng khuyến mãi
    SELECT km.TenKM,
           COUNT(*)                                AS SoHoaDon,
           CAST(SUM(hd.GiamGia) AS DECIMAL(18,0))  AS TongGiam
    FROM dbo.HoaDon AS hd
    INNER JOIN dbo.KhuyenMai AS km ON km.MaKM = hd.MaKM
    WHERE hd.TrangThai = 1
      AND hd.NgayLap >= @Tu AND hd.NgayLap < @Den
      AND (@MaTK IS NULL OR hd.MaTK = @MaTK)
    GROUP BY km.MaKM, km.TenKM
    ORDER BY TongGiam DESC;
END
GO

/* ---------------------------------------------------------------------
   sp_TraCuuHoaDon
   Mục đích : Tra cứu hóa đơn (xem lại, in lại, hủy).
   Tham số  : @MaHD – một phần mã HĐ (khi có giá trị thì BỎ QUA khoảng ngày để tìm được hóa đơn cũ);
              @TuKhoaKhach – tên hoặc SĐT khách; @TuNgay, @DenNgay;
              @MaTK – lọc thu ngân (thu ngân chỉ xem của mình); @TrangThai – 1/2 (NULL = tất cả).
   Kết quả  : MaHD, NgayLap, MaTK, TenThuNgan, MaKH, TenKhach (NULL → 'Khách lẻ'), SDT,
              TamTinh, GiamGia, TongThanhToan, MaKM, TenKM, TrangThai, LyDoHuy – mới nhất trước.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_TraCuuHoaDon
    @MaHD        VARCHAR(20)   = NULL,
    @TuKhoaKhach NVARCHAR(100) = NULL,
    @TuNgay      DATE,
    @DenNgay     DATE,
    @MaTK        INT           = NULL,
    @TrangThai   TINYINT       = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MauMa    VARCHAR(80)   = NULL;
    DECLARE @MauKhach NVARCHAR(400) = NULL;

    SET @MaHD        = LTRIM(RTRIM(@MaHD));
    SET @TuKhoaKhach = LTRIM(RTRIM(@TuKhoaKhach));

    IF @MaHD IS NOT NULL AND @MaHD <> ''
        SET @MauMa = '%' + REPLACE(REPLACE(REPLACE(@MaHD, '[', '[[]'), '%', '[%]'), '_', '[_]') + '%';

    IF @TuKhoaKhach IS NOT NULL AND @TuKhoaKhach <> N''
        SET @MauKhach = N'%' + REPLACE(REPLACE(REPLACE(@TuKhoaKhach, N'[', N'[[]'), N'%', N'[%]'), N'_', N'[_]') + N'%';

    SELECT hd.MaHD,
           hd.NgayLap,
           hd.MaTK,
           tk.HoTen                         AS TenThuNgan,
           hd.MaKH,
           ISNULL(kh.HoTen, N'Khách lẻ')    AS TenKhach,
           kh.SDT,
           hd.TamTinh,
           hd.GiamGia,
           hd.TongThanhToan,
           hd.MaKM,
           km.TenKM,
           hd.TrangThai,
           hd.LyDoHuy
    FROM dbo.HoaDon AS hd
    INNER JOIN dbo.TaiKhoan  AS tk ON tk.MaTK = hd.MaTK
    LEFT  JOIN dbo.KhachHang AS kh ON kh.MaKH = hd.MaKH
    LEFT  JOIN dbo.KhuyenMai AS km ON km.MaKM = hd.MaKM
    WHERE (@MauMa IS NOT NULL
           OR (hd.NgayLap >= @TuNgay AND hd.NgayLap < DATEADD(DAY, 1, @DenNgay)))
      AND (@MauMa IS NULL OR hd.MaHD LIKE @MauMa)
      AND (@MauKhach IS NULL
           OR kh.HoTen COLLATE Vietnamese_CI_AI LIKE @MauKhach
           OR kh.SDT LIKE @MauKhach)
      AND (@MaTK IS NULL OR hd.MaTK = @MaTK)
      AND (@TrangThai IS NULL OR hd.TrangThai = @TrangThai)
    ORDER BY hd.NgayLap DESC, hd.MaHD DESC
    OPTION (RECOMPILE);
END
GO

/* ---------------------------------------------------------------------
   sp_LayChiTietHoaDon
   Mục đích : Xem chi tiết và in lại hóa đơn.
   Tham số  : @MaHD.
   Kết quả  : 2 result set
              (1) Thông tin hóa đơn kèm TenThuNgan, TenKhach ('Khách lẻ' nếu NULL), SDT, TenKM
              (2) Dòng chi tiết: STT, MaSach, TenSach, SoLuong, DonGia, ThanhTien
   Quy tắc  : HD-04 – DonGia là giá lúc bán, không lấy giá hiện hành.
   --------------------------------------------------------------------- */
CREATE PROCEDURE dbo.sp_LayChiTietHoaDon
    @MaHD VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    -- (1) Thông tin chung
    SELECT hd.MaHD, hd.NgayLap, hd.MaTK, tk.HoTen AS TenThuNgan,
           hd.MaKH, ISNULL(kh.HoTen, N'Khách lẻ') AS TenKhach, kh.SDT,
           hd.TamTinh, hd.GiamGia, hd.MaKM, km.TenKM,
           hd.TongThanhToan, hd.TienKhachDua, hd.TienThoi,
           hd.TrangThai, hd.LyDoHuy, hd.GhiChu
    FROM dbo.HoaDon AS hd
    INNER JOIN dbo.TaiKhoan  AS tk ON tk.MaTK = hd.MaTK
    LEFT  JOIN dbo.KhachHang AS kh ON kh.MaKH = hd.MaKH
    LEFT  JOIN dbo.KhuyenMai AS km ON km.MaKM = hd.MaKM
    WHERE hd.MaHD = @MaHD;

    -- (2) Dòng chi tiết
    SELECT CAST(ROW_NUMBER() OVER (ORDER BY s.TenSach, ct.MaSach) AS INT) AS STT,
           ct.MaSach, s.TenSach, ct.SoLuong, ct.DonGia, ct.ThanhTien
    FROM dbo.ChiTietHoaDon AS ct
    INNER JOIN dbo.Sach AS s ON s.MaSach = ct.MaSach
    WHERE ct.MaHD = @MaHD
    ORDER BY STT;
END
GO

-- Kiểm tra đủ 18 SP rồi mới báo thành công
DECLARE @Thieu NVARCHAR(MAX) = NULL;
SELECT @Thieu = ISNULL(@Thieu + N', ', N'') + t.TenSP
FROM (VALUES
        (N'sp_DangNhap'),
        (N'sp_TimSach'),
        (N'sp_LaySachTheoMaHoacISBN'),
        (N'sp_TaoMaHoaDon'),
        (N'sp_LapHoaDon'),
        (N'sp_HuyHoaDon'),
        (N'sp_DoanhThuTheoNgay'),
        (N'sp_DoanhThuTheoThang'),
        (N'sp_SachBanChay'),
        (N'sp_SachSapHet'),
        (N'sp_DoanhThuTheoThuNgan'),
        (N'sp_HieuQuaKhuyenMai'),
        (N'sp_KhuyenMaiHieuLuc'),
        (N'sp_SachMuaKem'),
        (N'sp_TongQuanTrongNgay'),
        (N'sp_BaoCaoCuoiNgay'),
        (N'sp_TraCuuHoaDon'),
        (N'sp_LayChiTietHoaDon')
) AS t (TenSP)
WHERE OBJECT_ID(N'dbo.' + t.TenSP, N'P') IS NULL;

IF @Thieu IS NULL
    PRINT N'Tạo CSDL QuanLyBanSachDB thành công';
ELSE
    PRINT N'CHƯA THÀNH CÔNG – thiếu stored procedure: ' + @Thieu;
GO
