USE DvdLibrary;
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'RatingsSelectAll')
		DROP PROCEDURE RatingsSelectAll
GO

CREATE PROCEDURE RatingsSelectAll AS
BEGIN
	SELECT * FROM Ratings;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'RatingsSelectById')
		DROP PROCEDURE RatingsSelectById
GO

CREATE PROCEDURE RatingsSelectById (
	@RatingId INT
) AS
BEGIN
	SELECT * FROM Ratings WHERE RatingId = @RatingId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DirectorsSelectAll')
		DROP PROCEDURE DirectorsSelectAll
GO

CREATE PROCEDURE DirectorsSelectAll AS
BEGIN
	SELECT * FROM Directors;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DirectorsSelectById')
		DROP PROCEDURE DirectorsSelectById
GO

CREATE PROCEDURE DirectorsSelectById (
	@DirectorId INT
) AS
BEGIN
	SELECT * FROM Directors WHERE DirectorId = @DirectorId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DirectorsSelectByName')
		DROP PROCEDURE DirectorsSelectByName
GO

CREATE PROCEDURE DirectorsSelectByName (
	@DirectorName NVARCHAR(50)
) AS
BEGIN
	SELECT * FROM Directors WHERE DirectorName LIKE '%' + @DirectorName + '%';
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsSelectById')
		DROP PROCEDURE DvdsSelectById
GO

CREATE PROCEDURE DvdsSelectById (
	@DvdId int
) AS
BEGIN
	SELECT d.DvdId, d.Title, d.ReleaseYear, dr.DirectorName, r.RatingName, d.Notes
	FROM Dvds d
		LEFT OUTER JOIN Ratings r ON d.RatingId = r.RatingId
		LEFT OUTER JOIN Directors dr ON dr.DirectorId = d.DirectorId
	WHERE d.DvdId = @DvdId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsSelectAll')
		DROP PROCEDURE DvdsSelectAll
GO

CREATE PROCEDURE DvdsSelectAll AS
BEGIN
	SELECT d.DvdId, d.Title, d.ReleaseYear, dr.DirectorName, r.RatingName, d.Notes
	FROM Dvds d
		LEFT OUTER JOIN Ratings r ON d.RatingId = r.RatingId
		LEFT OUTER JOIN Directors dr ON dr.DirectorId = d.DirectorId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsSelectByTitle')
		DROP PROCEDURE DvdsSelectByTitle
GO

CREATE PROCEDURE DvdsSelectByTitle (
	@Title NVARCHAR(50)
) AS
BEGIN
	SELECT d.DvdId, d.Title, d.ReleaseYear, dr.DirectorName, r.RatingName, d.Notes
	FROM Dvds d
		LEFT OUTER JOIN Ratings r ON d.RatingId = r.RatingId
		LEFT OUTER JOIN Directors dr ON dr.DirectorId = d.DirectorId
	WHERE d.Title LIKE '%' + @Title + '%';
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsSelectByReleaseYear')
		DROP PROCEDURE DvdsSelectByReleaseYear
GO

CREATE PROCEDURE DvdsSelectByReleaseYear (
	@ReleaseYear INT
) AS
BEGIN
	SELECT d.DvdId, d.Title, d.ReleaseYear, dr.DirectorName, r.RatingName, d.Notes
	FROM Dvds d
		LEFT OUTER JOIN Ratings r ON d.RatingId = r.RatingId
		LEFT OUTER JOIN Directors dr ON dr.DirectorId = d.DirectorId
	WHERE d.ReleaseYear = @ReleaseYear;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsSelectByDirectorName')
		DROP PROCEDURE DvdsSelectByDirectorName
GO

CREATE PROCEDURE DvdsSelectByDirectorName (
	@DirectorName NVARCHAR(50)
) AS
BEGIN
	SELECT d.DvdId, d.Title, d.ReleaseYear, dr.DirectorName, r.RatingName, d.Notes
	FROM Dvds d
		LEFT OUTER JOIN Ratings r ON d.RatingId = r.RatingId
		LEFT OUTER JOIN Directors dr ON dr.DirectorId = d.DirectorId
	WHERE dr.DirectorName LIKE '%' + @DirectorName + '%';
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsSelectByRatingName')
		DROP PROCEDURE DvdsSelectByRatingName
