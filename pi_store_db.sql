create database PiStore
USE PiStore
GO

-- Table: Client
CREATE TABLE [dbo].[Client](
    [ID] NVARCHAR(10) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL UNIQUE,
    [Phone] NVARCHAR(20) NOT NULL,
    [Address] NVARCHAR(200) NULL
);
GO

-- Table: Employee
CREATE TABLE [dbo].[Employee](
    [ID] NVARCHAR(10) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL UNIQUE,
    [Phone] NVARCHAR(20) NOT NULL,
    [Address] NVARCHAR(200) NULL,
    [Salary] DECIMAL(18, 2) NOT NULL,
    [HireDate] DATE NOT NULL
);
GO

-- Table: Order
CREATE TABLE [dbo].[Order](
    [ID] NVARCHAR(10) NOT NULL PRIMARY KEY,
    [ClientID] NVARCHAR(10) NULL,
    [EmployeeID] NVARCHAR(10) NULL,
    [OrderDate] DATETIME NOT NULL,
    [TotalPrice] DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ClientID) REFERENCES [dbo].[Client](ID),
    FOREIGN KEY (EmployeeID) REFERENCES [dbo].[Employee](ID)
);
GO

-- Table: Product
CREATE TABLE [dbo].[Product](
    [ID] NVARCHAR(10) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    [Price] DECIMAL(18, 2) NOT NULL,
    [Quantity] INT NOT NULL
);
GO

-- Table: OrderItem
CREATE TABLE [dbo].[OrderItem](
    [ID] NVARCHAR(10) NOT NULL PRIMARY KEY,
    [OrderID] NVARCHAR(10) NULL,
    [ProductID] NVARCHAR(10) NULL,
    [Quantity] INT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES [dbo].[Order](ID),
    FOREIGN KEY (ProductID) REFERENCES [dbo].[Product](ID)
);
GO
-- Thêm dữ liệu mẫu vào bảng Employee
INSERT INTO Employee (ID, Name, Email, Phone, Address, Salary, HireDate)
VALUES 
('EP1001', N'Nguyễn Văn A', 'nguyenvana@example.com', '0123456789', N'123 Nguyễn Trãi, Hà Nội', 15000000, '2023-01-15'),
('EP1002', N'Trần Thị B', 'tranthib@example.com', '0987654321', N'456 Lê Lợi, Đà Nẵng', 12000000, '2023-02-20'),
('EP1003', N'Lê Văn C', 'levanc@example.com', '0912345678', N'789 Trường Sa, HCM', 17000000, '2022-12-01'),
('EP1004', N'Phạm Thị D', 'phamthid@example.com', '0908765432', N'12 Hai Bà Trưng, Huế', 13000000, '2023-03-05'),
('EP1005', N'Hoàng Văn E', 'hoangvane@example.com', '0934567890', N'34 Võ Văn Kiệt, Cần Thơ', 11000000, '2023-04-12');

-- Dữ liệu mẫu cho bảng Client
INSERT INTO Client (ID, Name, Email, Phone, Address)
VALUES 
('CL1001', N'Nguyễn Thị F', 'nguyenthif@example.com', '0932123456', N'123 Phan Đình Phùng, Hà Nội'),
('CL1002', N'Trần Văn G', 'tranvang@example.com', '0943123456', N'56 Phạm Văn Đồng, Đà Nẵng'),
('CL1003', N'Lê Thị H', 'lethih@example.com', '0911223344', N'78 Lý Tự Trọng, HCM'),
('CL1004', N'Nguyễn Văn I', 'nguyenvani@example.com', '0988112233', N'34 Tôn Đức Thắng, Huế'),
('CL1005', N'Bùi Thị K', 'buithik@example.com', '0922334455', N'45 Nguyễn Thị Minh Khai, Cần Thơ');

-- Dữ liệu mẫu cho bảng Product
INSERT INTO Product (ID, Name, Description, Price, Quantity)
VALUES 
('PD1001', N'Laptop Dell XPS 13', N'Ultrabook cao cấp, màn hình 13 inch', 15000000, 50),
('PD1002', N'iPhone 14 Pro', N'Diện thoại thông minh cao cấp của Apple', 12000000, 30),
('PD1003', N'Samsung Galaxy Tab S8', N'Máy tính bảng Android với màn hình 11 inch', 8000000, 40),
('PD1004', N'AirPods Pro', N'Tai nghe không dây chống ồn của Apple', 2500000, 100),
('PD1005', N'Apple Watch Series 8', N'Đồng hồ thông minh với cảm biến sức khỏe', 5000000, 60),
('PD1006', N'Lenovo ThinkPad X1 Carbon', N'Laptop siêu nhẹ, bền bỉ cho doanh nhân', 18000000, 25),
('PD1007', N'Sony WH-1000XM4', N'Tai nghe không dây chống ồn hàng đầu', 350000, 70),
('PD1008', N'Google Pixel 6', N'Diện thoại thông minh với camera chất lượng cao', 4500000, 20),
('PD1009', N'ASUS ROG Zephyrus G14', N'Máy tính xách tay chơi game mạnh mẽ', 23000000, 15),
('PD1010', N'Kindle Paperwhite', N'Máy đọc sách điện tử với màn hình chống lóa', 3400000, 90);



