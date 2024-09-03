CREATE DATABASE CrossCountry

CREATE TABLE Customer
(
	Cus_ID varchar(12) Primary Key check(Cus_ID like 'Cus%') not null,
	Cus_Username varchar(50) Unique not null,
	Cus_Name varchar(50) not null,
	Cus_Age int not null,
	Cus_Password varchar(50) not null,
	Cus_Country varchar(20) not null,
);

CREATE TABLE Customer_Address
(
	Address_ID varchar(10) Primary key check(Address_ID like 'Add%') not null,
	Cus_ID varchar(12) foreign key references Customer(Cus_ID) not null,
	Address varchar(200) not null
);

CREATE TABLE Orrder
(
	Order_ID varchar(10) Primary Key check(Order_ID like 'Odr%'),
	Item_ID varchar(5) Foreign Key References Items(Item_ID),
	Qty int not null,
	Order_Address varchar(200) not null,
	Bill int not null,
	Order_Date datetime not null,
	Cus_ID varchar(12) Foreign Key References Customer(Cus_ID)
);

CREATE TABLE Items
(
	Item_ID varchar(5) Primary Key check(Item_ID like 'I%') not null,
	Item_Qty int not null,
	Item_Price int not null,
);

CREATE TABLE Cart
(
	Cart_ID varchar(10) Primary key check(Cart_ID like 'Crt%') not null,
	Cus_ID varchar(12) Foreign Key References Customer(Cus_ID),
	Item_ID varchar(5)  Foreign Key References Items(Item_ID),
	Cart_Qty int not null,
	Price int not null,
);

CREATE TABLE Admin
(
	Admin_ID varchar(10) Primary Key check(Admin_ID like 'Adm%') not null,
	Admin_Username varchar(10) Unique check(Admin_Username like 'Admin-%') not null,
	Admin_Password varchar(12) not null 
);


INSERT INTO Items (Item_ID, Item_Qty, Item_Price)
values
	('I001', 26, 6700),
	('I002', 15, 8500),
	('I003', 45, 9800),
	('I004', 30, 4500),
	('I005', 18, 6500),
	('I006', 50, 11000),
	('I007', 25, 5000),
	('I008', 12, 9500),
	('I009', 38, 7800),
	('I010', 22, 5850),
	('I011', 55, 8500),
	('I012', 40, 7200),
	('I013', 20, 11200),
	('I014', 28, 8900),
	('I015', 15, 6500),
	('I016', 48, 4800),
	('I017', 33, 12500),
	('I018', 16, 7100)

INSERT INTO Admin (Admin_ID, Admin_Username, Admin_Password)
VALUES 
	('Adm001', 'Admin-im', 'imesh123'),
	('Adm002', 'Admin-john', '12345678'),
	('Adm003', 'Admin-Cate', '12345678')





/*Sample queries*/

SELECT Item_ID From Items
order by Item_ID Asc;


drop table Admin

UPDATE Customer Set Cus_Age = 40 WHERE Cus_ID = 'CusQ3VR1ECU5 '

INSERT INTO Customer_Address (Cus_ID, Address_ID, Address)
VALUES 
	('CusQ3VR1ECU5', 'Add01', 'galle'),
	('CusQ3VR1ECU5', 'Add02', 'kandy')

SELECT * FROM Customer
SELECT * FROM Customer_Address
SELECT * FROM Orrder
SELECT * FROM Items
SELECT * FROM Cart
SELECT * FROM Payment

SELECT COUNT(*) FROM Customer WHERE Cus_Id = 'CusQ3VR1ECU5'
SELECT * FROM Items WHERE Item_ID
SELECT Address FROM Customer_Address WHERE Cus_ID = 'Cus8SYNAYV9S'
SELECT * FROM Orrder WHERE Cus_ID = 'CusTEX09L6VA' ORDER BY Order_Date DESC

UPDATE Items SET Item_Qty = '23' WHERE Item_ID = 'I001'

TRUNCATE TABLE Customer_Address
TRUNCATE TABLE Orrder
TRUNCATE TABLE Cart
TRUNCATE TABLE Items
DELETE FROM Customer


