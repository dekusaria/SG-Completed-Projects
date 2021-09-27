using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GuildCars.Data.Mock
{
    public class MockAddressRepository : IAddressRepository
    {
        private static List<Address> _repo;

        public MockAddressRepository()
        {
            _repo = new List<Address>();

            Address model1 = new Address()
            {
                AddressId = 1,
                StateId = "MN",
                Street1 = "123 Test St.",
                Street2 = "Apt 1",
                City = "Minneapolis",
                Zipcode = "55555"
            };

            _repo.Add(model1);

            Address model2 = new Address()
            {
                AddressId = 1,
                StateId = "OH",
                Street1 = "24 Main St.",
                Street2 = null,
                City = "Columbus",
                Zipcode = "44444"
            };

            _repo.Add(model2);
        }
        public void Delete(int addressId)
        {
            _repo.RemoveAll(m => m.AddressId == addressId);
        }

        public IEnumerable<Address> GetAll()
        {
            return _repo;
        }

        public Address GetById(int addressId)
        {
            return _repo.FirstOrDefault(m => m.AddressId == addressId);
        }

        public void Insert(Address address)
        {
            address.AddressId = _repo.Max(m => m.AddressId) + 1;
            _repo.Add(address);
        }

        public void Update(Address address)
        {
            _repo.RemoveAll(m => m.AddressId == address.AddressId);

            _repo.Add(address);
        }
    }
}
