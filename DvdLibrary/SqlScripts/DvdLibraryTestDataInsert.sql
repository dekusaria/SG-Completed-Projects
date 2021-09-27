USE DvdLibrary;
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdLibraryTestDataInsert')
		DROP PROCEDURE DvdLibraryTestDataInsert
GO

CREATE PROCEDURE DvdLibraryTestDataInsert AS
BEGIN
	EXEC DvdDbReset;

	SET IDENTITY_INSERT Directors ON;
	INSERT INTO Directors (DirectorId, DirectorName)
	VALUES (1, 'Tim Burton'),
	(2, 'Peter Jackson'),
	(3, 'Cate Shortland')
	SET IDENTITY_INSERT Directors OFF;

	SET IDENTITY_INSERT Dvds ON;
	INSERT INTO Dvds (DvdId, RatingId, DirectorId, Title, ReleaseYear, Notes)
	VALUES (1, 3, 1, 'Edward Scissorhands', 1990, 'A touching movie'),
	(2, 3, 2, 'The Lord of the Rings: The Two Towers', 2002, 'Now is the hour when we draw swords!'),
	(3, 2, 1, 'Beetlejuice', 1988, 'How is this rated PG? I was terrified of this movie as a child.'),
	(4, 3, 3, 'Black Widow', 2021, 'A decent MCU entry that really should have been made years ago.'),
	(5, NULL, NULL, 'The Neverending Story', 1984, 'A classic! Shocking that no studio has tried to remake it yet.')
	SET IDENTITY_INSERT Dvds OFF;
END