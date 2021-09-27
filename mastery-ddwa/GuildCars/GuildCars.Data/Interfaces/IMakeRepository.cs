using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface IMakeRepository
    {
        Make GetById(int makeId);
        IEnumerable<Make> GetAll();
        void Insert(Make make);
        void Update(Make make);
        void Delete(int makeId);
    }
}
