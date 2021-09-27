using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockMakeRepository : IMakeRepository
    {
        private static List<Make> _repo;

        public MockMakeRepository()
        {
            _repo = new List<Make>()
            {
                new Make()
                {
                    MakeId = 1,
                    MakeName = "Honda",
                    DateAdded = new DateTime(2020, 10, 31),
                    EmployeeEmail = "employee@email.com"
                },

                new Make()
                {
                    MakeId = 2,
                    MakeName = "Jeep",
                    DateAdded = DateTime.Today,
                    EmployeeEmail = "employee@email.com"
                }
            };
        }
        public void Delete(int makeId)
        {
            _repo.RemoveAll(m => m.MakeId == makeId);
        }

        public IEnumerable<Make> GetAll()
        {
            return _repo;
        }

        public Make GetById(int makeId)
        {
            return _repo.FirstOrDefault(m => m.MakeId == makeId);
        }

        public void Insert(Make make)
        {
            make.MakeId = _repo.Max(m => m.MakeId) + 1;
            _repo.Add(make);
        }

        public void Update(Make make)
        {
            _repo.RemoveAll(m => m.MakeId == make.MakeId);
            _repo.Add(make);
        }
    }
}
