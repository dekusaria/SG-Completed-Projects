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
    public class ContactRepositoryTests
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
            var repo = new ContactRepository();

            var contacts = repo.GetAll().ToList();

            Assert.AreEqual(2, contacts.Count());
            Assert.AreEqual(1, contacts[0].ContactId);
            Assert.AreEqual(2, contacts[1].ContactId);
        }

        [Test]
        public void CanGetById()
        {
            var repo = new ContactRepository();

            var contact = repo.GetById(1);

            Assert.AreEqual(1, contact.ContactId);
            Assert.AreEqual("Hello, World!", contact.ContactMessage);
        }

        [Test]
        public void CanInsert()
        {
            var repo = new ContactRepository();

            Contact contact = new Contact();
            contact.ContactMessage = "Howdy, partner!";
            contact.CustomerId = 1;

            repo.Insert(contact);

            Assert.AreEqual(3, contact.ContactId);

            var result = repo.GetById(3);

            Assert.AreEqual("Howdy, partner!", result.ContactMessage);
            Assert.AreEqual(1, result.CustomerId);
        }

        [Test]
        public void CanDelete()
        {
            var repo = new ContactRepository();

            var contacts = repo.GetAll().ToList();

            Assert.AreEqual(2, contacts.Count());

            repo.Delete(1);

            contacts = repo.GetAll().ToList();

            Assert.AreEqual(1, contacts.Count());
        }
    }
}
