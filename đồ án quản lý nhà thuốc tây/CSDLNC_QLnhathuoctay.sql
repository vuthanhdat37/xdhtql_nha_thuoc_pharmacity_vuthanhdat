
-- ===================== Bảng Cửa hàng =====================
CREATE TABLE CuaHang (
    MaCH_ID INT PRIMARY KEY,
    TenCH NVARCHAR(100),
    DiaChi NVARCHAR(200),
    SDT VARCHAR(15),
    TieuChuanGPP NVARCHAR(100),
    CoSoVatChat NVARCHAR(200),
    HoatDongNhaThuoc NVARCHAR(200)
);

INSERT INTO CuaHang VALUES (1, N'Nhà thuốc Minh Tâm', N'123 Lê Lợi, Q.1, TP.HCM', '0909123456', N'GPP-001', N'Cơ sở đạt chuẩn GPP', N'Đang hoạt động');
INSERT INTO CuaHang VALUES (2, N'Nhà thuốc Phúc An', N'45 Hai Bà Trưng, Q.3, TP.HCM', '0911223344', N'GPP-002', N'Đầy đủ trang thiết bị', N'Đang hoạt động');

-- ===================== Bảng Tiêu chuẩn =====================
CREATE TABLE TieuChuan (
    MaTC_ID INT PRIMARY KEY,
    TenTC NVARCHAR(100),
    MoTa NVARCHAR(200),
    LoaiTC NVARCHAR(50),
    NgayDanhGia DATE
);

INSERT INTO TieuChuan VALUES (1, N'Tiêu chuẩn GPP', N'Tiêu chuẩn thực hành tốt nhà thuốc', N'GPP', '2024-01-01');
INSERT INTO TieuChuan VALUES (2, N'Tiêu chuẩn GDP', N'Tiêu chuẩn phân phối thuốc tốt', N'GDP', '2023-12-01');

-- ===================== Bảng CTTC =====================
CREATE TABLE CTTC (
    MaTC_ID INT,
    MaCH_ID INT,
    TrangThai NVARCHAR(50),
    PRIMARY KEY (MaTC_ID, MaCH_ID),
    FOREIGN KEY (MaTC_ID) REFERENCES TieuChuan(MaTC_ID),
    FOREIGN KEY (MaCH_ID) REFERENCES CuaHang(MaCH_ID)
);

INSERT INTO CTTC VALUES (1, 1, N'Đạt');
INSERT INTO CTTC VALUES (2, 2, N'Đang đánh giá');

-- ===================== Bảng Nhân viên =====================
CREATE TABLE NhanVien (
    MaNV_ID INT PRIMARY KEY,
    TenNV NVARCHAR(100),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    SDT VARCHAR(15),
    DiaChi NVARCHAR(200),
    MaCH_ID INT,
    FOREIGN KEY (MaCH_ID) REFERENCES CuaHang(MaCH_ID)
);

INSERT INTO NhanVien VALUES (1, N'Nguyễn Văn A', N'Nam', '1990-01-15', '0909345678', N'25 Pasteur, Q.1', 1);
INSERT INTO NhanVien VALUES (2, N'Trần Thị B', N'Nữ', '1992-05-20', '0912555444', N'78 Lê Duẩn, Q.1', 2);

-- ===================== Bảng Nhà cung cấp =====================
CREATE TABLE NhaCungCap (
    MaNCC_ID INT PRIMARY KEY,
    TenNCC NVARCHAR(100),
    DiaChi NVARCHAR(200),
    SDT VARCHAR(15),
    Email NVARCHAR(100)
);

INSERT INTO NhaCungCap VALUES (1, N'Dược Hậu Giang', N'Cần Thơ', '02923888888', 'contact@dhgpharma.com.vn');
INSERT INTO NhaCungCap VALUES (2, N'Traphaco', N'Hà Nội', '02438431397', 'info@traphaco.com.vn');
INSERT INTO NhaCungCap VALUES (3, N'Phúc An Khang Pharmacy', N'TP.HCM', '02839293456', 'support@phucankhang.vn');

