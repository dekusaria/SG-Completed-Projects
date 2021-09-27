using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockStatesRepository : IStatesRepository
    {
        private static List<State> _repo;

        public MockStatesRepository()
        {
            _repo = new List<State>()
            {
                new State() { StateId = "AL", StateName = "Alabama" },
                new State() { StateId = "AR", StateName = "Arkansas" },
                new State() { StateId = "AZ", StateName = "Arizona" },
                new State() { StateId = "CA", StateName = "California" },
                new State() { StateId = "CO", StateName = "Colorado" },
                new State() { StateId = "CT", StateName = "Connecticut" },
                new State() { StateId = "DE", StateName = "Delaware" },
                new State() { StateId = "FL", StateName = "Florida" },
                new State() { StateId = "GA", StateName = "Georgia" },
                new State() { StateId = "HI", StateName = "Hawaii" },
                new State() { StateId = "IA", StateName = "Iowa" },
                new State() { StateId = "ID", StateName = "Idaho" },
                new State() { StateId = "IL", StateName = "Illinois" },
                new State() { StateId = "IN", StateName = "Indiana" },
                new State() { StateId = "KS", StateName = "Kansas" },
                new State() { StateId = "KY", StateName = "Kentucky" },
                new State() { StateId = "LA", StateName = "Louisiana" },
                new State() { StateId = "MA", StateName = "Massachusetts" },
                new State() { StateId = "MD", StateName = "Maryland" },
                new State() { StateId = "ME", StateName = "Maine" },
                new State() { StateId = "MI", StateName = "Michigan" },
                new State() { StateId = "MN", StateName = "Minnesota" },
                new State() { StateId = "MO", StateName = "Missouri" },
                new State() { StateId = "MS", StateName = "Mississippi" },
                new State() { StateId = "MT", StateName = "Montana" },
                new State() { StateId = "NC", StateName = "North Carolina" },
                new State() { StateId = "ND", StateName = "North Dakota" },
                new State() { StateId = "NE", StateName = "Nebraska" },
                new State() { StateId = "NH", StateName = "New Hampshire" },
                new State() { StateId = "NJ", StateName = "New Jersey" },
                new State() { StateId = "NM", StateName = "New Mexico" },
                new State() { StateId = "NV", StateName = "Nevada" },
                new State() { StateId = "NY", StateName = "New York" },
                new State() { StateId = "OH", StateName = "Ohio" },
                new State() { StateId = "OK", StateName = "Oklahoma" },
                new State() { StateId = "OR", StateName = "Oregon" },
                new State() { StateId = "PA", StateName = "Pennsylvania" },
                new State() { StateId = "RI", StateName = "Rhode Island" },
                new State() { StateId = "SC", StateName = "South Carolina" },
                new State() { StateId = "SD", StateName = "South Dakota" },
                new State() { StateId = "TN", StateName = "Tennessee" },
                new State() { StateId = "TX", StateName = "Texas" },
                new State() { StateId = "UT", StateName = "Utah" },
                new State() { StateId = "VA", StateName = "Virginia" },
                new State() { StateId = "VT", StateName = "Vermont" },
                new State() { StateId = "WA", StateName = "Washington" },
                new State() { StateId = "WI", StateName = "Wisconsin" },
                new State() { StateId = "WV", StateName = "West Virginia" },
                new State() { StateId = "WY", StateName = "Wyoming" }
            };
        }
        public IEnumerable<State> GetAll()
        {
            return _repo;
        }
    }
}
