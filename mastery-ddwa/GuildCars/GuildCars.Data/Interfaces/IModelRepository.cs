using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface IModelRepository
    {
        Model GetById(int modelId);
        IEnumerable<Model> GetAll();
        IEnumerable<Model> GetModelsByMakeId(int makeId);
        void Insert(Model model);
        void Update(Model model);
        void Delete(int modelId);
    }
}
