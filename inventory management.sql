
CREATE DATABASE InventoryManagement10;

USE InventoryManagement10;

  ----------------------------Products----------------------------------------------------

CREATE TABLE Products8 (
    ProductID INT PRIMARY KEY,
    Name VARCHAR(100),
    SKU VARCHAR(50),
    Category VARCHAR(50),
    Quantity INT,
    UnitPrice DECIMAL(10, 2),
    Barcode VARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
INSERT INTO Products8 (ProductID, Name, SKU, Category, Quantity, UnitPrice, Barcode)
VALUES
    (1, 'Gaming Console', 'SKU004', 'Electronics', 15, 499.99, '1234567890126'),
    (2, 'Office Desk', 'SKU005', 'Furniture', 12, 299.99, '1234567890127'),
    (3, 'Novel', 'SKU006', 'Books', 30, 14.99, '1234567890128'),
    (4, 'Action Figure', 'SKU007', 'Toys', 25, 24.99, '1234567890129'),
    (5, 'Tennis Racket', 'SKU008', 'Sports Equipment', 10, 89.99, '1234567890130');

 select * from Products8; 


  ----------------------------Suppliers----------------------------------------------------

CREATE TABLE Suppliers7 (
    SupplierID INT PRIMARY KEY,
    SupplierName VARCHAR(100),
    ContactName VARCHAR(100),
    Phone VARCHAR(15),
    Email VARCHAR(100),
    Address VARCHAR(255)
);
INSERT INTO Suppliers7(SupplierID, SupplierName, ContactName, Phone, Email, Address)
VALUES
    (1, 'Book World', 'David Green', '555-2345', 'david@bookworld.com', '321 Book St'),
    (2, 'Toy Factory', 'Eva White', '555-3456', 'eva@toyfactory.com', '654 Toy Rd'),
    (3, 'Sports Gear Inc.', 'Frank Black', '555-4567', 'frank@sportsgear.com', '987 Sports Ave');

 select * from Suppliers7; 


 ----------------------------PurchaseOrders----------------------------------------------------

CREATE TABLE PurchaseOrders8 (
    PurchaseOrderID INT PRIMARY KEY,
    SupplierID INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(20) CHECK (Status IN ('Pending', 'Completed', 'Cancelled')),
    TotalAmount DECIMAL(10, 2),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers7(SupplierID)
);
INSERT INTO PurchaseOrders8 (PurchaseOrderID, SupplierID, OrderDate, Status, TotalAmount)
VALUES
    (3, 3, GETDATE(), 'Pending', 750.00),  
    (4, 1, GETDATE(), 'Completed', 300.00), 
    (5, 2, GETDATE(), 'Cancelled', 0.00),  
    (6, 3, GETDATE(), 'Completed', 1500.00), 
    (7, 1, GETDATE(), 'Pending', 650.00); 
	 
 select * from  PurchaseOrders8;
 

----------------------------PurchaseOrderDetails-----------------------------------------------------

CREATE TABLE PurchaseOrderDetails9 (
    PODetailID INT PRIMARY KEY,
    PurchaseOrderID INT,
    ProductID INT,
    Quantity INT,
    UnitPrice DECIMAL(10, 2),
    FOREIGN KEY (PurchaseOrderID) REFERENCES PurchaseOrders8(PurchaseOrderID),
    FOREIGN KEY (ProductID) REFERENCES Products8(ProductID)
);
INSERT INTO PurchaseOrderDetails9(PODetailID, PurchaseOrderID, ProductID, Quantity, UnitPrice)
VALUES
    (3, 3, 5, 10, 50.00), 
    (4, 4, 2, 5, 60.00),  
    (5, 5, 4, 7, 18.00),  
    (6, 6, 1, 8, 100.00);

select * from PurchaseOrderDetails9 ;


----------------------------SalesOrders2-----------------------------------------------------

CREATE TABLE SalesOrders7 (
    SalesOrderID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    OrderDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(20) CHECK (Status IN ('Pending', 'Shipped', 'Cancelled')),
    TotalAmount DECIMAL(10, 2)
);
INSERT INTO SalesOrders7 (SalesOrderID, CustomerName, OrderDate, Status, TotalAmount)
VALUES
    (5, 'Bruce Wayne', GETDATE(), 'Pending', 249.99),  
    (6, 'Clark Kent', GETDATE(), 'Shipped', 399.98),   
    (7, 'Lois Lane', GETDATE(), 'Cancelled', 89.99),    
    (8, 'Barry Allen', GETDATE(), 'Pending', 129.99); 

select * from SalesOrders7 ;


----------------------------SalesOrderDetails-----------------------------------------------------

CREATE TABLE SalesOrderDetails7 (
    SODetailID INT PRIMARY KEY,
    SalesOrderID INT,
    ProductID INT,
    Quantity INT,
    UnitPrice DECIMAL(10, 2),
    FOREIGN KEY (SalesOrderID) REFERENCES SalesOrders7(SalesOrderID),
    FOREIGN KEY (ProductID) REFERENCES Products8(ProductID)
);
INSERT INTO SalesOrderDetails7 (SODetailID, SalesOrderID, ProductID, Quantity, UnitPrice)
VALUES
    (5, 5, 1, 1, 249.99), 
    (6, 6, 3, 3, 14.99),
    (7, 7, 4, 5, 24.99),  
    (8, 8, 2, 2, 299.99);  

select * from SalesOrderDetails7;


----------------------------StockMovements-----------------------------------------------------

CREATE TABLE StockMovements7(
    MovementID INT PRIMARY KEY,
    ProductID INT,
    MovementType VARCHAR(20) CHECK (MovementType IN ('IN', 'OUT', 'ADJUSTMENT')),
    Quantity INT,
    MovementDate DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(255),
    FOREIGN KEY (ProductID) REFERENCES Products8(ProductID)
);
INSERT INTO StockMovements7 (MovementID, ProductID, MovementType, Quantity, MovementDate, Description)
VALUES
    (6, 1, 'IN', 30, GETDATE(), 'Restocked Gaming Consoles'),
    (7, 3, 'OUT', 5, GETDATE(), 'Sold 5 Novels'),
    (8, 4, 'IN', 10, GETDATE(), 'Restocked Action Figures'),
    (9, 5, 'ADJUSTMENT', 3, GETDATE(), 'Adjusted stock for Tennis Rackets');

select * from StockMovements7;


----------------------------Users-----------------------------------------------------

CREATE TABLE Users9 (
    UserID INT PRIMARY KEY,
    Username VARCHAR(50),
    PasswordHash VARCHAR(255),
    Role VARCHAR(20) CHECK (Role IN ('Admin', 'Manager', 'Staff')),
    CreatedAt DATETIME DEFAULT GETDATE()
);
INSERT INTO Users9 (UserID, Username, PasswordHash, Role, CreatedAt)
VALUES
    (4, 'user4', 'hashed_password_4', 'Manager', GETDATE()),
    (5, 'user5', 'hashed_password_5', 'Staff', GETDATE()),
    (6, 'user6', 'hashed_password_6', 'Manager', GETDATE()),
    (7, 'user7', 'hashed_password_7', 'Staff', GETDATE());

select * from  Users9;


----------------------------AuditLogs-----------------------------------------------------

CREATE TABLE AuditLogs9 (
    LogID INT PRIMARY KEY,
    UserID INT,
    Action VARCHAR(100),
    TableAffected VARCHAR(50),
    ActionTime DATETIME DEFAULT GETDATE(),
    Description VARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users9(UserID)
);
INSERT INTO AuditLogs9  (LogID, UserID, Action, TableAffected, ActionTime, Description)
VALUES
    (6, 4, 'INSERT', 'PurchaseOrders', GETDATE(), 'Created new purchase order'),
    (7, 5, 'UPDATE', 'SalesOrders', GETDATE(), 'Updated sales order status to Shipped'),
    (8, 6, 'DELETE', 'Products', GETDATE(), 'Removed a product from inventory'),
    (9, 7, 'UPDATE', 'StockMovements', GETDATE(), 'Updated stock quantity for Tennis Rackets');

 select * from AuditLogs9 ; 


 ----------------------------Categories-----------------------------------------------------

CREATE TABLE Categories9(
    CategoryID INT PRIMARY KEY,
    CategoryName VARCHAR(100),
    Description VARCHAR(255)
);
INSERT INTO Categories9 (CategoryID, CategoryName, Description)
VALUES
    (1, 'Electronics', 'Electronic devices and gadgets'),
    (2, 'Furniture', 'Household and office furniture'),
    (3, 'Books', 'Printed books and novels');

 select * from Categories9  ; 


 