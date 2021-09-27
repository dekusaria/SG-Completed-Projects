CREATE LOGIN DvdLibraryApp 
WITH PASSWORD = 'testing123',
DEFAULT_DATABASE = DvdLibrary;
GO

USE DvdLibrary;
GO

CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp;
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON DvdLibrary TO DvdLibraryApp;
GO

GRANT EXEC TO DvdLibraryApp;
GO


