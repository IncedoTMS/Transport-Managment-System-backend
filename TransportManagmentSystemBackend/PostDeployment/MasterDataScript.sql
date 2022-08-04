CREATE DATABASE TMS_Database;

USE TMS_Database;

CREATE TABLE Role (
  Id INT PRIMARY KEY,
  Name VARCHAR(50),
  Description VARCHAR(500),
  CreatedBy VARCHAR(100),
  CreatedDate DATETIME
);

CREATE TABLE Address (
  Id INT PRIMARY KEY,
  AddressName VARCHAR(200),
  City VARCHAR(50),
  State VARCHAR(50),
  PinCode VARCHAR(10),
  IsActive BIT,
  IsDeleted BIT
);

CREATE TABLE "User" (
  Id INT PRIMARY KEY,
  FirstName VARCHAR(50),
  LastName VARCHAR(50),
  EmpCode INT,
  Email VARCHAR(100),
  Password VARCHAR(50),
  Phone VARCHAR(15),
  RoleId INT,
  AddressId INT,
  CONSTRAINT FK_Role_User FOREIGN KEY (RoleId) REFERENCES Role(Id),
  CONSTRAINT FK_Address_User FOREIGN KEY (AddressId) REFERENCES Address(Id)
);

CREATE TABLE CabRequirementSlot (
  Id INT PRIMARY KEY,
  Time DATETIME,
  SlotAvailableTime TIMESTAMP,
  CreatedBy VARCHAR(100),
  CreatedDate DATETIME
);

CREATE TABLE CabRequirementRequest (
  Id INT PRIMARY KEY,
  UserId INT,
  TimeSlotId INT,
  RequestDate DATETIME,
  IsApproved BIT,
  ApprovedBy VARCHAR(100),
  PickUpLocation VARCHAR(300),
  DropLocation VARCHAR(300), 
  IsDeleted BIT,
  CreatedBy VARCHAR(100),
  CreatedDate DATETIME,
  CONSTRAINT FK_User_CabReqReq FOREIGN KEY (UserId) REFERENCES "User"(Id),
  CONSTRAINT FK_CabReqSlot_CabReqReq FOREIGN KEY (TimeSlotId) REFERENCES CabRequirementSlot(Id)
);
