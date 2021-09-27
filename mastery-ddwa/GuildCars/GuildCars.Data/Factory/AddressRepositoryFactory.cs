using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class AddressRepositoryFactory
    {
        public static IAddressRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockAddressRepository();
                case "PROD":
                    return new AddressRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
