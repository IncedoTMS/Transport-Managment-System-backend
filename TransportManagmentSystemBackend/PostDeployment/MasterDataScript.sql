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

CREATE TABLE dbo.[User] (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  FirstName VARCHAR(50),
  LastName VARCHAR(50),
  EmpCode INT,
  Email VARCHAR(100),
  Password VARCHAR(50),
  Phone VARCHAR(15),
  RoleId INT NOT NULL,
  AddressId INT NOT NULL,
  CONSTRAINT FK_Role_User FOREIGN KEY (RoleId) REFERENCES Role(Id),
  CONSTRAINT FK_Address_User FOREIGN KEY (AddressId) REFERENCES Address(Id)
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
  IsApproved BIT,
  ApprovedBy VARCHAR(500),
  PickUpLocation VARCHAR(500),
  DropLocation VARCHAR(500), 
  IsDeleted BIT,
  CreatedBy VARCHAR(100),
  CreatedDate DATETIME DEFAULT GETDATE(),
  CONSTRAINT FK_User_CabReqReq FOREIGN KEY (UserId) REFERENCES [User](Id),
  CONSTRAINT FK_CabReqSlot_CabReqReq FOREIGN KEY (TimeSlotId) REFERENCES CabRequirementSlot(Id)
);