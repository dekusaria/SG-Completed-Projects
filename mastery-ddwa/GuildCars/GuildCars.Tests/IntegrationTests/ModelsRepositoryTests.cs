using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GuildCars.Data.ADO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using GuildCars.Models.Tables;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class ModelsRepositoryTests
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
        public void CanGetAllModels()
        {
            var repo = new ModelRepository();
            var models = repo.GetAll().ToList();

            Assert.AreEqual(2, models.Count);
        }

        [Test]
        public void CanGetModelById()
        {
            var repo = new ModelRepository();
            var model = repo.GetById(1);

            Assert.AreEqual(1, model.ModelId);
            Assert.AreEqual(1, model.MakeId);
            Assert.AreEqual("CRV", model.ModelName);
            Assert.AreEqual(new DateTime(2021, 8, 3), model.DateAdded);
            Assert.AreEqual("employee1@email.com", model.EmployeeEmail);
        }
        
        [Test]
        public void CanGetModelsByMakeId()
        {
            var repo = new ModelRepository();
            var models = repo.GetModelsByMakeId(1).ToList();

            Assert.AreEqual(1, models.Count);
            Assert.AreEqual(1, models[0].ModelId);
            Assert.AreEqual("CRV", models[0].ModelName);
            Assert.AreEqual(new DateTime(2021, 8, 3), models[0].DateAdded);
            Assert.AreEqual("employee1@email.com", models[0].EmployeeEmail);
        }

        [Test]
        public void CanInsertModel()
        {
            var repo = new ModelRepository();

            Model model = new Model()
            {
                MakeId = 1,
                ModelName = "Civic",
                DateAdded = DateTime.Today,
                EmployeeEmail = "employee1@email.com"
            };

            repo.Insert(model);

            Assert.AreEqual(3, model.ModelId);

            var response = repo.GetById(3);

            Assert.AreEqual(1, response.MakeId);
            Assert.AreEqual("Civic", response.ModelName);
            Assert.AreEqual(DateTime.Today, response.DateAdded);
            Assert.AreEqual("employee1@email.com", response.EmployeeEmail);
        }

        [Test]
        public void CanUpdateModel()
        {
            var repo = new ModelRepository();

            Model model = new Model()
            {
                MakeId = 1,
                ModelName = "Civic",
                DateAdded = DateTime.Today,
                EmployeeEmail = "employee1@email.com"
            };

            repo.Insert(model);

            model.MakeId = 2;
            model.ModelName = "Wrangler";
            model.DateAdded = new DateTime(2021, 8, 8);
            model.EmployeeEmail = "employee2@email.com";

            repo.Update(model);

            var response = repo.GetById(3);

            Assert.AreEqual(2, response.MakeId);
            Assert.AreEqual("Wrangler", response.ModelName);
            Assert.AreEqual(new DateTime(2021, 8, 8), response.DateAdded);
            Assert.AreEqual("employee2@email.com", response.EmployeeEmail);
        }

        [Test]
        public void CanDeleteModel()
        {
            var repo = new ModelRepository();

            Model model = new Model()
            {
                MakeId = 1,
                ModelName = "Civic",
                DateAdded = DateTime.Today,
                EmployeeEmail = "employee1@email.com"
            };

            repo.Insert(model);

            Assert.AreEqual(3, model.ModelId);

            repo.Delete(3);

            var response = repo.GetById(3);

            Assert.IsNull(response);
        }
    }
}
