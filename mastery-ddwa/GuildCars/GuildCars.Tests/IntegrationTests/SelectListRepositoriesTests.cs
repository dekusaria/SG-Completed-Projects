using GuildCars.Data.ADO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class SelectListRepositoriesTests
    {
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
        public void CanGetAllBodyStyles()
        {
            var repo = new BodyStyleRepository();

            var results = repo.GetAll().ToList();

            Assert.AreEqual(4, results.Count);
            Assert.AreEqual(1, results[0].BodyStyleId);
            Assert.AreEqual("Car", results[0].BodyStyleType);
        }

        [Test]
        public void CanGetAllColors()
        {
            var repo = new ColorsRepository();

            var results = repo.GetAll().ToList();

            Assert.AreEqual(13, results.Count);
            Assert.AreEqual(1, results[0].ColorId);
            Assert.AreEqual("White", results[0].ColorName);
        }

        [Test]
        public void CanGetAllInteriors()
        {
            var repo = new InteriorsRepository();

            var results = repo.GetAll().ToList();

            Assert.AreEqual(6, results.Count);
            Assert.AreEqual(1, results[0].InteriorId);
            Assert.AreEqual("Black", results[0].InteriorType);
        }

        [Test]
        public void CanGetAllPurchaseTypes()
        {
            var repo = new PurchaseTypeRepository();

            var results = repo.GetAll().ToList();

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(1, results[0].PurchaseTypeId);
            Assert.AreEqual("Bank Finance", results[0].PurchaseTypeName);
        }

        [Test]
        public void CanGetAllStates()
        {
            var repo = new StatesRepository();

            var results = repo.GetAll().ToList();

            Assert.AreEqual(50, results.Count);
            Assert.AreEqual("AK", results[0].StateId);
            Assert.AreEqual("Alaska", results[0].StateName);
        }

        [Test]
        public void CanGetAllTransmissions()
        {
            var repo = new TransmissionRepository();

            var results = repo.GetAll().ToList();

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(1, results[0].TransmissionId);
            Assert.AreEqual("Automatic", results[0].TransmissionType);
        }

        [Test]
        public void CanGetAllVehicleTypes()
        {
            var repo = new VehicleTypeRepository();

            var results = repo.GetAll().ToList();

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(1, results[0].VehicleTypeId);
            Assert.AreEqual("New", results[0].VehicleTypeName);
        }
    }
}
