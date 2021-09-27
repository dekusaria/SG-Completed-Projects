USE GuildCars;
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GuildCarsTestData')
		DROP PROCEDURE GuildCarsTestData
GO

CREATE PROCEDURE GuildCarsTestData AS
BEGIN

	SET IDENTITY_INSERT Addresses ON;
	INSERT INTO Addresses (AddressId, StateId, Street1, Street2, City, Zipcode)
	VALUES (1, 'MN', 'Test Dr.', 'Apt. 1', 'Testopolis', '55555'),
	(2, 'OH', 'Fake Street', NULL, 'Columbus', '44444')
	SET IDENTITY_INSERT Addresses OFF;

	SET IDENTITY_INSERT Customers ON;
	INSERT INTO Customers (CustomerId, AddressId, CustomerFirstName, CustomerLastName, CustomerPhone, CustomerEmail)
	VALUES (1, 1, 'Testy', 'McTestington', '555-555-5555', 'customer@test.com'),
	(2, 2, 'John', 'Jackson', '444-444-4444', 'john@jackson.com')
	SET IDENTITY_INSERT Customers OFF;

	SET IDENTITY_INSERT Contacts ON;
	INSERT INTO Contacts (ContactId, CustomerId, ContactMessage)
	VALUES (1, 1, 'Hello, World!'),
	(2, 2, 'Can you hear me?')
	SET IDENTITY_INSERT Contacts OFF;

	SET IDENTITY_INSERT Makes ON;
	INSERT INTO Makes (MakeId, MakeName, DateAdded, EmployeeEmail)
	VALUES (1, 'Honda', '8-3-2021', 'employee1@email.com'),
	(2, 'Jeep', '10-31-2921', 'employee2@email.com')
	SET IDENTITY_INSERT Makes OFF;

	SET IDENTITY_INSERT Models ON;
	INSERT INTO Models (ModelId, MakeId, ModelName, DateAdded, EmployeeEmail)
	VALUES (1, 1, 'CRV', '8-3-2021', 'employee1@email.com'),
	(2, 2, 'Grand Cherokee', '10-31-2021', 'employee2@email.com')
	SET IDENTITY_INSERT Models OFF;

	SET IDENTITY_INSERT Vehicles ON;
	INSERT INTO Vehicles (VehicleId, Vin, VehicleTypeId, ModelId, ColorId, InteriorId, BodyStyleId, TransmissionId,
		[Year], Mileage, Msrp, SalePrice, [Description], ImageFileName, IsFeatured, IsSold)
	VALUES (1, 'AAAA1111BBBB2222', 1, 1, 2, 4, 2, 1, 2021, 0, 25000.00, 21000.00, 'The CR-V offers miles of driving fun, with all-wheel drive, 212 total system horsepower, and a 40-mpg city rating.',
		'testImage.png', 1, 0),
		(2, 'CCCC3333DDDD4444', 2, 2, 5, 3, 2, 2, 2021, 2000, 32000.00, 30000.00, 'The Jeep Grand Cherokee is a sophisticated SUV with serious power with Best-in-Class towing capacity.',
		'testImage.png', 0, 0),
		(3, 'EEEE5555FFFF6666', 1, 2, 3, 2, 2, 1, 2021, 200, 24000.00, 22000.00, 'The Jeep Grand Cherokee is a sophisticated SUV with serious power with Best-in-Class towing capacity.',
		'testImage.png', 1, 0),
		(4, 'FFFF6666GGGG7777', 2, 2, 6, 5, 2, 2, 2015, 60000, 15000.00, 14000.00, 'This bad boy comes loaded with SOME features.',
		'testImage.png', 0, 0)
	SET IDENTITY_INSERT Vehicles OFF;

		SET IDENTITY_INSERT Specials ON;
	INSERT INTO Specials (SpecialId, SpecialTitle, SpecialDescription)
	VALUES (1, 'Summer Sales Event', 'Bacon ipsum dolor amet rump pariatur non hamburger tri-tip, in biltong. Voluptate 
	porchetta bresaola short loin veniam, shankle dolor ribeye laboris. Esse aliquip turducken, ex pancetta culpa leberkas rump 
	frankfurter venison sirloin jowl laboris.'),
	(2, 'Everything MUST GO!', 'Bacon ipsum pariatur anim, occaecat tenderloin bresaola non excepteur ham hock labore picanha. Pariatur pork chop jerky kielbasa. 
	Boudin chislic mollit flank cupidatat porchetta, adipisicing cupim filet mignon culpa in deserunt strip steak. Qui enim andouille laboris brisket culpa.'),
	(3, 'Infinite Test Drive!', 'Bacon ipsum aliqua pork loin, dolor alcatra cupidatat prosciutto ea. Ham pork loin aute salami meatloaf dolor pariatur dolore minim nulla. 
	Eiusmod biltong velit est meatball leberkas ullamco. Aute ut brisket, dolore rump kevin ea esse jerky et burgdoggen elit cillum.')
	SET IDENTITY_INSERT Specials OFF;

END
GO