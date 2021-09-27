USE master;
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
	DROP DATABASE DvdLibrary
GO

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary;
GO

CREATE TABLE Ratings (
	RatingId INT PRIMARY KEY IDENTITY(1, 1),
	RatingName NVARCHAR(5) NOT NULL
);

CREATE TABLE Directors (
	DirectorId INT PRIMARY KEY IDENTITY(1, 1),
	DirectorName NVARCHAR(50) NOT NULL
);

CREATE TABLE Dvds (
	DvdId INT PRIMARY KEY IDENTITY(1, 1),
	RatingId INT NULL,
	DirectorId INT NULL,
	Title NVARCHAR(50) NOT NULL,
	ReleaseYear INT NOT NULL,
	Notes NVARCHAR(200) NULL,
	CONSTRAINT fk_Dvd_Rating FOREIGN KEY (RatingId)
		REFERENCES Ratings(RatingId),
	CONSTRAINT fk_Dvd_Director FOREIGN KEY (DirectorId)
		REFERENCES Directors(DirectorId)
);
