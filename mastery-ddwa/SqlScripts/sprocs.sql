USE GuildCars;
GO

-- VEHICLE REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesInsert')
		DROP PROCEDURE VehiclesInsert
GO

CREATE PROCEDURE VehiclesInsert (
	@VehicleId INT OUTPUT,
	@Vin NCHAR(16),
	@VehicleTypeId INT,
	@ModelId INT,
	@ColorId INT,
	@InteriorId INT,
	@BodyStyleId INT,
	@TransmissionId INT,
	@Year INT,
	@Mileage INT,
	@Msrp DECIMAL(8,2),
	@SalePrice DECIMAL(8,2),
	@Description NVARCHAR(200),
	@ImageFileName NVARCHAR(50),
	@IsFeatured BIT,
	@IsSold BIT
) AS
BEGIN


INSERT INTO Vehicles (Vin, VehicleTypeId, ModelId, ColorId, InteriorId, BodyStyleId, TransmissionId, 
	[Year], Mileage, Msrp, SalePrice, [Description], ImageFileName, IsFeatured, IsSold)
VALUES (@Vin, @VehicleTypeId, @ModelId, @ColorId, @InteriorId, @BodyStyleId, @TransmissionId,
	@Year, @Mileage, @Msrp, @SalePrice, @Description, @ImageFileName, @IsFeatured, @IsSold)
SET @VehicleId = SCOPE_IDENTITY();

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesSelectAll')
		DROP PROCEDURE VehiclesSelectAll
GO

CREATE PROCEDURE VehiclesSelectAll AS
BEGIN
	SELECT * FROM Vehicles;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesSelectById')
		DROP PROCEDURE VehiclesSelectById
GO

CREATE PROCEDURE VehiclesSelectById(
	@VehicleId INT
) AS
BEGIN
	SELECT * FROM Vehicles WHERE @VehicleId = VehicleId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesSelectDetails')
		DROP PROCEDURE VehiclesSelectDetails
GO

CREATE PROCEDURE VehiclesSelectDetails(
	@VehicleId INT
) AS
BEGIN
	SELECT v.VehicleId, v.Vin, vt.VehicleTypeName, m.ModelName, ma.MakeName, c.ColorName, i.InteriorType, b.BodyStyleType, t.TransmissionType,
	v.[Year], v.Mileage, v.Msrp, v.SalePrice, v.[Description], v.ImageFileName, v.IsFeatured, v.IsSold
	FROM Vehicles v
		INNER JOIN VehicleTypes vt ON v.VehicleTypeId = vt.VehicleTypeId
		INNER JOIN Models m ON v.ModelId = m.ModelId
		INNER JOIN Makes ma ON m.MakeId = ma.MakeId
		INNER JOIN Colors c ON v.ColorId = c.ColorId
		INNER JOIN Interiors i ON v.InteriorId = i.InteriorId
		INNER JOIN BodyStyles b ON v.BodyStyleId = b.BodyStyleId
		INNER JOIN Transmissions t ON v.TransmissionId = t.TransmissionId
		WHERE v.VehicleId = @VehicleId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesDelete')
		DROP PROCEDURE VehiclesDelete
GO

