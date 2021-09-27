using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockPurchaseRepository : IPurchaseRepository
    {
        private static List<Purchase> _repo;

        public MockPurchaseRepository()
        {
            _repo = new List<Purchase>()
            {
                new Purchase()
                {
                    PurchaseId = 1,
                    CustomerId = 1,
                    PurchaseTypeId = 1,
                    VehicleId = 2,
                    PurchasePrice = 28500M,
                    PurchaseDate = DateTime.Today,
                    SoldByEmail = "sales@email.com"
                },
                new Purchase()
                {
                    PurchaseId = 2,
                    CustomerId = 2,
                    PurchaseTypeId = 1,
                    PurchasePrice = 25000M,
                    PurchaseDate = new DateTime(2019, 10, 31),
                    SoldByEmail = "sales@email.com"
                }
            };
        }
        public IEnumerable<Purchase> GetAll()
        {
            return _repo;
        }

        public Purchase GetById(int purchaseId)
        {
            return _repo.FirstOrDefault(m => m.PurchaseId == purchaseId);
        }

        public void Insert(Purchase purchase)
        {
            _repo.Add(purchase);
        }
    }
}
