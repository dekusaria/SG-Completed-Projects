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
    public class MakeRepositoryTests
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
            var repo = new MakeRepository();

            var makes = repo.GetAll().ToList();

            Assert.AreEqual(2, makes.Count);
            Assert.AreEqual(1, makes[0].MakeId);
            Assert.AreEqual(2, makes[1].MakeId);
        }

        [Test]
        public void CanGetById()
        {
            var repo = new MakeRepository();

            var make = repo.GetById(1);

            Assert.AreEqual(1, make.MakeId);
            Assert.AreEqual("Honda", make.MakeName);
            Assert.AreEqual(new DateTime(2021, 8, 3), make.DateAdded);
            Assert.AreEqual("employee1@email.com", make.EmployeeEmail);
        }

        [Test]
        public void CanInsert()
        {
            var repo = new MakeRepository();

            Make make = new Make();

            make.MakeName = "Toyota";
            make.DateAdded = DateTime.Today;
            make.EmployeeEmail = "employee1@email.com";

            repo.Insert(make);

            Assert.AreEqual(3, make.MakeId);

            var result = repo.GetById(3);

            Assert.AreEqual(3, result.MakeId);
            Assert.AreEqual("Toyota", make.MakeName);
            Assert.AreEqual(DateTime.Today, make.DateAdded);
            Assert.AreEqual("employee1@email.com", make.EmployeeEmail);
        }

        [Test]
        public void CanUpdate()
        {
            var repo = new MakeRepository();

            Make make = new Make();

            make.MakeName = "Toyota";
            make.DateAdded = DateTime.Today;
            make.EmployeeEmail = "employee1@email.com";

            repo.Insert(make);

            Assert.AreEqual(3, make.MakeId);

            make.MakeName = "Volkswagen";
            make.DateAdded = new DateTime(2021, 8, 3);
            make.EmployeeEmail = "employee2@email.com";

            repo.Update(make);

            var result = repo.GetById(3);

            Assert.AreEqual(3, result.MakeId);
            Assert.AreEqual("Volkswagen", result.MakeName);
            Assert.AreEqual(new DateTime(2021, 8, 3), make.DateAdded);
            Assert.AreEqual("employee2@email.com", make.EmployeeEmail);
        }

        [Test]
        public void CanDelete()
        {

        }
    }
}
