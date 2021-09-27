using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockColorsRepository : IColorsRepository
    {
        private static List<Color> _repo;

        public MockColorsRepository()
        {
            _repo = new List<Color>()
            {
                new Color()
                {
                    ColorId = 1,
                    ColorName = "White"
                },

                new Color()
                {
                    ColorId = 2,
                    ColorName = "Black"
                },

                new Color()
                {
                    ColorId = 3,
                    ColorName = "Gray"
                },

                new Color()
                {
                    ColorId = 4,
                    ColorName = "Silver"
                },

                new Color()
                {
                    ColorId = 5,
                    ColorName = "Red"
                },

                new Color()
                {
                    ColorId = 6,
                    ColorName = "Blue"
                },

                new Color()
                {
                    ColorId = 7,
                    ColorName = "Tan"
                },

                new Color()
                {
                    ColorId = 8,
                    ColorName = "Brown"
                },

                new Color()
                {
                    ColorId = 9,
                    ColorName = "Green"
                },

                new Color()
                {
                    ColorId = 10,
                    ColorName = "Orange"
                },

                new Color()
                {
                    ColorId = 11,
                    ColorName = "Gold"
                },

                new Color()
                {
                    ColorId = 12,
                    ColorName = "Yellow"
                },

                new Color()
                {
                    ColorId = 13,
                    ColorName = "Purple"
                }
            };
        }
        public IEnumerable<Color> GetAll()
        {
            return _repo;
        }
    }
}
