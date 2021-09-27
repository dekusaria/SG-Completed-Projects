using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockVehicleTypeRepository : IVehicleTypeRepository
    {
        private static List<VehicleType> _repo;

        public MockVehicleTypeRepository()
        {
            _repo = new List<VehicleType>()
            {
                new VehicleType()
                {
                    VehicleTypeId = 1,
                    VehicleTypeName = "New"
                },

                new VehicleType()
                {
                    VehicleTypeId = 2,
                    VehicleTypeName = "Used"
                }
            };
        }
        public IEnumerable<VehicleType> GetAll()
        {
            return _repo;
        }
    }
}
