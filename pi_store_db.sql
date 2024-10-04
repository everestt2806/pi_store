-- Tạo cơ sở dữ liệu PiStore
CREATE DATABASE PiStore;
GO

-- Sử dụng cơ sở dữ liệu PiStore
USE PiStore;
GO

-- Tạo bảng Employee với mã ID theo định dạng EP01, EP02,...
CREATE TABLE Employee (
    ID NVARCHAR(10) PRIMARY KEY,               -- Khóa chính dạng chuỗi (ví dụ: EP01)
    Name NVARCHAR(100) NOT NULL,               -- Tên nhân viên
    Email NVARCHAR(100) UNIQUE NOT NULL,       -- Email duy nhất
    Phone NVARCHAR(20) NOT NULL,               -- Số điện thoại
    Address NVARCHAR(200),                     -- Địa chỉ
    Salary DECIMAL(18, 2) NOT NULL,            -- Lương
    HireDate DATE NOT NULL                     -- Ngày thuê
);
GO

-- Tạo bảng Client với mã ID theo định dạng CL01, CL02,...
CREATE TABLE Client (
    ID NVARCHAR(10) PRIMARY KEY,               -- Khóa chính dạng chuỗi (ví dụ: CL01)
    Name NVARCHAR(100) NOT NULL,               -- Tên khách hàng
    Email NVARCHAR(100) UNIQUE NOT NULL,       -- Email duy nhất
    Phone NVARCHAR(20) NOT NULL,               -- Số điện thoại
    Address NVARCHAR(200)                      -- Địa chỉ
);
GO

-- Tạo bảng Product với mã ID theo định dạng PD01, PD02,...
CREATE TABLE Product (
    ID NVARCHAR(10) PRIMARY KEY,               -- Khóa chính dạng chuỗi (ví dụ: PD01)
    Name NVARCHAR(100) NOT NULL,               -- Tên sản phẩm
    Description NVARCHAR(255),                 -- Mô tả sản phẩm
    Price DECIMAL(18, 2) NOT NULL,             -- Giá sản phẩm
    Quantity INT NOT NULL                      -- Số lượng tồn kho
);
GO

-- Tạo bảng Order với mã ID theo định dạng OD01, OD02,...
CREATE TABLE [Order] (
    ID NVARCHAR(10) PRIMARY KEY,               -- Khóa chính dạng chuỗi (ví dụ: OD01)
    ClientID NVARCHAR(10) FOREIGN KEY REFERENCES Client(ID),  -- Khóa ngoại tham chiếu bảng Client
    EmployeeID NVARCHAR(10) FOREIGN KEY REFERENCES Employee(ID),  -- Khóa ngoại tham chiếu bảng Employee
    OrderDate DATETIME NOT NULL,               -- Ngày đặt hàng
    TotalPrice DECIMAL(18, 2) NOT NULL         -- Tổng giá trị đơn hàng
);
GO

-- Tạo bảng OrderItem với mã ID theo định dạng OI01, OI02,...
CREATE TABLE OrderItem (
    ID NVARCHAR(10) PRIMARY KEY,               -- Khóa chính dạng chuỗi (ví dụ: OI01)
    OrderID NVARCHAR(10) FOREIGN KEY REFERENCES [Order](ID),  -- Khóa ngoại tham chiếu bảng Order
    ProductID NVARCHAR(10) FOREIGN KEY REFERENCES Product(ID),  -- Khóa ngoại tham chiếu bảng Product
    Quantity INT NOT NULL                      -- Số lượng sản phẩm trong đơn hàng
);
GO

-- Tạo bảng Bill với mã ID theo định dạng BL01, BL02,...
CREATE TABLE Bill (
    ID NVARCHAR(10) PRIMARY KEY,               -- Khóa chính dạng chuỗi (ví dụ: BL01)
    OrderID NVARCHAR(10) FOREIGN KEY REFERENCES [Order](ID),  -- Khóa ngoại tham chiếu bảng Order
    ClientID NVARCHAR(10) FOREIGN KEY REFERENCES Client(ID),  -- Khóa ngoại tham chiếu bảng Client
    EmployeeID NVARCHAR(10) FOREIGN KEY REFERENCES Employee(ID),  -- Khóa ngoại tham chiếu bảng Employee
    BillDate DATETIME NOT NULL,                -- Ngày tạo hóa đơn
    TotalPrice DECIMAL(18, 2) NOT NULL         -- Tổng giá trị hóa đơn
);
GO


