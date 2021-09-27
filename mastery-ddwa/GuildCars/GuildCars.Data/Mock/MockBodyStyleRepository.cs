using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockBodyStyleRepository : IBodyStyleRepository
    {
        private static List<BodyStyle> _repo;

        public MockBodyStyleRepository()
        {
            _repo = new List<BodyStyle>()
            {
                new BodyStyle()
                {
                    BodyStyleId = 1,
                    BodyStyleType = "Car"
                },

                new BodyStyle()
                {
                    BodyStyleId = 2,
                    BodyStyleType = "SUV"
                },

                new BodyStyle()
                {
                    BodyStyleId = 3,
                    BodyStyleType = "Truck"
                },

                new BodyStyle()
                {
                    BodyStyleId = 4,
                    BodyStyleType = "Van"
                }
            };


        }
        public IEnumerable<BodyStyle> GetAll()
        {
            return _repo;
        }
    }
}
