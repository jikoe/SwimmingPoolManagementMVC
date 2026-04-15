-- =============================================
-- DATABASE
-- =============================================
CREATE DATABASE SwimmingPoolManagement_Final;
GO
USE SwimmingPoolManagement_Final;
GO

-- =============================================
-- 1. ROLES
-- =============================================
CREATE TABLE Roles (
    RoleId INT IDENTITY PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO Roles VALUES
(N'Admin'),
(N'Customer');

-- =============================================
-- 2. USERS
-- =============================================
CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(100),
    FullName NVARCHAR(100),
    RoleId INT,

    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

-- =============================================
-- 3. SWIMMING POOLS
-- =============================================
CREATE TABLE SwimmingPools (
    PoolId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),
    Location NVARCHAR(200),
    OpenTime TIME,
    CloseTime TIME
);

-- =============================================
-- 4. CATEGORIES
-- =============================================
CREATE TABLE Categories (
    CategoryId INT IDENTITY PRIMARY KEY,
    CategoryName NVARCHAR(100)
);

-- =============================================
-- 5. PRODUCTS (đồ ăn, nước, phụ kiện)
-- =============================================
CREATE TABLE Products (
    ProductId INT IDENTITY PRIMARY KEY,
    ProductName NVARCHAR(150),
    Price DECIMAL(18,2) CHECK (Price > 0),
    Quantity INT CHECK (Quantity >= 0),
    CategoryId INT,
    IsRentable BIT DEFAULT 0,
    RentalPrice DECIMAL(18,2) DEFAULT 0,

    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

-- =============================================
-- 6. INVOICES
-- =============================================
CREATE TABLE Invoices (
    InvoiceId INT IDENTITY PRIMARY KEY,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UserId INT,
    TotalAmount DECIMAL(18,2),
    Discount DECIMAL(5,2) DEFAULT 0,
    FinalAmount AS (TotalAmount - (TotalAmount * Discount / 100)),
    Status NVARCHAR(50) DEFAULT N'Paid',

    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- =============================================
-- 7. INVOICE DETAILS
-- =============================================
CREATE TABLE InvoiceDetails (
    InvoiceDetailId INT IDENTITY PRIMARY KEY,
    InvoiceId INT,
    ProductId INT NULL,
    Quantity INT CHECK (Quantity > 0),
    Price DECIMAL(18,2),
    SubTotal AS (Quantity * Price),

    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- =============================================
-- 🔥 8. TICKET TYPES (logic vé)
-- =============================================
CREATE TABLE TicketTypes (
    TicketTypeId INT IDENTITY PRIMARY KEY,
    TicketName NVARCHAR(100),
    Price DECIMAL(18,2),
    DurationDays INT,      -- 1 = vé thường, 30 = vé tháng
    IsSingleUse BIT        -- 1 = dùng 1 lần
);

-- =============================================
-- 🔥 9. TICKETS (vé đã mua)
-- =============================================
CREATE TABLE Tickets (
    TicketId INT IDENTITY PRIMARY KEY,
    CustomerId INT,
    TicketTypeId INT,
    PurchaseDate DATETIME DEFAULT GETDATE(),
    ExpiryDate DATETIME,
    IsUsed BIT DEFAULT 0,

    FOREIGN KEY (CustomerId) REFERENCES Users(UserId),
    FOREIGN KEY (TicketTypeId) REFERENCES TicketTypes(TicketTypeId)
);

-- =============================================
-- 🔥 10. TICKET USAGES (lịch sử sử dụng)
-- =============================================
CREATE TABLE TicketUsages (
    UsageId INT IDENTITY PRIMARY KEY,
    TicketId INT,
    UseDate DATE,
    Shift NVARCHAR(20), -- Morning / Afternoon

    FOREIGN KEY (TicketId) REFERENCES Tickets(TicketId)
);

-- =============================================
-- SAMPLE DATA
-- =============================================

-- Users
INSERT INTO Users VALUES
('admin','123',N'Admin',1),
('user1','123',N'Nguyễn Văn A',2);

-- Categories
INSERT INTO Categories VALUES
(N'Đồ ăn'),
(N'Đồ uống'),
(N'Phụ kiện');

-- Products
INSERT INTO Products VALUES
(N'Nước suối',10000,100,2,0,0),
(N'Coca',15000,80,2,0,0),
(N'Kính bơi',120000,50,3,1,30000);

-- Ticket types
INSERT INTO TicketTypes VALUES
(N'Vé thường',50000,1,1),
(N'Vé tháng',500000,30,0);

-- Sample Ticket
INSERT INTO Tickets (CustomerId, TicketTypeId, ExpiryDate)
VALUES (2,1, DATEADD(day,1,GETDATE()));