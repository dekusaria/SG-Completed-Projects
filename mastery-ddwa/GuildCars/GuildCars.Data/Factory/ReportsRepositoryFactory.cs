using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class ReportsRepositoryFactory
    {
        public static IReportsRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockReportsRepository();
                case "PROD":
                    return new ReportsRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
