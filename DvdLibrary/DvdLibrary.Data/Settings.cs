using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DvdLibrary.Data
{
    public class Settings
    {
        private static string _connectionString;
        private static string _factoryMode;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }

            return _connectionString;
        }

        public static string GetFactoryMode()
        {
            if (string.IsNullOrEmpty(_factoryMode))
            {
                _factoryMode = ConfigurationManager.AppSettings["Mode"].ToString();
            }

            return _factoryMode;
        }
    }
}
