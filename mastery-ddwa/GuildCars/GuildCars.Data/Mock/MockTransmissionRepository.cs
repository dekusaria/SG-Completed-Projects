using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockTransmissionRepository : ITransmissionRepository
    {
        private static List<Transmission> _repo;

        public MockTransmissionRepository()
        {
            _repo = new List<Transmission>()
            {
                new Transmission()
                {
                    TransmissionId = 1,
                    TransmissionType = "Automatic"
                },

                new Transmission()
                {
                    TransmissionId = 2,
                    TransmissionType = "Manual"
                }
            };
        }
        public IEnumerable<Transmission> GetAll()
        {
            return _repo;
        }
    }
}
