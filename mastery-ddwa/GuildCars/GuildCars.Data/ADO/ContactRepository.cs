using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class ContactRepository : IContactRepository
    {
        public void Delete(int contactId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ContactId", contactId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact row = new Contact();

                        row.ContactId = (int)dr["ContactId"];
                        row.CustomerId = (int)dr["CustomerId"];
                        row.ContactMessage = dr["ContactMessage"].ToString();

                        contacts.Add(row);
                    }
                }
            }

            return contacts;
        }

        public Contact GetById(int contactId)
        {
            Contact contact = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ContactId", contactId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        contact = new Contact();

                        contact.ContactId = (int)dr["ContactId"];
                        contact.CustomerId = (int)dr["CustomerId"];
                        contact.ContactMessage = dr["ContactMessage"].ToString();
                    }
                }
            }

            return contact;
        }

        public void Insert(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@CustomerId", contact.CustomerId);
                cmd.Parameters.AddWithValue("@ContactMessage", contact.ContactMessage);

                cn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactId = (int)param.Value;
            }
        }
    }
}
