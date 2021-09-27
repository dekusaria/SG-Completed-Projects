using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAll();
        Address GetById(int addressId);
        void Insert(Address address);
        void Update(Address address);
        void Delete(int addressId);
    }
}
