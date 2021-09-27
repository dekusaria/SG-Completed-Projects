using DvdLibrary.Data.ADO;
using DvdLibrary.Data.Interfaces;
using DvdLibrary.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace DvdLibrary.Data.Factory
{
    public class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            switch (Settings.GetFactoryMode())
            {
                case "ADO":
                    return new DvdRepositoryADO();
                case "SampleData":
                    return new DvdReposiotryMock();
                default:
                    throw new Exception("Could not find Mode configuration value.");
            }
        }
    }
}
