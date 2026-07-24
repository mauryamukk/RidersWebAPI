# RidersWebAPI
Database Structure
-- ============================================
-- Users
-- ============================================

CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(15) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Role TINYINT NOT NULL, -- 1 = Rider, 2 = Driver
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL
);

-- ============================================
-- Riders
-- ============================================

CREATE TABLE Riders
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Rating DECIMAL(3,2) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Riders_Users
        FOREIGN KEY(UserId)
        REFERENCES Users(Id)
);

-- ============================================
-- Drivers
-- ============================================

CREATE TABLE Drivers
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,

    IsOnline BIT NOT NULL DEFAULT 0,
    IsAvailable BIT NOT NULL DEFAULT 1,

    CurrentLatitude DECIMAL(10,7) NULL,
    CurrentLongitude DECIMAL(10,7) NULL,

    Rating DECIMAL(3,2) NULL,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Drivers_Users
        FOREIGN KEY(UserId)
        REFERENCES Users(Id)
);

-- ============================================
-- Vehicles
-- ============================================

CREATE TABLE Vehicles
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    DriverId INT NOT NULL,

    VehicleNumber NVARCHAR(20) NOT NULL,
    VehicleType NVARCHAR(50) NOT NULL,

    Brand NVARCHAR(100),
    Model NVARCHAR(100),
    Color NVARCHAR(50),

    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Vehicles_Drivers
        FOREIGN KEY(DriverId)
        REFERENCES Drivers(Id)
);

-- ============================================
-- Driver Locations
-- ============================================

CREATE TABLE DriverLocations
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    DriverId INT NOT NULL,

    Latitude DECIMAL(10,7) NOT NULL,
    Longitude DECIMAL(10,7) NOT NULL,

    RecordedAt DATETIME2 DEFAULT GETDATE(),

    CONSTRAINT FK_DriverLocations_Drivers
        FOREIGN KEY(DriverId)
        REFERENCES Drivers(Id)
);

-- ============================================
-- Ride Requests
-- ============================================

CREATE TABLE RideRequests
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    RiderId INT NOT NULL,

    PickupLatitude DECIMAL(10,7) NOT NULL,
    PickupLongitude DECIMAL(10,7) NOT NULL,

    DropLatitude DECIMAL(10,7) NOT NULL,
    DropLongitude DECIMAL(10,7) NOT NULL,

    Status TINYINT NOT NULL DEFAULT 1,
    -- 1 Pending
    -- 2 Matched
    -- 3 Cancelled

    MatchedDriverId INT NULL,

    RequestedAt DATETIME2 DEFAULT GETDATE(),
    CancelledAt DATETIME2 NULL,

    CONSTRAINT FK_RideRequests_Riders
        FOREIGN KEY(RiderId)
        REFERENCES Riders(Id),

    CONSTRAINT FK_RideRequests_Drivers
        FOREIGN KEY(MatchedDriverId)
        REFERENCES Drivers(Id)
);

-- ============================================
-- Rides
-- ============================================

CREATE TABLE Rides
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    RideRequestId BIGINT NOT NULL,

    DriverId INT NOT NULL,
    RiderId INT NOT NULL,

    Status TINYINT NOT NULL,

    StartTime DATETIME2 NULL,
    EndTime DATETIME2 NULL,

    Distance DECIMAL(10,2) NULL,

    EstimatedFare DECIMAL(10,2) NULL,
    FinalFare DECIMAL(10,2) NULL,

    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Rides_RideRequests
        FOREIGN KEY(RideRequestId)
        REFERENCES RideRequests(Id),

    CONSTRAINT FK_Rides_Drivers
        FOREIGN KEY(DriverId)
        REFERENCES Drivers(Id),

    CONSTRAINT FK_Rides_Riders
        FOREIGN KEY(RiderId)
        REFERENCES Riders(Id)
);

-- ============================================
-- Ride Status History
-- ============================================

CREATE TABLE RideStatusHistory
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    RideId BIGINT NOT NULL,

    Status TINYINT NOT NULL,

    Remarks NVARCHAR(500),

    CreatedAt DATETIME2 DEFAULT GETDATE(),

    CONSTRAINT FK_RideStatusHistory_Rides
        FOREIGN KEY(RideId)
        REFERENCES Rides(Id)
);
-- ============================================
-- Driver Availability History
-- ============================================

CREATE TABLE DriverAvailability
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    DriverId INT NOT NULL,

    IsOnline BIT NOT NULL,

    FromTime DATETIME2 NOT NULL DEFAULT GETDATE(),

    ToTime DATETIME2 NULL,

    CONSTRAINT FK_DriverAvailability_Drivers
        FOREIGN KEY(DriverId)
        REFERENCES Drivers(Id)
);
