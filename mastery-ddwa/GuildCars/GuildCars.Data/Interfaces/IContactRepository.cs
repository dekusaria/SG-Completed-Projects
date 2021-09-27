using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildCars.Data.Interfaces
{
    public interface IContactRepository
    {
        Contact GetById(int contactId);
        IEnumerable<Contact> GetAll();
        void Insert(Contact contact);
        void Delete(int contactId);
    }
}
