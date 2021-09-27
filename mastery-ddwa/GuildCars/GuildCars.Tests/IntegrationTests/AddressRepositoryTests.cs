using GuildCars.Data.ADO;
using GuildCars.Models.Tables;
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
    public class AddressRepositoryTests
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
        public void CanGetAll()
        {
            var repo = new AddressRepository();

            var addresses = repo.GetAll().ToList();

            Assert.AreEqual(2, addresses.Count);
            Assert.AreEqual(1, addresses[0].AddressId);
            Assert.AreEqual(2, addresses[1].AddressId);
        }

        [Test]
        public void CanGetById()
        {
            var repo = new AddressRepository();

            var address = repo.GetById(1);

            Assert.AreEqual(1, address.AddressId);
            Assert.AreEqual("MN", address.StateId);
            Assert.AreEqual("Test Dr.", address.Street1);
            Assert.AreEqual("Apt. 1", address.Street2);
            Assert.AreEqual("Testopolis", address.City);
            Assert.AreEqual("55555", address.Zipcode);
        }

        [Test]
        public void CanInsert()
        {
            var repo = new AddressRepository();

            Address address = new Address();
            address.StateId = "OH";
            address.Street1 = "123 Fake St.";
            address.Street2 = "Apt. 3";
            address.City = "Columbus";
            address.Zipcode = "44444";

            repo.Insert(address);

            Assert.AreEqual(3, address.AddressId);

            var results = repo.GetById(3);

            Assert.AreEqual(3, results.AddressId);
            Assert.AreEqual("OH", results.StateId);
            Assert.AreEqual("123 Fake St.", results.Street1);
            Assert.AreEqual("Apt. 3", results.Street2);
            Assert.AreEqual("Columbus", results.City);
            Assert.AreEqual("44444", results.Zipcode);
        }

        [Test]
        public void CanInsertNullValues()
        {
            var repo = new AddressRepository();

            Address address = new Address();
            address.StateId = "OH";
            address.Street1 = "123 Fake St.";
            address.Street2 = null;
            address.City = "Columbus";
            address.Zipcode = "44444";

            repo.Insert(address);

            Assert.AreEqual(3, address.AddressId);

            var results = repo.GetById(3);

            Assert.AreEqual(3, results.AddressId);
            Assert.AreEqual("OH", results.StateId);
            Assert.AreEqual("123 Fake St.", results.Street1);
            Assert.AreEqual("", results.Street2);
            Assert.AreEqual("Columbus", results.City);
            Assert.AreEqual("44444", results.Zipcode);
        }

        [Test]
        public void CanUpdate()
        {
            var repo = new AddressRepository();

            Address address = new Address();
            address.StateId = "OH";
            address.Street1 = "123 Fake St.";
            address.Street2 = "Apt. 3";
            address.City = "Columbus";
            address.Zipcode = "44444";

            repo.Insert(address);

            Assert.AreEqual(3, address.AddressId);

            address.StateId = "WI";
            address.Street1 = "Main St.";
            address.Street2 = "Unit 1";
            address.City = "Milwaukee";
            address.Zipcode = "33333";

            repo.Update(address);

            var results = repo.GetById(3);

            Assert.AreEqual("WI", results.StateId);
            Assert.AreEqual("Main St.", results.Street1);
            Assert.AreEqual("Unit 1", results.Street2);
            Assert.AreEqual("Milwaukee", results.City);
            Assert.AreEqual("33333", results.Zipcode);
        }

        [Test]
        public void CanUpdateNullValues()
        {
            var repo = new AddressRepository();

            Address address = new Address();
            address.StateId = "OH";
            address.Street1 = "123 Fake St.";
            address.Street2 = "Apt. 3";
            address.City = "Columbus";
            address.Zipcode = "44444";

            repo.Insert(address);

            Assert.AreEqual(3, address.AddressId);

            address.StateId = "WI";
            address.Street1 = "Main St.";
            address.Street2 = null;
            address.City = "Milwaukee";
            address.Zipcode = "33333";

            repo.Update(address);

            var results = repo.GetById(3);

            Assert.AreEqual("WI", results.StateId);
            Assert.AreEqual("Main St.", results.Street1);
            Assert.AreEqual("", results.Street2);
            Assert.AreEqual("Milwaukee", results.City);
            Assert.AreEqual("33333", results.Zipcode);
        }

        [Test]
        public void CanDelete()
        {
            var repo = new AddressRepository();

            Address address = new Address();
            address.StateId = "OH";
            address.Street1 = "123 Fake St.";
            address.Street2 = "Apt. 3";
            address.City = "Columbus";
            address.Zipcode = "44444";

            repo.Insert(address);

            Assert.AreEqual(3, address.AddressId);

            repo.Delete(3);

            Assert.IsNull(repo.GetById(3));
        }
    }
}
