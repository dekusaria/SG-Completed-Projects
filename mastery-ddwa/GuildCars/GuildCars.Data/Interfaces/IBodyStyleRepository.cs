using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface IBodyStyleRepository
    {
        IEnumerable<BodyStyle> GetAll();
    }
}