-- Thêm dữ liệu mẫu vào bảng Employee
INSERT INTO Employee (ID, Name, Email, Phone, Address, Salary, HireDate)
VALUES 
('EP1001', 'Nguyen Van A', 'nguyenvana@example.com', '0123456789', '123 Nguyen Trai, Ha Noi', 15000.00, '2023-01-15'),
('EP1002', 'Tran Thi B', 'tranthib@example.com', '0987654321', '456 Le Loi, Da Nang', 12000.00, '2023-02-20'),
('EP1003', 'Le Van C', 'levanc@example.com', '0912345678', '789 Truong Sa, HCM', 17000.00, '2022-12-01'),
('EP1004', 'Pham Thi D', 'phamthid@example.com', '0908765432', '12 Hai Ba Trung, Hue', 13000.00, '2023-03-05'),
('EP1005', 'Hoang Van E', 'hoangvane@example.com', '0934567890', '34 Vo Van Kiet, Can Tho', 11000.00, '2023-04-12');

-- Dữ liệu mẫu cho bảng Client
INSERT INTO Client (ID, Name, Email, Phone, Address)
VALUES 
('CL1001', 'Nguyen Thi F', 'nguyenthif@example.com', '0932123456', '123 Phan Dinh Phung, Ha Noi'),
('CL1002', 'Tran Van G', 'tranvang@example.com', '0943123456', '56 Pham Van Dong, Da Nang'),
('CL1003', 'Le Thi H', 'lethih@example.com', '0911223344', '78 Ly Tu Trong, HCM'),
('CL1004', 'Nguyen Van I', 'nguyenvani@example.com', '0988112233', '34 Ton Duc Thang, Hue'),
('CL1005', 'Bui Thi K', 'buithik@example.com', '0922334455', '45 Nguyen Thi Minh Khai, Can Tho');

-- Dữ liệu mẫu cho bảng Product
INSERT INTO Product (ID, Name, Description, Price, Quantity)
VALUES 
('PD1001', 'Banh Mi', 'Banh mi que', 10.00, 100),
('PD1002', 'Pho Bo', 'Pho bo Ha Noi', 50.00, 50),
('PD1003', 'Ca Phe Sua', 'Ca phe sua da', 20.00, 200),
('PD1004', 'Tra Chanh', 'Tra chanh tuoi', 15.00, 150),
('PD1005', 'Nuoc Cam', 'Nuoc cam tuoi', 25.00, 120);

-- Dữ liệu mẫu cho bảng Order
INSERT INTO [Order] (ID, ClientID, EmployeeID, OrderDate, TotalPrice)
VALUES 
('OD1001', 'CL1001', 'EP1001', '2024-09-25 10:30:00', 90.00),
('OD1002', 'CL1002', 'EP1002', '2024-09-26 15:45:00', 60.00),
('OD1003', 'CL1003', 'EP1003', '2024-09-27 09:20:00', 50.00),
('OD1004', 'CL1004', 'EP1004', '2024-09-28 11:00:00', 75.00),
('OD1005', 'CL1005', 'EP1005', '2024-09-29 14:50:00', 40.00);

-- Dữ liệu mẫu cho bảng OrderItem
INSERT INTO OrderItem (ID, OrderID, ProductID, Quantity)
VALUES 
('OI1001', 'OD1001', 'PD1001', 2),
('OI1002', 'OD1001', 'PD1003', 3),
('OI1003', 'OD1002', 'PD1002', 1),
('OI1004', 'OD1002', 'PD1004', 2),
('OI1005', 'OD1003', 'PD1005', 1),
('OI1006', 'OD1003', 'PD1003', 2),
('OI1007', 'OD1004', 'PD1001', 3),
('OI1008', 'OD1004', 'PD1002', 1),
('OI1009', 'OD1005', 'PD1004', 2),
('OI1010', 'OD1005', 'PD1005', 1);

-- Dữ liệu mẫu cho bảng Bill
INSERT INTO Bill (ID, OrderID, ClientID, EmployeeID, BillDate, TotalPrice)
VALUES 
('BL1001', 'OD1001', 'CL1001', 'EP1001', '2024-09-25 10:45:00', 90.00),
('BL1002', 'OD1002', 'CL1002', 'EP1002', '2024-09-26 16:00:00', 60.00),
('BL1003', 'OD1003', 'CL1003', 'EP1003', '2024-09-27 09:40:00', 50.00),
('BL1004', 'OD1004', 'CL1004', 'EP1004', '2024-09-28 11:20:00', 75.00),
('BL1005', 'OD1005', 'CL1005', 'EP1005', '2024-09-29 15:10:00', 40.00);

select * from Client