-- ===================== Bảng Loại thuốc =====================
CREATE TABLE LoaiThuoc (
    MaLoaiThuoc_ID INT PRIMARY KEY,
    TenLoaiThuoc NVARCHAR(100),
    MaNCC_ID INT,
    FOREIGN KEY (MaNCC_ID) REFERENCES NhaCungCap(MaNCC_ID)
);

INSERT INTO LoaiThuoc VALUES (1, N'Thuốc giảm đau', 1);
INSERT INTO LoaiThuoc VALUES (2, N'Kháng sinh', 2);
INSERT INTO LoaiThuoc VALUES (3, N'Thuốc cảm cúm', 3);

-- ===================== Bảng Thuốc =====================
CREATE TABLE Thuoc (
    MaThuoc_ID INT PRIMARY KEY IDENTITY(1,1),
    TenThuoc NVARCHAR(100),
    DVT NVARCHAR(20),
    GiaNhap DECIMAL(10, 2),
    GiaBan DECIMAL(10, 2),
    SLTonKho INT,
    MaLoaiThuoc_ID INT,
    NgayHetHan DATE,
    FOREIGN KEY (MaLoaiThuoc_ID) REFERENCES LoaiThuoc(MaLoaiThuoc_ID)
);

SET IDENTITY_INSERT Thuoc ON;

INSERT INTO Thuoc (MaThuoc_ID, TenThuoc, DVT, GiaNhap, GiaBan, SLTonKho, MaLoaiThuoc_ID, NgayHetHan)
VALUES 
(1, N'Paracetamol 500mg', N'viên', 1500, 3000, 1000, 1, '2026-12-31'),
(2, N'Amoxicillin 500mg', N'viên', 2000, 4500, 800, 2, '2025-11-30'),
(3, N'Decolgen Forte', N'viên', 2500, 5000, 600, 3, '2025-08-15');

SET IDENTITY_INSERT Thuoc OFF;


-- ===================== Bảng Khách hàng =====================
CREATE TABLE KhachHang (
    MaKH_ID INT PRIMARY KEY,
    TenKH NVARCHAR(100),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    SDT VARCHAR(15),
    BenhLyNen NVARCHAR(200)
);

INSERT INTO KhachHang VALUES (1, N'Lê Quốc Dũng', N'Nam', '1985-03-12', '0988123456', N'Tăng huyết áp');
INSERT INTO KhachHang VALUES (2, N'Nguyễn Thị Mai', N'Nữ', '1993-07-24', '0939456123', N'Tiểu đường');
INSERT INTO KhachHang VALUES (3, N'Phạm Văn Minh', N'Nam', '1978-10-05', '0977223344', N'Đau dạ dày');

-- ===================== Bảng Phiếu mua hàng =====================
CREATE TABLE PhieuMuaHang (
    MaPMH_ID INT PRIMARY KEY,
    NgayMuaHang DATE,
    TrangThai NVARCHAR(50),
    PTThanhToan NVARCHAR(50),
    TongTien DECIMAL(10,2),
    MaKH_ID INT,
    FOREIGN KEY (MaKH_ID) REFERENCES KhachHang(MaKH_ID)
);

INSERT INTO PhieuMuaHang VALUES (1, '2025-06-10', N'Đã thanh toán', N'Tiền mặt', 60000, 1);
INSERT INTO PhieuMuaHang VALUES (2, '2025-06-11', N'Chờ thanh toán', N'Chuyển khoản', 90000, 2);

-- ===================== Bảng CTPMH =====================
CREATE TABLE CTPMH (
    MaPMH_ID INT,
    MaThuoc_ID INT,
    SLThuocMua INT,
    PRIMARY KEY (MaPMH_ID, MaThuoc_ID),
    FOREIGN KEY (MaPMH_ID) REFERENCES PhieuMuaHang(MaPMH_ID),
    FOREIGN KEY (MaThuoc_ID) REFERENCES Thuoc(MaThuoc_ID)
);

INSERT INTO CTPMH VALUES (1, 1, 10);
INSERT INTO CTPMH VALUES (2, 2, 20);

-- ===================== Bảng Hóa đơn =====================
CREATE TABLE HoaDon (
    MaHD_ID INT PRIMARY KEY,
    TenKH NVARCHAR(100),
    NgayLap DATE,
    TongTien DECIMAL(10,2),
    ThuocMua NVARCHAR(200),
    MaNV_ID INT,
    MaPMH_ID INT,
    FOREIGN KEY (MaNV_ID) REFERENCES NhanVien(MaNV_ID),
    FOREIGN KEY (MaPMH_ID) REFERENCES PhieuMuaHang(MaPMH_ID)
);

