using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace GuildCars.Data
{
    public class Settings
    {
        private static string _connectionString;
        private static string _productionMode;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }

            return _connectionString;
        }

        public static string GetProductionMode()
        {
            if (string.IsNullOrEmpty(_productionMode))
            {
                _productionMode = ConfigurationManager.AppSettings["Mode"].ToString();
            }

            return _productionMode;
        }
    }
}
