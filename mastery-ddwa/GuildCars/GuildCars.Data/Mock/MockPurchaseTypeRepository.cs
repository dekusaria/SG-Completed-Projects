using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockPurchaseTypeRepository : IPurchaseTypeRepository
    {
        private static List<PurchaseType> _repo;

        public MockPurchaseTypeRepository()
        {
            _repo = new List<PurchaseType>()
            {
                new PurchaseType()
                {
                    PurchaseTypeId = 1,
                    PurchaseTypeName = "Bank Finance"
                },

                new PurchaseType()
                {
                    PurchaseTypeId = 2,
                    PurchaseTypeName = "Cash"
                },

                new PurchaseType()
                {
                    PurchaseTypeId = 3,
                    PurchaseTypeName = "Dealer Finance"
                }
            };
        }
        public IEnumerable<PurchaseType> GetAll()
        {
            return _repo;
        }
    }
}
