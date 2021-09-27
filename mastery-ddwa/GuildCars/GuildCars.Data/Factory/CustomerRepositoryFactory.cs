using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Factory
{
    public static class CustomerRepositoryFactory
    {
        public static ICustomerRepository GetRepository()
        {
            switch (Settings.GetProductionMode())
            {
                case "QA":
                    return new MockCustomerRepository();
                case "PROD":
                    return new CustomerRepository();
                default:
                    throw new Exception("Error finding Production Mode Setting");
            }
        }
    }
}
