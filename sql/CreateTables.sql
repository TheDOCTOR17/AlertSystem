CREATE TABLE UserLoginEvents (
    Id INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(100),
    IPAddress NVARCHAR(100),
    LoginTime DATETIME
);

CREATE TABLE UserAlerts (
    Id INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(100),
    IPAddress NVARCHAR(100),
    AlertTime DATETIME,
    AlertMessage NVARCHAR(500)
);
