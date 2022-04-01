CREATE DATABASE shoppingApp;

USE shoppingApp;

CREATE TABLE Customers
(
	customerID INT PRIMARY KEY IDENTITY(1,1),
	firstName VARCHAR(20),
	lastName VARCHAR(20),
	street VARCHAR(50),
	city VARCHAR(50),
	stateInitials VARCHAR(2),
	zipcode INT
);

CREATE TABLE Products
(
	productID INT PRIMARY KEY IDENTITY(100,1),
	productName VARCHAR(20),
	price FLOAT,
	quantityInStock INT
);

CREATE TABLE Orders
(
	orderID INT PRIMARY KEY IDENTITY(500,1),
	dateOrdered DATETIME,
	customerID INT REFERENCES Customers(customerID)
);

CREATE TABLE OrderDetails
(
	detailsID INT PRIMARY KEY IDENTITY(2000,1),
	orderID INT REFERENCES Orders(orderID),
	productID INT REFERENCES Products(productID),
	quantityOrdered INT
);