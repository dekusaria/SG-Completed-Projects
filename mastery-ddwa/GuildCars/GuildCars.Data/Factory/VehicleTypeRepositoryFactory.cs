using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class VehicleTypeRepositoryFactory
    {
        public static IVehicleTypeRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockVehicleTypeRepository();
                case "PROD":
                    return new VehicleTypeRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