-- Dữ liệu mẫu cho bảng Order
INSERT INTO [Order] (ID, ClientID, EmployeeID, OrderDate, TotalPrice)
VALUES 
('OD1001', 'CL1001', 'EP1001', '2024-09-25 10:30:00', 9000000),
('OD1002', 'CL1002', 'EP1002', '2024-09-26 15:45:00', 6000000),
('OD1003', 'CL1003', 'EP1003', '2024-09-27 09:20:00', 5000000),
('OD1004', 'CL1004', 'EP1004', '2024-09-28 11:00:00', 7500000),
('OD1005', 'CL1005', 'EP1005', '2024-09-29 14:50:00', 4000000);

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


-- Create Stored Procedure

CREATE PROCEDURE sp_AddClient
    @Name NVARCHAR(100),
    @Email NVARCHAR(100), 
    @Phone NVARCHAR(20),
    @Address NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @NewID NVARCHAR(10)
        
        BEGIN TRANSACTION
            -- Tạo ID mới với format CLxxxx
            SELECT @NewID = 'CL' + RIGHT('0000' + 
                CAST(COALESCE(MAX(CAST(SUBSTRING(ID, 3, 4) AS INT)), 0) + 1 AS NVARCHAR(4)), 4)
            FROM Client WITH (TABLOCKX);

            INSERT INTO Client (ID, Name, Email, Phone, Address)
            VALUES (@NewID, @Name, @Email, @Phone, @Address);
        COMMIT TRANSACTION

        SELECT @NewID AS NewClientID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

GO

CREATE PROCEDURE sp_AddEmployee
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(200),
    @Salary DECIMAL(18,2),
    @HireDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION
            DECLARE @NewID NVARCHAR(10)
            SELECT @NewID = 'EP' + RIGHT('0000' + 
                CAST(COALESCE(MAX(CAST(SUBSTRING(ID, 3, 4) AS INT)), 0) + 1 AS NVARCHAR(4)), 4)
            FROM Employee WITH (TABLOCKX);

            INSERT INTO Employee (ID, Name, Email, Phone, Address, Salary, HireDate)
            VALUES (@NewID, @Name, @Email, @Phone, @Address, @Salary, @HireDate);
        COMMIT TRANSACTION
        SELECT @NewID AS NewEmployeeID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_AddProduct
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Price DECIMAL(18,2),
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION
            DECLARE @NewID NVARCHAR(10)
            SELECT @NewID = 'PD' + RIGHT('0000' + 
                CAST(COALESCE(MAX(CAST(SUBSTRING(ID, 3, 4) AS INT)), 0) + 1 AS NVARCHAR(4)), 4)
            FROM Product WITH (TABLOCKX);

            INSERT INTO Product (ID, Name, Description, Price, Quantity)
            VALUES (@NewID, @Name, @Description, @Price, @Quantity);
        COMMIT TRANSACTION
        SELECT @NewID AS NewProductID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_AddOrder
    @ClientID NVARCHAR(10),
    @EmployeeID NVARCHAR(10),
    @OrderDate DATETIME,
    @TotalPrice DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION
            DECLARE @NewID NVARCHAR(10)
            SELECT @NewID = 'OD' + RIGHT('0000' + 
                CAST(COALESCE(MAX(CAST(SUBSTRING(ID, 3, 4) AS INT)), 0) + 1 AS NVARCHAR(4)), 4)
            FROM [Order] WITH (TABLOCKX);

            INSERT INTO [Order] (ID, ClientID, EmployeeID, OrderDate, TotalPrice)
            VALUES (@NewID, @ClientID, @EmployeeID, @OrderDate, @TotalPrice);
        COMMIT TRANSACTION
        SELECT @NewID AS NewOrderID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_AddOrderItem
    @OrderID NVARCHAR(10),
    @ProductID NVARCHAR(10),
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION
            DECLARE @NewID NVARCHAR(10)
            SELECT @NewID = 'OI' + RIGHT('0000' + 
                CAST(COALESCE(MAX(CAST(SUBSTRING(ID, 3, 4) AS INT)), 0) + 1 AS NVARCHAR(4)), 4)
            FROM OrderItem WITH (TABLOCKX);

            INSERT INTO OrderItem (ID, OrderID, ProductID, Quantity)
            VALUES (@NewID, @OrderID, @ProductID, @Quantity);
        COMMIT TRANSACTION
        SELECT @NewID AS NewOrderItemID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
