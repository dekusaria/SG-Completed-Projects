using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class TransmissionFactoryRepository
    {
        public static ITransmissionRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockTransmissionRepository();
                case "PROD":
                    return new TransmissionRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
