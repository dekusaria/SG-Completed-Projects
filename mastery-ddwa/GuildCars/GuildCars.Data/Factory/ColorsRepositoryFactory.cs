using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class ColorsRepositoryFactory
    {
        public static IColorsRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockColorsRepository();
                case "PROD":
                    return new ColorsRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
