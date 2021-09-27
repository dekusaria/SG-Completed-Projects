using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GuildCars.Data.Mock
{
    public class MockContactRepository : IContactRepository
    {
        private static List<Contact> _repo;

        public MockContactRepository()
        {
            _repo = new List<Contact>()
            {
                new Contact()
                {
                    ContactId = 1,
                    CustomerId = 1,
                    ContactMessage = "Hey! I'm contacting you!"
                },

                new Contact()
                {
                    ContactId = 2,
                    CustomerId = 2,
                    ContactMessage = "Nice website, I'd like to buy all of the cars please."
                }
            };
        }
        public void Delete(int contactId)
        {
            _repo.RemoveAll(m => m.ContactId == contactId);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _repo;
        }

        public Contact GetById(int contactId)
        {
            return _repo.FirstOrDefault(m => m.ContactId == contactId);
        }

        public void Insert(Contact contact)
        {
            contact.ContactId = _repo.Max(m => m.ContactId) + 1;
            _repo.Add(contact);
        }
    }
}