INSERT INTO HoaDon VALUES (1, N'Lê Quốc Dũng', '2025-06-10', 60000, N'Paracetamol 500mg x10', 1, 1);
INSERT INTO HoaDon VALUES (2, N'Nguyễn Thị Mai', '2025-06-11', 90000, N'Amoxicillin 500mg x20', 2, 2);

-- ===================== Bảng CTHD =====================
CREATE TABLE CTHD (
    MaHD_ID INT,
    MaThuoc_ID INT,
    SLThuoc INT,
    TongTien DECIMAL(10,2),
    PRIMARY KEY (MaHD_ID, MaThuoc_ID),
    FOREIGN KEY (MaHD_ID) REFERENCES HoaDon(MaHD_ID),
    FOREIGN KEY (MaThuoc_ID) REFERENCES Thuoc(MaThuoc_ID)
);

INSERT INTO CTHD VALUES (1, 1, 10, 60000);
INSERT INTO CTHD VALUES (2, 2, 20, 90000);

-- ===================== Bảng Đơn đặt hàng =====================
CREATE TABLE DonDatHang (
    MaDDH_ID INT PRIMARY KEY,
    TenThuoc NVARCHAR(100),
    NgayLap DATE,
    SoLuong INT,
    MaNV_ID INT,
    MaNCC_ID INT,
    FOREIGN KEY (MaNV_ID) REFERENCES NhanVien(MaNV_ID),
    FOREIGN KEY (MaNCC_ID) REFERENCES NhaCungCap(MaNCC_ID)
);

INSERT INTO DonDatHang VALUES (1, N'Paracetamol 500mg', '2025-06-01', 500, 1, 1);
INSERT INTO DonDatHang VALUES (2, N'Amoxicillin 500mg', '2025-06-03', 600, 2, 2);

-- ===================== Bảng CTDDH =====================
CREATE TABLE CTDDH (
    MaDDH_ID INT,
    MaThuoc_ID INT,
    SLDat INT,
    PRIMARY KEY (MaDDH_ID, MaThuoc_ID),
    FOREIGN KEY (MaDDH_ID) REFERENCES DonDatHang(MaDDH_ID),
    FOREIGN KEY (MaThuoc_ID) REFERENCES Thuoc(MaThuoc_ID)
);

INSERT INTO CTDDH VALUES (1, 1, 500);
INSERT INTO CTDDH VALUES (2, 2, 600);

-- ===================== Bảng Phiếu giao hàng =====================
CREATE TABLE PhieuGiaoHang (
    MaPGH_ID INT PRIMARY KEY,
    NgayGiaoHang DATE,
    TrangThaiGiaoHang NVARCHAR(50),
    MaDDH_ID INT,
    FOREIGN KEY (MaDDH_ID) REFERENCES DonDatHang(MaDDH_ID)
);

INSERT INTO PhieuGiaoHang VALUES (1, '2025-06-05', N'Hoàn tất', 1);
INSERT INTO PhieuGiaoHang VALUES (2, '2025-06-07', N'Đang giao', 2);

-- ===================== Bảng CTPGH =====================
CREATE TABLE CTPGH (
    MaPGH_ID INT,
    MaThuoc_ID INT,
    SLGiao INT,
    PRIMARY KEY (MaPGH_ID, MaThuoc_ID),
    FOREIGN KEY (MaPGH_ID) REFERENCES PhieuGiaoHang(MaPGH_ID),
    FOREIGN KEY (MaThuoc_ID) REFERENCES Thuoc(MaThuoc_ID)
);

INSERT INTO CTPGH VALUES (1, 1, 500);
INSERT INTO CTPGH VALUES (2, 2, 600);

SELECT *FROM thuoc ;
SELECT *FROM CTHD;
SELECT *FROM CTPGH;
SELECT *FROM CTPMH;
SELECT *FROM CTTC ;
SELECT *FROM CuaHang;
SELECT *FROM TieuChuan;


