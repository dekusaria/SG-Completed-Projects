using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface IPurchaseRepository
    {
        Purchase GetById(int purchaseId);
        IEnumerable<Purchase> GetAll();
        void Insert(Purchase purchase);
    }
}
