using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildCars.Data.Mock
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private static List<Customer> _repo;

        public MockCustomerRepository()
        {
            _repo = new List<Customer>()
            {
                new Customer()
                {
                    CustomerId = 1,
                    AddressId = 1,
                    CustomerFirstName = "Test",
                    CustomerLastName = "Testington",
                    CustomerPhone = "555-555-5555",
                    CustomerEmail = "customer1@email.com"
                },

                new Customer()
                {
                    CustomerId = 2,
                    AddressId = 2,
                    CustomerFirstName = "Hubert",
                    CustomerLastName = "Farnsworth",
                    CustomerPhone = "444-444-4444",
                    CustomerEmail = "customer2@email.com"
                },

                new Customer()
                {
                    CustomerId = 3,
                    AddressId = null,
                    CustomerFirstName = "Turanga",
                    CustomerLastName = "Leela",
                    CustomerPhone = "333-333-3333",
                    CustomerEmail = "customer3@email.com"
                }
            };
        }
        public void Delete(int customerId)
        {
            _repo.RemoveAll(m => m.CustomerId == customerId);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _repo;
        }

        public Customer GetById(int customerId)
        {
            return _repo.FirstOrDefault(m => m.CustomerId == customerId);
        }

        public void Insert(Customer customer)
        {
            customer.CustomerId = _repo.Max(m => m.CustomerId) + 1;
            _repo.Add(customer);
        }

        public void Update(Customer customer)
        {
            _repo.RemoveAll(m => m.CustomerId == customer.CustomerId);
            _repo.Add(customer);
        }
    }
}
