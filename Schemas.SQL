-- Create the DotNetAPI database
CREATE DATABASE DotNetAPI;
GO

-- Use the newly created database
USE DotNetAPI;
GO

-- Create the schema called APISchema
CREATE SCHEMA APISchema;
GO

-- Create the JobInfo table in the APISchema schema
CREATE TABLE APISchema.JobInfo (
    JobId INT PRIMARY KEY IDENTITY(1,1),
    JobTitle NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    Salary DECIMAL(10, 2) NOT NULL
);
GO

-- Create the Users table in the APISchema schema
CREATE TABLE APISchema.Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Active BIT NOT NULL,
    JobId INT,
    CONSTRAINT FK_Users_JobInfo FOREIGN KEY (JobId)
        REFERENCES APISchema.JobInfo(JobId)
);
GO