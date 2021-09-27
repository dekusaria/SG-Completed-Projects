USE GuildCars;
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GuildCarsDbReset')
		DROP PROCEDURE GuildCarsDbReset
GO

CREATE PROCEDURE GuildCarsDbReset AS
BEGIN

	DELETE FROM Purchases;
	DELETE FROM Specials;
	DELETE FROM Contacts;
	DELETE FROM Customers;
	DELETE FROM Addresses;
	DELETE FROM States;
	DELETE FROM PurchaseTypes;
	DELETE FROM Vehicles;
	DELETE FROM Interiors;
	DELETE FROM Colors;
	DELETE FROM VehicleTypes;
	DELETE FROM Transmissions;
	DELETE FROM BodyStyles;
	DELETE FROM Models;
	DELETE FROM Makes;

	DBCC CHECKIDENT('vehicles', RESEED, 1)
	DBCC CHECKIDENT('purchases', RESEED, 1)
	DBCC CHECKIDENT('makes', RESEED, 1)
	DBCC CHECKIDENT('models', RESEED, 1)
	DBCC CHECKIDENT('specials', RESEED, 1)
	DBCC CHECKIDENT('contacts', RESEED, 1)
	DBCC CHECKIDENT('customers', RESEED, 1)
	DBCC CHECKIDENT('addresses', RESEED, 1)

	SET IDENTITY_INSERT BodyStyles ON;
	INSERT INTO BodyStyles (BodyStyleId, BodyStyleType)
	VALUES (1, 'Car'),
	(2, 'SUV'),
	(3, 'Truck'),
	(4, 'Van')
	SET IDENTITY_INSERT BodyStyles OFF;

	SET IDENTITY_INSERT Transmissions ON;
	INSERT INTO Transmissions (TransmissionId, TransmissionType)
	VALUES (1, 'Automatic'),
	(2, 'Manual')
	SET IDENTITY_INSERT Transmissions OFF;

	SET IDENTITY_INSERT VehicleTypes ON;
	INSERT INTO VehicleTypes (VehicleTypeId, VehicleTypeName)
	VALUES (1, 'New'),
	(2, 'Used')
	SET IDENTITY_INSERT VehicleTypes OFF;

	SET IDENTITY_INSERT Colors ON;
	INSERT INTO Colors (ColorId, ColorName)
	VALUES (1, 'White'),
	(2, 'Black'),
	(3, 'Gray'),
	(4, 'Silver'),
	(5, 'Red'),
	(6, 'Blue'),
	(7, 'Tan'),
	(8, 'Brown'),
	(9, 'Green'),
	(10, 'Orange'),
	(11, 'Gold'),
	(12, 'Yellow'),
	(13, 'Purple')
	SET IDENTITY_INSERT Colors OFF;

	SET IDENTITY_INSERT Interiors ON;
	INSERT INTO Interiors (InteriorId, InteriorType)
	VALUES (1, 'Black'),
	(2, 'Tan'),
	(3, 'Gray'),
	(4, 'Black Leather'),
	(5, 'Brown Leather'),
	(6, 'Tan Leather')
	SET IDENTITY_INSERT Interiors OFF;

	SET IDENTITY_INSERT PurchaseTypes ON;
	INSERT INTO PurchaseTypes (PurchaseTypeId, PurchaseTypeName)
	VALUES (1, 'Bank Finance'),
	(2, 'Cash'),
	(3, 'Dealer Finance')
	SET IDENTITY_INSERT PurchaseTypes OFF;

	INSERT INTO States (StateId, StateName)
	VALUES ('AK', 'Alaska'),
	('AL', 'Alabama'),
	('AZ', 'Arizona'),
	('AR', 'Arkansas'),
	('CA', 'California'),
	('CO', 'Colorado'),
	('CT', 'Connecticut'),
	('DE', 'Delaware'),
	('FL', 'Florida'),
	('GA', 'Georgia'),
	('HI', 'Hawaii'),
	('ID', 'Idaho'),
	('IL', 'Illinois'),
	('IN', 'Indiana'),
	('IA', 'Iowa'),
	('KS', 'Kansas'),
	('KY', 'Kentucky'),
	('LA', 'Louisiana'),
	('ME', 'Maine'),
	('MD', 'Maryland'),
	('MA', 'Massachusetts'),
	('MI', 'Michigan'),
	('MN', 'Minnesota'),
	('MS', 'Mississippi'),
	('MO', 'Missouri'),
	('MT', 'Montana'),
	('NE', 'Nebraska'),
	('NV', 'Nevada'),
	('NH', 'New Hampshire'),
	('NJ', 'New Jersey'),
	('NM', 'New Mexico'),
	('NY', 'New York'),
	('NC', 'North Carolina'),
	('ND', 'North Dakota'),
	('OH', 'Ohio'),
	('OK', 'Oklahoma'),
	('OR', 'Oregon'),
	('PA', 'Pennsylvania'),
	('RI', 'Rhode Island'),
	('SC', 'South Carolina'),
	('SD', 'South Dakota'),
	('TN', 'Tennessee'),
	('TX', 'Texas'),
	('UT', 'Utah'),
	('VT', 'Vermont'),
	('VA', 'Virginia'),
	('WA', 'Washington'),
	('WV', 'West Virginia'),
	('WI', 'Wisconsin'),
	('WY', 'Wyoming')

END
GO

EXEC GuildCarsDbReset;