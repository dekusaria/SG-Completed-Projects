using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class BodyStyleRepositoryFactory
    {
        public static IBodyStyleRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockBodyStyleRepository();
                case "PROD":
                    return new BodyStyleRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
