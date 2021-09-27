using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockInteriorsRepository : IInteriorsRepository
    {
        private static List<Interior> _repo;

        public MockInteriorsRepository()
        {
            _repo = new List<Interior>()
            {
                new Interior()
                {
                    InteriorId = 1,
                    InteriorType = "Black"
                },

                new Interior()
                {
                    InteriorId = 2,
                    InteriorType = "Tan"
                },

                new Interior()
                {
                    InteriorId = 3,
                    InteriorType = "Gray"
                },

                new Interior()
                {
                    InteriorId = 4,
                    InteriorType = "Black Leather"
                },

                new Interior()
                {
                    InteriorId = 5,
                    InteriorType = "Brown Leather"
                },

                new Interior()
                {
                    InteriorId = 6,
                    InteriorType = "Tan Leather"
                }
            };
        }
        public IEnumerable<Interior> GetAll()
        {
            return _repo;
        }
    }
}
