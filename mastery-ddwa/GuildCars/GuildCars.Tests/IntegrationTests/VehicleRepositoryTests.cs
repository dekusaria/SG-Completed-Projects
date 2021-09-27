using GuildCars.Data.ADO;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using GuildCars.Models.Queries;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class VehicleRepositoryTests
    {
        // This SetUp uses Stored Procedures that can and WILL wipe data!
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "GuildCarsDbReset";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();

                cmd.CommandText = "GuildCarsTestData";
                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadAllVehicles()
        {
            var repo = new VehicleRepository();
            var vehicles = repo.GetAll().ToList();

            Assert.AreEqual(4, vehicles.Count);
            Assert.AreEqual(1, vehicles[0].VehicleId);
            Assert.AreEqual(2, vehicles[1].VehicleId);
            Assert.AreEqual(3, vehicles[2].VehicleId);
            Assert.AreEqual(4, vehicles[3].VehicleId);
        }

        [Test]
        public void CanLoadVehicleById()
        {
            var repo = new VehicleRepository();
            Vehicle vehicle = repo.GetById(1);

            Assert.IsNotNull(vehicle);
            Assert.AreEqual(1, vehicle.VehicleId);
            Assert.AreEqual("AAAA1111BBBB2222", vehicle.Vin);
            Assert.AreEqual(1, vehicle.VehicleTypeId);
            Assert.AreEqual(1, vehicle.ModelId);
            Assert.AreEqual(2, vehicle.ColorId);
            Assert.AreEqual(4, vehicle.InteriorId);
            Assert.AreEqual(2, vehicle.BodyStyleId);
            Assert.AreEqual(1, vehicle.TransmissionId);
            Assert.AreEqual(2021, vehicle.Year);
            Assert.AreEqual(0, vehicle.Mileage);
            Assert.AreEqual(25000M, vehicle.Msrp);
            Assert.AreEqual(21000M, vehicle.SalePrice);
            Assert.AreEqual("The CR-V offers miles of driving fun, with all-wheel drive, 212 total system horsepower, and a 40-mpg city rating.", vehicle.Description);
            Assert.AreEqual("testImage.png", vehicle.ImageFileName);
            Assert.AreEqual(true, vehicle.IsFeatured);
            Assert.AreEqual(false, vehicle.IsSold);
        }

        [Test]
        public void CanInsertVehicle()
        {
            var repo = new VehicleRepository();
            Vehicle vehicle = new Vehicle();

            vehicle.Vin = "ZZZZ0000YYYY9999";
            vehicle.VehicleTypeId = 2;
            vehicle.ModelId = 1;
            vehicle.ColorId = 5;
            vehicle.InteriorId = 1;
            vehicle.BodyStyleId = 2;
            vehicle.TransmissionId = 2;
            vehicle.Year = 2019;
            vehicle.Mileage = 5000;
            vehicle.Msrp = 20000M;
            vehicle.SalePrice = 18000M;
            vehicle.Description = "Another CRV, this time - Used!";
            vehicle.ImageFileName = "testImage.png";
            vehicle.IsFeatured = false;
            vehicle.IsSold = false;

            repo.Insert(vehicle);

            Assert.AreEqual(5, vehicle.VehicleId);

            Vehicle resposne = repo.GetById(5);

            Assert.AreEqual("ZZZZ0000YYYY9999", resposne.Vin);
            Assert.AreEqual(2, resposne.VehicleTypeId);
            Assert.AreEqual(1, resposne.ModelId);
            Assert.AreEqual(5, resposne.ColorId);
            Assert.AreEqual(1, resposne.InteriorId);
            Assert.AreEqual(2, resposne.BodyStyleId);
            Assert.AreEqual(2, resposne.TransmissionId);
            Assert.AreEqual(2019, resposne.Year);
            Assert.AreEqual(5000, resposne.Mileage);
            Assert.AreEqual(20000M, resposne.Msrp);
            Assert.AreEqual(18000M, resposne.SalePrice);
            Assert.AreEqual("Another CRV, this time - Used!", resposne.Description);
            Assert.AreEqual("testImage.png", resposne.ImageFileName);
            Assert.AreEqual(false, resposne.IsFeatured);
            Assert.AreEqual(false, resposne.IsSold);
        }

        [Test]
        public void CanUpdateVehicle()
        {
            var repo = new VehicleRepository();
            Vehicle vehicle = new Vehicle();

            vehicle.Vin = "ZZZZ0000YYYY9999";
            vehicle.VehicleTypeId = 2;
            vehicle.ModelId = 1;
            vehicle.ColorId = 5;
            vehicle.InteriorId = 1;
            vehicle.BodyStyleId = 2;
            vehicle.TransmissionId = 2;
            vehicle.Year = 2019;
            vehicle.Mileage = 5000;
            vehicle.Msrp = 20000M;
            vehicle.SalePrice = 18000M;
            vehicle.Description = "Another CRV, this time - Used!";
            vehicle.ImageFileName = "testImage.png";
            vehicle.IsFeatured = false;
            vehicle.IsSold = false;

            repo.Insert(vehicle);

            vehicle.Vin = "XXXX8888WWWW7777";
            vehicle.VehicleTypeId = 1;
            vehicle.ModelId = 2;
            vehicle.ColorId = 4;
            vehicle.InteriorId = 2;
            vehicle.BodyStyleId = 1;
            vehicle.TransmissionId = 1;
            vehicle.Year = 2018;
            vehicle.Mileage = 6000;
            vehicle.Msrp = 21000M;
            vehicle.SalePrice = 19000M;
            vehicle.Description = "Just kidding! It's a Jeep now!";
            vehicle.ImageFileName = "testImage1.png";
            vehicle.IsFeatured = true;
            vehicle.IsSold = true;

            repo.Update(vehicle);

            Vehicle resposne = repo.GetById(5);

            Assert.AreEqual("XXXX8888WWWW7777", resposne.Vin);
            Assert.AreEqual(1, resposne.VehicleTypeId);
            Assert.AreEqual(2, resposne.ModelId);
            Assert.AreEqual(4, resposne.ColorId);
            Assert.AreEqual(2, resposne.InteriorId);
            Assert.AreEqual(1, resposne.BodyStyleId);
            Assert.AreEqual(1, resposne.TransmissionId);
            Assert.AreEqual(2018, resposne.Year);
            Assert.AreEqual(6000, resposne.Mileage);
            Assert.AreEqual(21000M, resposne.Msrp);
            Assert.AreEqual(19000M, resposne.SalePrice);
            Assert.AreEqual("Just kidding! It's a Jeep now!", resposne.Description);
            Assert.AreEqual("testImage1.png", resposne.ImageFileName);
            Assert.AreEqual(true, resposne.IsFeatured);
            Assert.AreEqual(true, resposne.IsSold);
        }

        [Test]
        public void CanDeleteVehicle()
        {
            var repo = new VehicleRepository();
            Vehicle vehicle = new Vehicle();

            vehicle.Vin = "ZZZZ0000YYYY9999";
            vehicle.VehicleTypeId = 2;
            vehicle.ModelId = 1;
            vehicle.ColorId = 5;
            vehicle.InteriorId = 1;
            vehicle.BodyStyleId = 2;
            vehicle.TransmissionId = 2;
            vehicle.Year = 2019;
            vehicle.Mileage = 5000;
            vehicle.Msrp = 20000M;
            vehicle.SalePrice = 18000M;
            vehicle.Description = "Another CRV, this time - Used!";
            vehicle.ImageFileName = "testImage.png";
            vehicle.IsFeatured = false;
            vehicle.IsSold = false;

            repo.Insert(vehicle);

            Assert.AreEqual(5, vehicle.VehicleId);

            repo.Delete(5);

            var allVehicles = repo.GetAll().ToList();

            Assert.AreEqual(4, allVehicles.Count);

            var response = repo.GetById(5);
            Assert.IsNull(response);
        }

        [Test]
        public void CanSelectVehiclesByVehicleType()
        {
            var repo = new VehicleRepository();
            var newVehicles = repo.GetByVehicleType(1).ToList();

            Assert.AreEqual(2, newVehicles.Count);

            var usedVehicles = repo.GetByVehicleType(2).ToList();

            Assert.AreEqual(2, usedVehicles.Count);

            var allVehicles = repo.GetByVehicleType(0).ToList();

            Assert.AreEqual(4, allVehicles.Count);

            Vehicle vehicleNew = new Vehicle();

            vehicleNew.Vin = "XXXX8888WWWW7777";
            vehicleNew.VehicleTypeId = 1;
            vehicleNew.ModelId = 2;
            vehicleNew.ColorId = 4;
            vehicleNew.InteriorId = 2;
            vehicleNew.BodyStyleId = 1;
            vehicleNew.TransmissionId = 1;
            vehicleNew.Year = 2018;
            vehicleNew.Mileage = 6000;
            vehicleNew.Msrp = 21000M;
            vehicleNew.SalePrice = 19000M;
            vehicleNew.Description = "Just kidding! It's a Jeep now!";
            vehicleNew.ImageFileName = "testImage1.png";
            vehicleNew.IsFeatured = true;
            vehicleNew.IsSold = true;

            repo.Insert(vehicleNew);

            newVehicles = repo.GetByVehicleType(1).ToList();

            Assert.AreEqual(3, newVehicles.Count);

            Vehicle vehicleUsed = new Vehicle();

            vehicleUsed.Vin = "ZZZZ0000YYYY9999";
            vehicleUsed.VehicleTypeId = 2;
            vehicleUsed.ModelId = 1;
            vehicleUsed.ColorId = 5;
            vehicleUsed.InteriorId = 1;
            vehicleUsed.BodyStyleId = 2;
            vehicleUsed.TransmissionId = 2;
            vehicleUsed.Year = 2019;
            vehicleUsed.Mileage = 5000;
            vehicleUsed.Msrp = 20000M;
            vehicleUsed.SalePrice = 18000M;
            vehicleUsed.Description = "Another CRV, this time - Used!";
            vehicleUsed.ImageFileName = "testImage.png";
            vehicleUsed.IsFeatured = false;
            vehicleUsed.IsSold = false;

            repo.Insert(vehicleUsed);

            usedVehicles = repo.GetByVehicleType(2).ToList();

            Assert.AreEqual(3, usedVehicles.Count);
        }

        [Test]
        public void CanGetVehicleDetails()
        {
            var repo = new VehicleRepository();
            VehicleItem vehicle = repo.GetDetails(1);

            Assert.AreEqual("AAAA1111BBBB2222", vehicle.Vin);
            Assert.AreEqual("New", vehicle.VehicleTypeName);
            Assert.AreEqual("CRV", vehicle.ModelName);
            Assert.AreEqual("Honda", vehicle.MakeName);
            Assert.AreEqual("Black", vehicle.ColorName);
            Assert.AreEqual("Black Leather", vehicle.InteriorType);
            Assert.AreEqual("SUV", vehicle.BodyStyleType);
            Assert.AreEqual("Automatic", vehicle.TransmissionType);
            Assert.AreEqual(2021, vehicle.Year);
            Assert.AreEqual(0, vehicle.Mileage);
            Assert.AreEqual(25000M, vehicle.Msrp);
            Assert.AreEqual(21000M, vehicle.SalePrice);
            Assert.AreEqual("The CR-V offers miles of driving fun, with all-wheel drive, 212 total system horsepower, and a 40-mpg city rating.", vehicle.Description);
            Assert.AreEqual("testImage.png", vehicle.ImageFileName);
            Assert.AreEqual(true, vehicle.IsFeatured);
            Assert.AreEqual(false, vehicle.IsSold);
        }

        [Test]
        public void CanGetFeaturedVehicles()
        {
            var repo = new VehicleRepository();
            var vehicles = repo.GetFeaturedVehicles().ToList();

            Assert.AreEqual(2, vehicles.Count);

            Vehicle vehicleNew = new Vehicle();

            vehicleNew.Vin = "XXXX8888WWWW7777";
            vehicleNew.VehicleTypeId = 1;
            vehicleNew.ModelId = 2;
            vehicleNew.ColorId = 4;
            vehicleNew.InteriorId = 2;
            vehicleNew.BodyStyleId = 1;
            vehicleNew.TransmissionId = 1;
            vehicleNew.Year = 2018;
            vehicleNew.Mileage = 6000;
            vehicleNew.Msrp = 21000M;
            vehicleNew.SalePrice = 19000M;
            vehicleNew.Description = "Just kidding! It's a Jeep now!";
            vehicleNew.ImageFileName = "testImage1.png";
            vehicleNew.IsFeatured = true;
            vehicleNew.IsSold = false;

            repo.Insert(vehicleNew);

            vehicles = repo.GetFeaturedVehicles().ToList();

            Assert.AreEqual(3, vehicles.Count);

            Vehicle vehicleUsed = new Vehicle();

            vehicleUsed.Vin = "ZZZZ0000YYYY9999";
            vehicleUsed.VehicleTypeId = 2;
            vehicleUsed.ModelId = 1;
            vehicleUsed.ColorId = 5;
            vehicleUsed.InteriorId = 1;
            vehicleUsed.BodyStyleId = 2;
            vehicleUsed.TransmissionId = 2;
            vehicleUsed.Year = 2019;
            vehicleUsed.Mileage = 5000;
            vehicleUsed.Msrp = 20000M;
            vehicleUsed.SalePrice = 18000M;
            vehicleUsed.Description = "Another CRV, this time - Used!";
            vehicleUsed.ImageFileName = "testImage.png";
            vehicleUsed.IsFeatured = true;
            vehicleUsed.IsSold = true;

            repo.Insert(vehicleUsed);

            vehicles = repo.GetFeaturedVehicles().ToList();

            Assert.AreEqual(3, vehicles.Count);
        }

        [Test]
        public void CanSearchForVehicles()
        {
            var repo = new VehicleRepository();

            VehicleSearchParameters param1 = new VehicleSearchParameters();
            param1.VehicleTypeId = 1;
            param1.SearchTerm = "CRV";
            param1.MinPrice = 18000M;
            param1.MaxPrice = 24000M;
            param1.MinYear = 2019;
            param1.MaxYear = 2021;

            var vehicles = repo.SearchVehicles(param1).ToList();

            Assert.AreEqual(1, vehicles.Count);

            VehicleSearchParameters param2 = new VehicleSearchParameters();
            param2.VehicleTypeId = 2;
            param2.SearchTerm = "2021";

            vehicles = repo.SearchVehicles(param2).ToList();

            Assert.AreEqual(1, vehicles.Count);

            VehicleSearchParameters param3 = new VehicleSearchParameters();
            param3.VehicleTypeId = 1;
            param3.SearchTerm = "CRV";

            vehicles = repo.SearchVehicles(param3).ToList();

            Assert.AreEqual(1, vehicles.Count);

            VehicleSearchParameters param4 = new VehicleSearchParameters();
            param4.VehicleTypeId = 2;
            param4.SearchTerm = "Grand C";

            vehicles = repo.SearchVehicles(param4).ToList();

            Assert.AreEqual(2, vehicles.Count);

            VehicleSearchParameters param5 = new VehicleSearchParameters();
            param5.SearchTerm = "Honda";

            vehicles = repo.SearchVehicles(param5).ToList();

            Assert.AreEqual(1, vehicles.Count);

            VehicleSearchParameters param6 = new VehicleSearchParameters();
            param6.MaxYear = 2015;

            vehicles = repo.SearchVehicles(param6).ToList();

            Assert.AreEqual(1, vehicles.Count);

            VehicleSearchParameters param7 = new VehicleSearchParameters();

            vehicles = repo.SearchVehicles(param7).ToList();

            Assert.AreEqual(4, vehicles.Count);

            VehicleSearchParameters param8 = new VehicleSearchParameters();
            param8.SearchTerm = "BANANA OOOO YUMMY";

            vehicles = repo.SearchVehicles(param8).ToList();

            Assert.AreEqual(0, vehicles.Count);
        }

        [Test]
        public void CanUpdateIsSold()
        {
            var repo = new VehicleRepository();

            var param = new VehicleSearchParameters();

            var inventory = repo.SearchVehicles(param);

            Assert.AreEqual(4, inventory.Count());

            repo.VehicleIsSold(1);

            inventory = repo.SearchVehicles(param);

            Assert.AreEqual(3, inventory.Count());
        }
    }
}