CREATE PROCEDURE VehiclesDelete(
	@VehicleId INT
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Vehicles WHERE VehicleId = @VehicleId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesUpdate')
		DROP PROCEDURE VehiclesUpdate
GO

CREATE PROCEDURE VehiclesUpdate(
	@VehicleId INT,
	@Vin NCHAR(16),
	@VehicleTypeId INT,
	@ModelId INT,
	@ColorId INT,
	@InteriorId INT,
	@BodyStyleId INT,
	@TransmissionId INT,
	@Year INT,
	@Mileage INT,
	@Msrp DECIMAL(8,2),
	@SalePrice DECIMAL(8,2),
	@Description NVARCHAR(200),
	@ImageFileName NVARCHAR(50),
	@IsFeatured BIT,
	@IsSold BIT
) AS
BEGIN
	BEGIN TRANSACTION

	UPDATE Vehicles SET
	Vin = @Vin,
	VehicleTypeId = @VehicleTypeId,
	ModelId = @ModelId,
	ColorId = @ColorId,
	InteriorId = @InteriorId,
	BodyStyleId = @BodyStyleId,
	TransmissionId = @TransmissionId,
	[Year] = @Year,
	Mileage = @Mileage,
	Msrp = @Msrp,
	SalePrice = @SalePrice,
	[Description] = @Description,
	ImageFileName = @ImageFileName,
	IsFeatured = @IsFeatured,
	IsSold = @IsSold
	WHERE VehicleId = @VehicleId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesSelectByVehicleType')
		DROP PROCEDURE VehiclesSelectByVehicleType
GO

CREATE PROCEDURE VehiclesSelectByVehicleType (
	@VehicleTypeId INT
) AS
BEGIN

	SELECT v.VehicleId, v.Vin, vt.VehicleTypeName, m.ModelName, ma.MakeName, c.ColorName, i.InteriorType, b.BodyStyleType, t.TransmissionType,
	v.[Year], v.Mileage, v.Msrp, v.SalePrice, v.[Description], v.ImageFileName, v.IsFeatured, v.IsSold
	FROM Vehicles v
		INNER JOIN VehicleTypes vt ON v.VehicleTypeId = vt.VehicleTypeId
		INNER JOIN Models m ON v.ModelId = m.ModelId
		INNER JOIN Makes ma ON m.MakeId = ma.MakeId
		INNER JOIN Colors c ON v.ColorId = c.ColorId
		INNER JOIN Interiors i ON v.InteriorId = i.InteriorId
		INNER JOIN BodyStyles b ON v.BodyStyleId = b.BodyStyleId
		INNER JOIN Transmissions t ON v.TransmissionId = t.TransmissionId
	WHERE @VehicleTypeId IS NULL OR @VehicleTypeId = 0 OR v.VehicleTypeId = @VehicleTypeId;

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesSelectFeatured')
		DROP PROCEDURE VehiclesSelectFeatured
GO

CREATE PROCEDURE VehiclesSelectFeatured AS
BEGIN

	SELECT v.VehicleId, v.Vin, v.[Year], ma.MakeName, m.ModelName, v.SalePrice, v.ImageFileName
	FROM Vehicles v
		INNER JOIN Models m ON v.ModelId = m.ModelId
		INNER JOIN Makes ma ON m.MakeId = ma.MakeId
	WHERE v.IsFeatured = 1 AND v.IsSold = 0;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesSearch')
		DROP PROCEDURE VehiclesSearch
GO

CREATE PROCEDURE VehiclesSearch (
	@SearchTerm NVARCHAR(50),
	@VehicleTypeId INT,
	@MinPrice DECIMAL,
	@MaxPrice DECIMAL,
	@MinYear INT,
	@MaxYear INT
) AS
BEGIN

SET NOCOUNT ON
BEGIN TRANSACTION
	
	SELECT v.VehicleId, v.Vin, vt.VehicleTypeName, m.ModelName, ma.MakeName, c.ColorName, i.InteriorType,
	b.BodyStyleType, t.TransmissionType, v.[Year], v.Mileage, v.Msrp, v.SalePrice, v.ImageFileName
	FROM Vehicles v
		INNER JOIN VehicleTypes vt ON v.VehicleTypeId = vt.VehicleTypeId
		INNER JOIN Models m ON v.ModelId = m.ModelId
		INNER JOIN Makes ma ON m.MakeId = ma.MakeId
		INNER JOIN Colors c ON v.ColorId = c.ColorId
		INNER JOIN Interiors i ON v.InteriorId = i.InteriorId
		INNER JOIN BodyStyles b ON v.BodyStyleId = b.BodyStyleId
		INNER JOIN Transmissions t ON v.TransmissionId = t.TransmissionId
	WHERE (@SearchTerm IS NULL OR m.ModelName LIKE '%' + @SearchTerm + '%'
								OR ma.MakeName LIKE '%' + @SearchTerm + '%'
								OR v.[Year] = CASE
									WHEN (ISNUMERIC(@SearchTerm) = 1)
									THEN CONVERT(INT, @SearchTerm)
									ELSE 0
									END)
		AND (@MinPrice IS NULL OR v.SalePrice >= @MinPrice)
		AND (@MaxPrice IS NULL OR v.SalePrice <= @MaxPrice)
		AND (@MinYear IS NULL OR v.[Year] >= @MinYear)
		AND (@MaxYear IS NULL OR v.[Year] <= @MaxYear)
		AND (@VehicleTypeId IS NULL OR v.VehicleTypeId = @VehicleTypeId)
		AND v.IsSold = 0
	ORDER BY v.Msrp DESC
		OFFSET 0 ROWS
		FETCH NEXT 20 ROWS ONLY;
										

COMMIT TRANSACTION

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesIsSoldUpdate')
		DROP PROCEDURE VehiclesIsSoldUpdate
GO

CREATE PROCEDURE VehiclesIsSoldUpdate (
	@VehicleId INT
) AS
BEGIN
	UPDATE Vehicles SET
	IsSold = 1
	WHERE VehicleId = @VehicleId;
END
GO

-- MODEL REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectAll')
		DROP PROCEDURE ModelsSelectAll
GO

CREATE PROCEDURE ModelsSelectAll AS
BEGIN
	SELECT * FROM Models
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectById')
		DROP PROCEDURE ModelsSelectById
GO

CREATE PROCEDURE ModelsSelectById (
	@ModelId INT
) AS
BEGIN
	SELECT *
	FROM Models m
	WHERE m.ModelId = @ModelId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectByMakeId')
		DROP PROCEDURE ModelsSelectByMakeId
GO

CREATE PROCEDURE ModelsSelectByMakeId (
	@MakeId INT
) AS
BEGIN
	SELECT *
	FROM Models m
	WHERE m.MakeId = @MakeId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsInsert')
		DROP PROCEDURE ModelsInsert
GO

CREATE PROCEDURE ModelsInsert (
	@ModelId INT OUTPUT,
	@MakeId INT,
	@ModelName NVARCHAR(50),
	@DateAdded DATETIME2(0),
	@EmployeeEmail NVARCHAR(50)
) AS
BEGIN

	INSERT INTO Models (MakeId, ModelName, DateAdded, EmployeeEmail)
	VALUES (@MakeId, @ModelName, @DateAdded, @EmployeeEmail)
	SET @ModelId = SCOPE_IDENTITY();

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsUpdate')
		DROP PROCEDURE ModelsUpdate
GO

CREATE PROCEDURE ModelsUpdate (
	@ModelId INT,
	@MakeId INT,
	@ModelName NVARCHAR(50),
	@DateAdded DATETIME2(0),
	@EmployeeEmail NVARCHAR(50)
) AS
BEGIN
	BEGIN TRANSACTION

	UPDATE Models SET
	MakeId = @MakeId,
	ModelName = @ModelName,
	DateAdded = @DateAdded,
	EmployeeEmail = @EmployeeEmail
	WHERE ModelId = @ModelId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsDelete')
		DROP PROCEDURE ModelsDelete
GO

CREATE PROCEDURE ModelsDelete (
	@ModelId INT
) AS
BEGIN

	DELETE FROM Models WHERE ModelId = @ModelId;

END
GO

-- MAKE REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesSelectAll')
		DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll AS
BEGIN
	SELECT * FROM Makes
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesSelectById')
		DROP PROCEDURE MakesSelectById
GO

CREATE PROCEDURE MakesSelectById (
	@MakeId INT
) AS
BEGIN
	SELECT *
	FROM Makes ma
	WHERE ma.MakeId = @MakeId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesInsert')
		DROP PROCEDURE MakesInsert
GO

CREATE PROCEDURE MakesInsert (
	@MakeId INT OUTPUT,
	@MakeName NVARCHAR(50),
	@DateAdded DATETIME2(0),
	@EmployeeEmail NVARCHAR(50)
) AS
BEGIN

	INSERT INTO Makes (MakeName, DateAdded, EmployeeEmail)
	VALUES (@MakeName, @DateAdded, @EmployeeEmail)
	SET @MakeId = SCOPE_IDENTITY();

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesUpdate')
		DROP PROCEDURE MakesUpdate
GO

CREATE PROCEDURE MakesUpdate (
	@MakeId INT,
	@MakeName NVARCHAR(50),
	@DateAdded DATETIME2(0),
	@EmployeeEmail NVARCHAR(50)
) AS
BEGIN
	BEGIN TRANSACTION

	UPDATE Makes SET
	MakeName = @MakeName,
	DateAdded = @DateAdded,
	EmployeeEmail = @EmployeeEmail
	WHERE MakeId = @MakeId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesDelete')
		DROP PROCEDURE MakesDelete
GO

CREATE PROCEDURE MakesDelete (
	@MakeId INT
) AS
BEGIN

	DELETE FROM Makes WHERE MakeId = @MakeId;

END
GO

-- SPECIALS REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsSelectAll')
		DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
	SELECT * FROM Specials
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsSelectById')
		DROP PROCEDURE SpecialsSelectById
GO

CREATE PROCEDURE SpecialsSelectById (
	@SpecialId INT
) AS
BEGIN
	SELECT *
	FROM Specials
	WHERE SpecialId = @SpecialId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsInsert')
		DROP PROCEDURE SpecialsInsert
GO

CREATE PROCEDURE SpecialsInsert (
	@SpecialId INT OUTPUT,
	@SpecialTitle NVARCHAR(50),
	@SpecialDescription NVARCHAR(500)
) AS
BEGIN

	INSERT INTO Specials (SpecialTitle, SpecialDescription)
	VALUES (@SpecialTitle, @SpecialDescription)
	SET @SpecialId = SCOPE_IDENTITY();

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsUpdate')
		DROP PROCEDURE SpecialsUpdate
GO

CREATE PROCEDURE SpecialsUpdate (
	@SpecialId INT,
	@SpecialTitle NVARCHAR(50),
	@SpecialDescription NVARCHAR(500)
) AS
BEGIN
	BEGIN TRANSACTION

	UPDATE Specials SET
	SpecialTitle = @SpecialTitle,
	SpecialDescription = @SpecialDescription
	WHERE SpecialId = @SpecialId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsDelete')
		DROP PROCEDURE SpecialsDelete
GO

CREATE PROCEDURE SpecialsDelete (
	@SpecialId INT
) AS
BEGIN

	DELETE FROM Specials WHERE SpecialId = @SpecialId;

END
GO

-- ADDRESS REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddressesSelectAll')
		DROP PROCEDURE AddressesSelectAll
GO

CREATE PROCEDURE AddressesSelectAll AS
BEGIN
	SELECT * FROM Addresses
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddressesSelectById')
		DROP PROCEDURE AddressesSelectById
GO

CREATE PROCEDURE AddressesSelectById (
	@AddressId INT
) AS
BEGIN
	SELECT *
	FROM Addresses
	WHERE AddressId = @AddressId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddressesInsert')
		DROP PROCEDURE AddressesInsert
GO

CREATE PROCEDURE AddressesInsert (
	@AddressId INT OUTPUT,
	@StateId CHAR(2),
	@Street1 NVARCHAR(50),
	@Street2 NVARCHAR(50),
	@City NVARCHAR(50),
	@Zipcode NVARCHAR(9)
) AS
BEGIN

	INSERT INTO Addresses (StateId, Street1, Street2, City, Zipcode)
	VALUES (@StateId, @Street1, @Street2, @City, @Zipcode)
	SET @AddressId = SCOPE_IDENTITY();

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddressesUpdate')
		DROP PROCEDURE AddressesUpdate
GO

CREATE PROCEDURE AddressesUpdate (
	@AddressId INT,
	@StateId CHAR(2),
	@Street1 NVARCHAR(50),
	@Street2 NVARCHAR(50),
	@City NVARCHAR(50),
	@Zipcode NVARCHAR(9)
) AS
BEGIN
	BEGIN TRANSACTION

	UPDATE Addresses SET
	StateId = @StateId,
	Street1 = @Street1,
	Street2 = @Street2,
	City = @City,
	Zipcode = @Zipcode
	WHERE AddressId = @AddressId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddressesDelete')
		DROP PROCEDURE AddressesDelete
GO

CREATE PROCEDURE AddressesDelete (
	@AddressId INT
) AS
BEGIN

	DELETE FROM Addresses WHERE AddressId = @AddressId;

END
GO

-- CONTACT REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactsSelectAll')
		DROP PROCEDURE ContactsSelectAll
GO

CREATE PROCEDURE ContactsSelectAll AS
BEGIN
	SELECT * FROM Contacts
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactsSelectById')
		DROP PROCEDURE ContactsSelectById
GO

CREATE PROCEDURE ContactsSelectById (
	@ContactId INT
) AS
BEGIN
	SELECT *
	FROM Contacts
	WHERE ContactId = @ContactId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactsInsert')
		DROP PROCEDURE ContactsInsert
GO

CREATE PROCEDURE ContactsInsert (
	@ContactId INT OUTPUT,
	@CustomerId INT,
	@ContactMessage NVARCHAR(500)
) AS
BEGIN

	INSERT INTO Contacts (CustomerId, ContactMessage)
	VALUES (@CustomerId, @ContactMessage)
	SET @ContactId = SCOPE_IDENTITY();

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactsDelete')
		DROP PROCEDURE ContactsDelete
GO

CREATE PROCEDURE ContactsDelete (
	@ContactId INT
) AS
BEGIN

	DELETE FROM Contacts WHERE ContactId = @ContactId;

END
GO

-- CUSTOMER REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersSelectAll')
		DROP PROCEDURE CustomersSelectAll
GO

CREATE PROCEDURE CustomersSelectAll AS
BEGIN
	SELECT * FROM Customers
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersSelectById')
		DROP PROCEDURE CustomersSelectById
GO

CREATE PROCEDURE CustomersSelectById (
	@CustomerId INT
) AS
BEGIN
	SELECT *
	FROM Customers
	WHERE CustomerId = @CustomerId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersInsert')
		DROP PROCEDURE CustomersInsert
GO

CREATE PROCEDURE CustomersInsert (
	@CustomerId INT OUTPUT,
	@AddressId INT,
	@CustomerFirstName NVARCHAR(50),
	@CustomerLastName NVARCHAR(50),
	@CustomerPhone NVARCHAR(12),
	@CustomerEmail NVARCHAR(50)
) AS
BEGIN
	INSERT INTO Customers (AddressId, CustomerFirstName, CustomerLastName, CustomerPhone, CustomerEmail)
	VALUES (@AddressId, @CustomerFirstName, @CustomerLastName, @CustomerPhone, @CustomerEmail)
	SET @CustomerId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersUpdate')
		DROP PROCEDURE CustomersUpdate
GO

CREATE PROCEDURE CustomersUpdate (
	@CustomerId INT,
	@AddressId INT,
	@CustomerFirstName NVARCHAR(50),
	@CustomerLastName NVARCHAR(50),
	@CustomerPhone NVARCHAR(12),
	@CustomerEmail NVARCHAR(50)
) AS
BEGIN
	BEGIN TRANSACTION

	UPDATE Customers SET
	AddressId = @AddressId,
	CustomerFirstName = @CustomerFirstName,
	CustomerLastName = @CustomerLastName,
	CustomerPhone = @CustomerPhone,
	CustomerEmail = @CustomerEmail
	WHERE CustomerId = @CustomerId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersDelete')
		DROP PROCEDURE CustomersDelete
GO

CREATE PROCEDURE CustomersDelete (
	@CustomerId INT
) AS
BEGIN

	DELETE FROM Customers WHERE CustomerId = @CustomerId;

END
GO

-- PURCHASE REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchasesSelectAll')
		DROP PROCEDURE PurchasesSelectAll
GO

CREATE PROCEDURE PurchasesSelectAll AS
BEGIN
	SELECT * FROM Purchases;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchasesSelectById')
		DROP PROCEDURE PurchasesSelectById
GO

CREATE PROCEDURE PurchasesSelectById(
	@PurchaseId INT
) AS
BEGIN
	SELECT *
	FROM Purchases
	WHERE PurchaseId = @PurchaseId;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchasesInsert')
		DROP PROCEDURE PurchasesInsert
GO

CREATE PROCEDURE PurchasesInsert(
	@PurchaseId INT OUTPUT,
	@CustomerId INT,
	@PurchaseTypeId INT,
	@VehicleId INT,
	@PurchasePrice DECIMAL(8,2),
	@PurchaseDate DATETIME2(0),
	@SoldByEmail NVARCHAR(50)
) AS
BEGIN
	INSERT INTO Purchases (CustomerId, PurchaseTypeId, VehicleId, PurchasePrice, PurchaseDate, SoldByEmail)
	VALUES (@CustomerId, @PurchaseTypeId, @VehicleId, @PurchasePrice, @PurchaseDate, @SoldByEmail)
	SET @PurchaseId = SCOPE_IDENTITY();
END
GO

-- REPORTS REPOSITORY SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InventoryReportsSelectByVehicleType')
		DROP PROCEDURE InventoryReportsSelectByVehicleType
GO

CREATE PROCEDURE InventoryReportsSelectByVehicleType (
	@VehicleTypeId INT
) AS
BEGIN

	SELECT v.[Year], ma.MakeName, m.ModelName, COUNT(v.ModelId) [Count], SUM(v.Msrp) StockValue
	FROM Vehicles v
		INNER JOIN Models m ON v.ModelId = m.ModelId
		INNER JOIN Makes ma ON m.MakeId = ma.MakeId
	WHERE v.VehicleTypeId = @VehicleTypeId
	GROUP BY v.[Year], ma.MakeName, m.ModelName
	ORDER BY v.[Year], ma.MakeName, m.ModelName;

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SalesReportsSelectAll')
		DROP PROCEDURE SalesReportsSelectAll
GO

CREATE PROCEDURE SalesReportsSelectAll AS
BEGIN

	SELECT CONCAT(u.FirstName, ' ', u.LastName) [Name],
	SUM(p.PurchasePrice) TotalSales,
	COUNT(p.PurchaseId) TotalVehicles
	FROM Purchases p
		INNER JOIN AspNetUsers u ON p.SoldByEmail LIKE u.Email
	GROUP BY u.Id, u.FirstName, u.LastName
	ORDER BY SUM(p.PurchasePrice) DESC;

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SalesReportsSelectBySearch')
		DROP PROCEDURE SalesReportsSelectBySearch
GO

CREATE PROCEDURE SalesReportsSelectBySearch(
	@UserEmail NVARCHAR(50),
	@MinDate DATETIME2(0),
	@MaxDate DATETIME2(0)
) AS
BEGIN

	SELECT CONCAT(u.FirstName, ' ', u.LastName) [Name],
	SUM(p.PurchasePrice) TotalSales,
	COUNT(p.PurchaseId) TotalVehicles
	FROM Purchases p
		INNER JOIN AspNetUsers u ON p.SoldByEmail LIKE u.Email
	WHERE (@UserEmail IS NULL OR u.Email LIKE '%' + @UserEmail + '%')
		AND (@MinDate IS NULL OR p.PurchaseDate > @MinDate)
		AND (@MaxDate IS NULL OR p.PurchaseDate < @MaxDate)
	GROUP BY u.Id, u.FirstName, u.LastName
	ORDER BY SUM(p.PurchasePrice) DESC;

END
GO

-- SELECT LIST REPOSITORIES SPROCS

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BodyStylesSelectAll')
		DROP PROCEDURE BodyStylesSelectAll
GO

CREATE PROCEDURE BodyStylesSelectAll AS
BEGIN
	SELECT * FROM BodyStyles;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ColorsSelectAll')
		DROP PROCEDURE ColorsSelectAll
GO

CREATE PROCEDURE ColorsSelectAll AS
BEGIN
	SELECT * FROM Colors;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InteriorsSelectAll')
		DROP PROCEDURE InteriorsSelectAll
GO

CREATE PROCEDURE InteriorsSelectAll AS
BEGIN
	SELECT * FROM Interiors;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchaseTypesSelectAll')
		DROP PROCEDURE PurchaseTypesSelectAll
GO

CREATE PROCEDURE PurchaseTypesSelectAll AS
BEGIN
	SELECT * FROM PurchaseTypes;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'StatesSelectAll')
		DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT * FROM States;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TransmissionsSelectAll')
		DROP PROCEDURE TransmissionsSelectAll
GO

CREATE PROCEDURE TransmissionsSelectAll AS
BEGIN
	SELECT * FROM Transmissions;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehicleTypesSelectAll')
		DROP PROCEDURE VehicleTypesSelectAll
GO

CREATE PROCEDURE VehicleTypesSelectAll AS
BEGIN
	SELECT * FROM VehicleTypes;
END
GO