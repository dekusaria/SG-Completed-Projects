using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int customerId);
        void Insert(Customer customer);
        void Update(Customer customer);
        void Delete(int customerId);
    }
}
