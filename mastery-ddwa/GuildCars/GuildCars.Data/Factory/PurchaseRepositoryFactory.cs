using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class PurchaseRepositoryFactory
    {
        public static IPurchaseRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockPurchaseRepository();
                case "PROD":
                    return new PurchaseRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
