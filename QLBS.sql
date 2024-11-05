create database QLBS1
go

use QLBS1
go

create table KHACHHANG
(
	MaKH int primary key not null,
	HoTen nvarchar(50) not null,
	Email varchar(70),
	DiaChiKH nvarchar(100),
	DienThoaiKH varchar(30),
	NgaySinh datetime,
	TaiKhoan varchar(50) unique,
	MatKhau varchar(50) not null
)
go


create table CHUDE
(
	MaChuDe int primary key not null,
	TenChuDe nvarchar(50) not null
)

insert into CHUDE values('1', N'Văn học'),
						('2', N'Kinh tế'),
						('3', N'Sách thiếu nhi'),
						('4', N'Kĩ năng sống'),
						('5', N'Nuôi dạy con'),
						('6', N'Tiểu sử hồi ký'),
						('7', N'Sách giáo khoa'),
						('8', N'Sách học ngoại ngữ');

select * from CHUDE
				
go

create table SACH
(
	MaSach int primary key not null,
	TenSach nvarchar(100) not null,
	MoTa nvarchar(500),
	GiaSach decimal(18,0),
	AnhBiaSach varchar(50),
	SoLuongTon int,
	NgayCapNhat datetime,
	MaChuDe int,
	constraint FK_SACH_CHUDE foreign key(MaChuDe) references CHUDE(MaChuDe)
)
go

insert into SACH values ('1', N'Nhà Giả Kim', N'abcxyz', '79000', 'product1.jpg', '10', '2023-11-24', '1'),
						('2', N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', N'abcxyz', '150000', 'product2.jpg', '10', '2023-11-24', '1'),
						('3', N'Tắt Đèn', N'abcxyz', '50000', 'product3.jpg', '10', '2023-11-24', '1'),
						('4', N'Mắt Biếc', N'abcxyz', '43000', 'product4.jpg', '10', '2023-11-25', '1'),
						('5', N'Lạc Giữa Tần Số Không Người Nghe', N'abcxyz', '86000', 'product5.jpg', '10', '2023-11-25', '2'),
						('6', N'Cấm Thư Ma Thuật Index - Tập 1', N'abcxyz', '105000', 'product6.jpg', '10', '2023-11-25', '3');

select * from SACH

create table TACGIA
(
	MaTG int primary key not null,
	TenTG nvarchar(50) not null,
	DiaChiTG nvarchar(100),
	TieuSuTG nvarchar(500),
	DienThoaiTG varchar(30),
)
go
create table VIETSACH
(
	MaTG int,
	MaSach int,
	VaiTro nvarchar(50),
	ViTri nvarchar(50),
	constraint FK_VIETSACH_TACGIA foreign key(MaTG) references TACGIA(MaTG),
	constraint FK_VIETSACH_SACH foreign key(MaSach) references SACH(MaSach)
)
go
create table DONDATHANG
(
	MaDonHang int primary key not null,
	ThanhToan bit,
	TinhTrangGiaoHang bit,
	NgayDat datetime,
	NgayGiao datetime,
	MaKH int,
	constraint FK_DONDATHANG_KHACHHANG foreign key(MaKH) references KHACHHANG(MaKH)
)
go
create table CHITIETDATHANG
(
	MaDonHang int not null,
	MaSach int not null,
	SoLuong int,
	DonGia decimal(18,0),
	constraint PK_CHITIETDATHANG primary key(MaDonHang,MaSach),
	constraint FK_CHITIETDATHANG_DONHANG foreign key(MaDonHang) references DONDATHANG(MaDonHang),
	constraint FK_CHITIETDATHANG_SACH foreign key(MaSach) references SACH(MaSach)
)
go

create table ADMIN
(
	UserAdmin varchar(50) primary key not null,
	PassAdmin varchar(50) not null
)

insert into ADMIN values ('nhdkhang', 'nhdkhang');
