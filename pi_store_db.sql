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

CREATE TABLE [dbo].[UserInfo](
	[Username] VARCHAR(20),
	[Password] VARCHAR(20)
)
-- Add sample data for Client table
INSERT INTO [dbo].[Client] (ID, Name, Email, Phone, Address) VALUES 
('CL01', N'Nguyễn Văn A', 'vananguyen@gmail.com', '0945638338', N'123 Phạm Văn Đồng, Hà Nội'),
('CL02', N'Lê Thị B', 'lethib@example.com', '0987654321', N'456 Lê Duẩn, Đà Nẵng'),
('CL03', N'Trần Văn C', 'tranvc@gmail.com', '0945123456', N'789 Điện Biên Phủ, TP.HCM'),
('CL04', N'Phạm Minh D', 'phammd@gmail.com', '0912345678', N'234 Hai Bà Trưng, Huế'),
('CL05', N'Đỗ Thị E', 'dothe@gmail.com', '0934567890', N'567 Nguyễn Trãi, Cần Thơ');

-- Add sample data for Employee table
INSERT INTO [dbo].[Employee] (ID, Name, Email, Phone, Address, Salary, HireDate) VALUES 
('EP01', N'Nguyễn Tiến K', 'nguyentienk@gmail.com', '0923456781', N'123 Bạch Mai, Hà Nội', 15000000, '2022-01-15'),
('EP02', N'Trần Thị L', 'tranl@example.com', '0934567812', N'234 Trần Hưng Đạo, Đà Nẵng', 18000000, '2021-06-10'),
('EP03', N'Phạm Văn M', 'phammv@gmail.com', '0912345678', N'345 Lê Lợi, TP.HCM', 12000000, '2023-03-25'),
('EP04', N'Lê Thị N', 'lethine@gmail.com', '0976543210', N'456 Hai Bà Trưng, Huế', 20000000, '2020-11-01'),
('EP05', N'Nguyễn Văn P', 'nguyenp@gmail.com', '0956781234', N'678 Trần Phú, TP.HCM', 16000000, '2021-08-20');

-- Add sample data for Product table
INSERT INTO [dbo].[Product] (ID, Name, Description, Price, Quantity) VALUES 
('PD01', N'Nón Thời Trang A', N'Nón thời trang cao cấp', 2000000, 50),
('PD02', N'Nón Thời Trang B', N'Nón thời trang trẻ trung', 2500000, 40),
('PD03', N'Túi Xách C', N'Túi xách chính hãng', 5000000, 30),
('PD04', N'Túi Xách D', N'Túi xách cao cấp', 7000000, 20),
('PD05', N'Áo Thời Trang E', N'Áo thời trang nữ', 1000000, 100),
('PD06', N'Áo Thời Trang F', N'Áo thời trang nam', 1500000, 60),
('PD07', N'Quần Jean G', N'Quần jean nữ', 3000000, 80),
('PD08', N'Quần Jean H', N'Quần jean nam', 3500000, 75),
('PD09', N'Giày Thể Thao I', N'Giày thể thao nữ', 4500000, 60),
('PD10', N'Giày Thể Thao J', N'Giày thể thao nam', 5000000, 45);

-- Add sample data for Order table
INSERT INTO [dbo].[Order] (ID, ClientID, EmployeeID, OrderDate, TotalPrice) VALUES 
('OD01', 'CL01', 'EP01', '2023-08-15', 10000000),
('OD02', 'CL02', 'EP02', '2023-08-20', 8000000),
('OD03', 'CL03', 'EP03', '2023-08-25', 7000000),
('OD04', 'CL04', 'EP04', '2023-09-01', 12000000),
('OD05', 'CL05', 'EP05', '2023-09-05', 9000000);

-- Add sample data for OrderItem table
INSERT INTO [dbo].[OrderItem] (ID, OrderID, ProductID, Quantity) VALUES 
('OI01', 'OD01', 'PD01', 3),   -- 3 * 2000000 = 6000000
('OI02', 'OD01', 'PD03', 1),   -- 1 * 5000000 = 5000000
('OI03', 'OD02', 'PD02', 2),   -- 2 * 2500000 = 5000000
('OI04', 'OD02', 'PD05', 3),   -- 3 * 1000000 = 3000000
('OI05', 'OD03', 'PD04', 1),   -- 1 * 7000000 = 7000000
('OI06', 'OD04', 'PD07', 2),   -- 2 * 3000000 = 6000000
('OI07', 'OD04', 'PD09', 1),   -- 1 * 4500000 = 4500000
('OI08', 'OD05', 'PD06', 4),   -- 4 * 1500000 = 6000000
('OI09', 'OD05', 'PD10', 1),   -- 1 * 5000000 = 5000000
('OI10', 'OD05', 'PD08', 1);   -- 1 * 3500000 = 3500000 


INSERT INTO  UserInfo(Username, Password)
VALUES 
('user1', '123456'),
('user2', '123456');

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
