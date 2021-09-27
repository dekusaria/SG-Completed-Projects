using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface ISpecialRepository
    {
        Special GetById(int specialId);
        IEnumerable<Special> GetAll();
        void Insert(Special special);
        void Update(Special special);
        void Delete(int specialId);
    }
}
