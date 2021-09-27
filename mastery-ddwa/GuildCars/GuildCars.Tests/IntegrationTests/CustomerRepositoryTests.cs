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
    public class CustomerRepositoryTests
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
            var repo = new CustomerRepository();

            var customers = repo.GetAll().ToList();

            Assert.AreEqual(2, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerId);
            Assert.AreEqual(2, customers[1].CustomerId);
        }

        [Test]
        public void CanGetById()
        {
            var repo = new CustomerRepository();

            var customer = repo.GetById(1);

            Assert.AreEqual(1, customer.CustomerId);
            Assert.AreEqual(1, customer.AddressId);
            Assert.AreEqual("Testy", customer.CustomerFirstName);
            Assert.AreEqual("McTestington", customer.CustomerLastName);
            Assert.AreEqual("555-555-5555", customer.CustomerPhone);
            Assert.AreEqual("customer@test.com", customer.CustomerEmail);
        }

        [Test]
        public void CanInsert()
        {
            var repo = new CustomerRepository();

            Customer customer = new Customer();

            customer.AddressId = null;
            customer.CustomerFirstName = "Jack";
            customer.CustomerLastName = "Johnson";
            customer.CustomerPhone = "666-666-6666";
            customer.CustomerEmail = "jack@johnson.com";

            repo.Insert(customer);

            Assert.AreEqual(3, customer.CustomerId);

            var result = repo.GetById(3);

            Assert.AreEqual(3, result.CustomerId);
        }

        [Test]
        public void CanUpdate()
        {
            var repo = new CustomerRepository();

            Customer customer = new Customer();

            customer.AddressId = null;
            customer.CustomerFirstName = "Jack";
            customer.CustomerLastName = "Johnson";
            customer.CustomerPhone = "666-666-6666";
            customer.CustomerEmail = "jack@johnson.com";

            repo.Insert(customer);

            Assert.AreEqual(3, customer.CustomerId);

            customer.AddressId = null;
            customer.CustomerFirstName = "Jeff";
            customer.CustomerLastName = "Bridges";
            customer.CustomerPhone = "123-456-7890";
            customer.CustomerEmail = "thedude@abides.com";

            repo.Update(customer);

            var result = repo.GetById(3);

            Assert.IsNull(result.AddressId);
            Assert.AreEqual("Jeff", result.CustomerFirstName);
            Assert.AreEqual("Bridges", result.CustomerLastName);
            Assert.AreEqual("123-456-7890", result.CustomerPhone);
            Assert.AreEqual("thedude@abides.com", result.CustomerEmail);
        }

        [Test]
        public void CanDelete()
        {
            var repo = new CustomerRepository();

            Customer customer = new Customer();

            customer.AddressId = null;
            customer.CustomerFirstName = "Jack";
            customer.CustomerLastName = "Johnson";
            customer.CustomerPhone = "666-666-6666";
            customer.CustomerEmail = "jack@johnson.com";

            repo.Insert(customer);

            Assert.AreEqual(3, customer.CustomerId);

            var allCustomers = repo.GetAll().ToList();

            Assert.AreEqual(3, allCustomers.Count);

            repo.Delete(3);

            allCustomers = repo.GetAll().ToList();

            Assert.AreEqual(2, allCustomers.Count);
        }
    }
}