GO

CREATE PROCEDURE DvdsSelectByRatingName (
	@RatingName NVARCHAR(5)
) AS
BEGIN
	SELECT d.DvdId, d.Title, d.ReleaseYear, dr.DirectorName, r.RatingName, d.Notes
	FROM Dvds d
		LEFT OUTER JOIN Ratings r ON d.RatingId = r.RatingId
		LEFT OUTER JOIN Directors dr ON dr.DirectorId = d.DirectorId
	WHERE r.RatingName LIKE @RatingName;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsInsert')
		DROP PROCEDURE DvdsInsert
GO

CREATE PROCEDURE DvdsInsert (
	@DvdId INT OUTPUT,
	@Title NVARCHAR(50),
	@ReleaseYear INT,
	@DirectorName NVARCHAR(50),
	@RatingName NVARCHAR(5),
	@Notes NVARCHAR(200)
) AS
BEGIN
DECLARE @DirectorId INT,
		@RatingId INT

IF @DirectorName IS NOT NULL
	BEGIN
	IF EXISTS(SELECT * FROM Directors WHERE DirectorName LIKE @DirectorName)
		BEGIN
		SET @DirectorId = (SELECT DirectorId FROM Directors WHERE DirectorName LIKE @DirectorName)
		END
	ELSE
		BEGIN
		INSERT Directors(DirectorName)
		VALUES(@DirectorName)
		SET @DirectorId = SCOPE_IDENTITY()
		END
	END
ELSE
	BEGIN
	SET @DirectorId = NULL;
	END

IF @RatingName IS NOT NULL
	BEGIN
	SET @RatingId = (SELECT RatingId FROM Ratings WHERE RatingName = @RatingName)
	END
ELSE
	BEGIN
	SET @RatingId = NULL;
	END

	INSERT Dvds(Title, ReleaseYear, DirectorId, RatingId, Notes)
	VALUES(@Title, @ReleaseYear, @DirectorId, @RatingId, @Notes)
	SET @DvdId = SCOPE_IDENTITY()
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsUpdate')
		DROP PROCEDURE DvdsUpdate
GO

CREATE PROCEDURE DvdsUpdate (
	@DvdId INT,
	@DirectorName NVARCHAR(50),
	@RatingName NVARCHAR(5),
	@Title NVARCHAR(50),
	@ReleaseYear INT,
	@Notes NVARCHAR(200)
) AS
BEGIN
	BEGIN TRANSACTION
DECLARE @DirectorId INT,
		@RatingId INT

IF @DirectorName IS NULL
	BEGIN
	SET @DirectorId = NULL;
	END
ELSE
	BEGIN
	IF EXISTS(SELECT * FROM Directors WHERE DirectorName LIKE @DirectorName)
		BEGIN
		SET @DirectorId = (SELECT DirectorId FROM Directors WHERE DirectorName LIKE @DirectorName)
		END
	ELSE
		BEGIN
		INSERT Directors(DirectorName)
		VALUES (@DirectorName)
		SET @DirectorId = SCOPE_IDENTITY()
		END
	END

IF @RatingName IS NULL
	BEGIN
	SET @RatingId = NULL;
	END
ELSE
	BEGIN
	SET @RatingId = (SELECT RatingId FROM Ratings WHERE RatingName LIKE @RatingName)
	END

	UPDATE Dvds SET
	Title = @Title,
	ReleaseYear = @ReleaseYear,
	DirectorId = @DirectorId,
	RatingId = @RatingId,
	Notes = @Notes
	WHERE DvdId = @DvdId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DvdsDelete')
		DROP PROCEDURE DvdsDelete
GO

CREATE PROCEDURE DvdsDelete (
	@DvdId INT
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Dvds WHERE DvdId = @DvdId;

	COMMIT TRANSACTION
END
GO