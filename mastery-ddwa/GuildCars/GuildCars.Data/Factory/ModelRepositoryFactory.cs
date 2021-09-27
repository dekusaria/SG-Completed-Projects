using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class ModelRepositoryFactory
    {
        public static IModelRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockModelRepository();
                case "PROD":
                    return new ModelRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
