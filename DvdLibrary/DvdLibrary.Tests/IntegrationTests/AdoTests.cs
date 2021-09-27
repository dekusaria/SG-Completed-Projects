using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DvdLibrary.Data.ADO;
using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Queries;
using NUnit.Framework;

namespace DvdLibrary.Tests.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DvdLibraryTestDataInsert";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanGetDvds()
        {
            var repo = new DvdRepositoryADO();
            var dvds = repo.GetAll().ToList();

            Assert.AreEqual(5, dvds.Count);
            Assert.AreEqual(1, dvds[0].DvdId);
            Assert.AreEqual("PG-13", dvds[0].RatingName);
            Assert.AreEqual("Tim Burton", dvds[0].DirectorName);
            Assert.AreEqual("Edward Scissorhands", dvds[0].Title);
            Assert.AreEqual(1990, dvds[0].ReleaseYear);
            Assert.IsNotNull(dvds[0].Notes);
        }

        [Test]
        public void CanAddDvdAndGetById()
        {
            DvdItem dvd = new DvdItem();
            var repo = new DvdRepositoryADO();

            dvd.Title = "Spider-Man 2";
            dvd.ReleaseYear = 2004;
            dvd.DirectorName = "Sam Raimi";
            dvd.RatingName = "PG-13";
            dvd.Notes = "It's pizza time!";

            repo.Insert(dvd);

            Assert.AreEqual(6, dvd.DvdId);

            dvd = repo.GetDvdById(6);

            Assert.AreEqual("Spider-Man 2", dvd.Title);
            Assert.AreEqual(2004, dvd.ReleaseYear);
            Assert.AreEqual("Sam Raimi", dvd.DirectorName);
            Assert.AreEqual("PG-13", dvd.RatingName);
            Assert.AreEqual("It's pizza time!", dvd.Notes);
        }

        [Test]
        public void CanAddDvdWithNullValues()
        {
            DvdItem dvd = new DvdItem();
            var repo = new DvdRepositoryADO();

            dvd.Title = "Spider-Man 2";
            dvd.ReleaseYear = 2004;
            dvd.DirectorName = null;
            dvd.RatingName = null;
            dvd.Notes = null;

            repo.Insert(dvd);

            Assert.AreEqual(6, dvd.DvdId);

            dvd = repo.GetDvdById(6);

            Assert.AreEqual("Spider-Man 2", dvd.Title);
            Assert.AreEqual(2004, dvd.ReleaseYear);
            Assert.IsNull(dvd.DirectorName);
            Assert.IsNull(dvd.RatingName);
            Assert.IsNull(dvd.Notes);
        }

        [Test]
        public void CanDeleteDvd()
        {
            DvdItem dvd = new DvdItem();
            var repo = new DvdRepositoryADO();

            dvd.Title = "Spider-Man 2";
            dvd.ReleaseYear = 2004;
            dvd.DirectorName = null;
            dvd.RatingName = null;
            dvd.Notes = null;

            repo.Insert(dvd);

            Assert.AreEqual(6, dvd.DvdId);

            repo.Delete(6);

            dvd = repo.GetDvdById(6);

            Assert.IsNull(dvd);
        }

        [Test]
        public void CanUpdateDvd()
        {
            DvdItem dvd = new DvdItem();
            var repo = new DvdRepositoryADO();

            dvd.Title = "Spider-Man 2";
            dvd.ReleaseYear = 2004;
            dvd.DirectorName = "Sam Raimi";
            dvd.RatingName = "PG-13";
            dvd.Notes = "It's pizza time!";

            repo.Insert(dvd);

            Assert.AreEqual(6, dvd.DvdId);

            dvd = repo.GetDvdById(6);

            Assert.AreEqual("Spider-Man 2", dvd.Title);
            Assert.AreEqual(2004, dvd.ReleaseYear);
            Assert.AreEqual("Sam Raimi", dvd.DirectorName);
            Assert.AreEqual("PG-13", dvd.RatingName);
            Assert.AreEqual("It's pizza time!", dvd.Notes);

            dvd.Title = "Spider-Man: Far From Home";
            dvd.ReleaseYear = 2019;
            dvd.DirectorName = "Jon Watts";
            dvd.RatingName = "PG-13";
            dvd.Notes = "A more modern take.";

            repo.Update(dvd);

            dvd = repo.GetDvdById(6);

            Assert.AreEqual("Spider-Man: Far From Home", dvd.Title);
            Assert.AreEqual(2019, dvd.ReleaseYear);
            Assert.AreEqual("Jon Watts", dvd.DirectorName);
            Assert.AreEqual("PG-13", dvd.RatingName);
            Assert.AreEqual("A more modern take.", dvd.Notes);
        }

        [Test]
        public void CanUpdateDvdWithNullValues()
        {
            DvdItem dvd = new DvdItem();
            var repo = new DvdRepositoryADO();

            dvd.Title = "Spider-Man 2";
            dvd.ReleaseYear = 2004;

            repo.Insert(dvd);

            Assert.AreEqual(6, dvd.DvdId);

            dvd = repo.GetDvdById(6);

            Assert.IsNull(dvd.DirectorName);
            Assert.IsNull(dvd.RatingName);
            Assert.IsNull(dvd.Notes);

            Assert.AreEqual("Spider-Man 2", dvd.Title);
            Assert.AreEqual(2004, dvd.ReleaseYear);

            dvd.Title = "Spider-Man: Far From Home";
            dvd.ReleaseYear = 2019;
            dvd.DirectorName = "Jon Watts";
            dvd.RatingName = "PG-13";
            dvd.Notes = "A more modern take.";

            repo.Update(dvd);

            dvd = repo.GetDvdById(6);

            Assert.AreEqual("Spider-Man: Far From Home", dvd.Title);
            Assert.AreEqual(2019, dvd.ReleaseYear);
            Assert.AreEqual("Jon Watts", dvd.DirectorName);
            Assert.AreEqual("PG-13", dvd.RatingName);
            Assert.AreEqual("A more modern take.", dvd.Notes);

            dvd.DirectorName = null;
            dvd.RatingName = null;
            dvd.Notes = null;

            repo.Update(dvd);

            dvd = repo.GetDvdById(6);

            Assert.AreEqual("Spider-Man: Far From Home", dvd.Title);
            Assert.AreEqual(2019, dvd.ReleaseYear);
            Assert.IsNull(dvd.DirectorName);
            Assert.IsNull(dvd.RatingName);
            Assert.IsNull(dvd.Notes);
        }

        [Test]
        public void CanSearchByTitle()
        {
            List<DvdItem> dvds = new List<DvdItem>();
            var repo = new DvdRepositoryADO();

            dvds = repo.SearchByTitle("Edward").ToList();

            Assert.AreEqual(1, dvds.Count);

            dvds = repo.SearchByTitle("B").ToList();

            Assert.AreEqual(2, dvds.Count);
        }

        [Test]
        public void CanSearchByReleaseYear()
        {
            List<DvdItem> dvds = new List<DvdItem>();
            var repo = new DvdRepositoryADO();

            dvds = repo.SearchByReleaseYear(2021).ToList();

            Assert.AreEqual(1, dvds.Count);

            DvdItem test1 = new DvdItem();
            test1.Title = "Test 1";
            test1.ReleaseYear = 2021;

            repo.Insert(test1);

            dvds = repo.SearchByReleaseYear(2021).ToList();

            Assert.AreEqual(2, dvds.Count);
        }

        [Test]
        public void CanSearchByDirector()
        {
            List<DvdItem> dvds = new List<DvdItem>();
            var repo = new DvdRepositoryADO();

            dvds = repo.SearchByDirector("Peter J").ToList();

            Assert.AreEqual(1, dvds.Count);

            dvds = repo.SearchByDirector("Tim").ToList();

            Assert.AreEqual(2, dvds.Count);

            DvdItem test1 = new DvdItem();
            test1.Title = "Iron Man";
            test1.ReleaseYear = 2008;
            test1.DirectorName = "Jon Favreau";

            DvdItem test2 = new DvdItem();
            test2.Title = "Spider-Man: Far From Home";
            test2.ReleaseYear = 2019;
            test2.DirectorName = "Jon Watts";

            repo.Insert(test1);
            repo.Insert(test2);

            dvds = repo.SearchByDirector("Jon").ToList();

            Assert.AreEqual(2, dvds.Count);
        }

        [Test]
        public void CanSearchByRating()
        {
            List<DvdItem> dvds = new List<DvdItem>();
            var repo = new DvdRepositoryADO();

            dvds = repo.SearchByRating("PG-13").ToList();

            Assert.AreEqual(3, dvds.Count);

            dvds = repo.SearchByRating("PG").ToList();

            Assert.AreEqual(1, dvds.Count);
        }

    }
}
