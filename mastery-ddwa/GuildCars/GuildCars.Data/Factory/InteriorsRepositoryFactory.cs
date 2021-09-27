using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class InteriorsRepositoryFactory
    {
        public static IInteriorsRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockInteriorsRepository();
                case "PROD":
                    return new InteriorsRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
