CREATE TABLE Role (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  Name VARCHAR(50),
  Description VARCHAR(500),
  CreatedBy VARCHAR(100),
  CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Address (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  AddressName VARCHAR(200),
  City VARCHAR(50),
  State VARCHAR(50),
  PinCode VARCHAR(10),
  IsActive BIT DEFAULT 1 NOT NULL,
  IsDeleted BIT DEFAULT 0 NOT NULL
);

CREATE TABLE Manager (
  Id INT PRIMARY KEY,
  ManagerName VARCHAR(100),
  ManagerEmail VARCHAR(100),
  Password VARCHAR(50)
);

CREATE TABLE dbo.[User] (
  Id INT PRIMARY KEY,
  FirstName VARCHAR(50),
  LastName VARCHAR(50),
  Email VARCHAR(100),
  Password VARCHAR(50),
  Phone VARCHAR(15),
  RoleId INT NOT NULL,
  AddressId INT,
  Department VARCHAR(50),
  ProjectId INT,
  ProjectName VARCHAR(100),
  ManagerId INT,
  Office VARCHAR(50),
  AddressDetails VARCHAR(100),
  CONSTRAINT FK_Role_User FOREIGN KEY (RoleId) REFERENCES Role(Id),
  CONSTRAINT FK_Address_User FOREIGN KEY (AddressId) REFERENCES Address(Id),
  CONSTRAINT FK_Manager_User FOREIGN KEY (ManagerId) REFERENCES Manager(Id)
);

CREATE TABLE CabRequirementSlot(
  Id INT PRIMARY KEY IDENTITY(1, 1),
  Time TIME(7) NOT NULL,
  SlotAvailableTime INT NOT NULL,
  CreatedBy VARCHAR(100) NOT NULL,
  CreatedDate DATETIME DEFAULT GETDATE() NOT NULL
);

CREATE TABLE CabRequirementRequest (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  UserId INT NOT NULL,
  TimeSlotId INT NOT NULL,
  RequestDate DATETIME,
  IsApproved INT NOT NULL,
  IsAdhoc BIT,
  ApprovedBy VARCHAR(500),
  PickUpLocation VARCHAR(500),
  DropLocation VARCHAR(500), 
  IsDeleted BIT,
  CreatedBy VARCHAR(100),
  CreatedDate DATETIME DEFAULT GETDATE(),
  CONSTRAINT FK_User_CabReqReq FOREIGN KEY (UserId) REFERENCES [User](Id),
  CONSTRAINT FK_CabReqSlot_CabReqReq FOREIGN KEY (TimeSlotId) REFERENCES CabRequirementSlot(Id)
);
