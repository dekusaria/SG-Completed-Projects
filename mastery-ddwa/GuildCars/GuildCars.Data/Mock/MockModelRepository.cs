using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockModelRepository : IModelRepository
    {
        private static List<Model> _repo;

        public MockModelRepository()
        {
            _repo = new List<Model>()
            {
                new Model()
                {
                    ModelId = 1,
                    MakeId = 1,
                    ModelName = "CRV",
                    DateAdded = new DateTime(2020, 10, 31),
                    EmployeeEmail = "employee@email.com"
                },

                new Model()
                {
                    ModelId = 2,
                    MakeId = 2,
                    ModelName = "Grand Cherokee",
                    DateAdded = DateTime.Today,
                    EmployeeEmail = "employee@email.com"
                }
            };
        }
        public void Delete(int modelId)
        {
            _repo.RemoveAll(m => m.ModelId == modelId);
        }

        public IEnumerable<Model> GetAll()
        {
            return _repo;
        }

        public Model GetById(int modelId)
        {
            return _repo.FirstOrDefault(m => m.ModelId == modelId);
        }

        public IEnumerable<Model> GetModelsByMakeId(int makeId)
        {
            return _repo.Where(m => m.MakeId == makeId);
        }

        public void Insert(Model model)
        {
            model.ModelId = _repo.Max(m => m.ModelId) + 1;
            _repo.Add(model);
        }

        public void Update(Model model)
        {
            _repo.RemoveAll(m => m.ModelId == model.ModelId);
            _repo.Add(model);
        }
    }
}
