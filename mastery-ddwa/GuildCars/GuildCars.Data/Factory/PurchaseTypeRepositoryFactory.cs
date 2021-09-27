using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class PurchaseTypeRepositoryFactory
    {
        public static IPurchaseTypeRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockPurchaseTypeRepository();
                case "PROD":
                    return new PurchaseTypeRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
