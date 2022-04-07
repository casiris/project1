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


INSERT INTO Customers VALUES('Bob', 'Ross', '77 Bay Drive', 'Boise', 'ID', '44812');
INSERT INTO Customers VALUES('Fannie', 'Mae', '5643 Castle Street', 'Washington', 'VA', '41512');
INSERT INTO Customers VALUES('Freddie', 'Mac', '5643 Castle Street', 'Washington', 'VA', '41512');
INSERT INTO Customers VALUES('Norm', 'Macdonald', '521', 'New York', 'NY', '56311');
INSERT INTO Customers VALUES('Bill', 'Burr', '889 Shark Avenue', 'Los Angeles', 'CA', '99632');
INSERT INTO Customers VALUES('Patrice', 'O''Neil', '889', 'Boston', 'MA', '85156');


SELECT * FROM Customers;

SELECT * FROM Products;

SELECT * FROM Orders;

SELECT * FROM OrderDetails;

INSERT INTO Products VALUES('Bread', 2.00, '550');
INSERT INTO Products VALUES('Milk', 4.00, '705');
INSERT INTO Products VALUES('Eggs', 1.50, '200');
INSERT INTO Products VALUES('Ham', 3.25, '614');
INSERT INTO Products VALUES('Olive Oil', 4.15, '59');
INSERT INTO Products VALUES('Tea', 3.10, '140');

INSERT INTO Orders VALUES('2022-04-02T11:50:23', 1);
INSERT INTO Orders VALUES('2022-04-01T13:27:02', 5);
INSERT INTO Orders VALUES('2022-04-03T17:24:24', 2);
INSERT INTO Orders VALUES('2022-04-03T21:08:42', 3);
INSERT INTO Orders VALUES('2022-04-05T16:00:59', 7);

DELETE FROM Orders WHERE orderID = 501;

INSERT INTO OrderDetails VALUES(502, 108, 4);
INSERT INTO OrderDetails VALUES(503, 110, 1);
INSERT INTO OrderDetails VALUES(506, 111, 1);
INSERT INTO OrderDetails VALUES(504, 106, 2);
INSERT INTO OrderDetails VALUES(505, 102, 5);

-- order history by customer
SELECT Orders.orderID, dateOrdered, customerID, productName, quantityOrdered, price 
FROM Orders 
JOIN OrderDetails ON Orders.orderID = OrderDetails.orderID
JOIN Products ON Products.productID = OrderDetails.productID
WHERE customerID=1;

-- invoice
SELECT productName, price, quantityOrdered, customerID 
FROM Orders
JOIN OrderDetails ON Orders.orderID = OrderDetails.orderID
JOIN Products ON OrderDetails.productID = Products.productID
WHERE customerID = 1;

SELECT Orders.orderID, dateOrdered, customerID, productName, quantityOrdered, price FROM Orders
JOIN OrderDetails ON orders.orderID = OrderDetails.orderID
JOIN Products ON OrderDetails.productID = Products.productID
WHERE dateOrdered >= DATEADD(day, -1, GETDATE()); 