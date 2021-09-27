USE master;
GO

CREATE LOGIN gcAppUser WITH PASSWORD = 'Testing123', DEFAULT_DATABASE = GuildCars;
GO

USE GuildCars;
CREATE USER appUser FOR LOGIN gcAppUser;
EXEC sp_addrolemember 'db_datareader', 'appUser'
EXEC sp_addrolemember 'db_datawriter', 'appUser'
GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON GuildCars TO appUser;
GO