create database Bikedatabase;

use Bikedatabase;

CREATE TABLE Bikes (
    ID UNIQUEIDENTIFIER PRIMARY KEY,  
    Title NVARCHAR(255) NOT NULL,    
    ImagePath NVARCHAR(255),         
    Regnumber INT NOT NULL,          
    Brand NVARCHAR(100) NOT NULL,    
    Category NVARCHAR(100),           
    Description NVARCHAR(MAX),      
    Model NVARCHAR(100),             
    IsAvailable BIT DEFAULT 1         
);

CREATE TABLE Customers (
    Id UNIQUEIDENTIFIER PRIMARY KEY,   
    FirstName NVARCHAR(100) NOT NULL,  
    Mobilenumber INT NOT NULL,      
    Licence INT NOT NULL,				
    Nic INT NOT NULL,					
    Password NVARCHAR(255) NOT NULL,		
    IsActive BIT DEFAULT 1					
);

CREATE TABLE Managers (
    Id UNIQUEIDENTIFIER PRIMARY KEY,		
    FirstName NVARCHAR(100) NOT NULL,	
    LastName NVARCHAR(100) NOT NULL,		
    Email NVARCHAR(255) NOT NULL,			
    Password NVARCHAR(255) NOT NULL,		
    PhoneNumber NVARCHAR(20),			
    IsActive BIT NOT NULL,					
    Dateofhire DATETIME NOT NULL		
);

CREATE TABLE Rentals (
    id UNIQUEIDENTIFIER PRIMARY KEY,			
    CustomerID UNIQUEIDENTIFIER NOT NULL,		
    MotorbikeID UNIQUEIDENTIFIER NOT NULL,			
    RentalDate DATETIME NOT NULL,					
    Returndate DATETIME NULL,						
    Isoverdue BIT DEFAULT 0,					
    status NVARCHAR(50) DEFAULT 'Pending',			
    FOREIGN KEY (CustomerID) REFERENCES Customers(Id),
    FOREIGN KEY (MotorbikeID) REFERENCES Bikes(ID)
);


select*from customers
select*from Rentals
select*from Bikes

