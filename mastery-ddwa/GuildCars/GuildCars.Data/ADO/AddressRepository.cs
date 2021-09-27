using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class AddressRepository : IAddressRepository
    {
        public void Delete(int addressId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddressesDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddressId", addressId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Address> GetAll()
        {
            List<Address> addresses = new List<Address>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddressesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Address row = new Address();

                        row.AddressId = (int)dr["AddressId"];
                        row.StateId = dr["StateId"].ToString();
                        row.Street1 = dr["Street1"].ToString();
                        row.Street2 = dr["Street2"] != DBNull.Value ? dr["Street2"].ToString() : "";
                        row.City = dr["City"].ToString();
                        row.Zipcode = dr["Zipcode"].ToString();

                        addresses.Add(row);
                    }
                }
            }

            return addresses;
        }

        public Address GetById(int addressId)
        {
            Address address = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddressesSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddressId", addressId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        address = new Address();

                        address.AddressId = (int)dr["AddressId"];
                        address.StateId = dr["StateId"].ToString();
                        address.Street1 = dr["Street1"].ToString();
                        address.Street2 = dr["Street2"] != DBNull.Value ? dr["Street2"].ToString() : "";
                        address.City = dr["City"].ToString();
                        address.Zipcode = dr["Zipcode"].ToString();
                    }
                }
            }

            return address;
        }

        public void Insert(Address address)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddressesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@AddressId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@StateId", address.StateId);
                cmd.Parameters.AddWithValue("@Street1", address.Street1);
                if (string.IsNullOrEmpty(address.Street2))
                    cmd.Parameters.AddWithValue("@Street2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Street2", address.Street2);
                cmd.Parameters.AddWithValue("@City", address.City);
                cmd.Parameters.AddWithValue("@Zipcode", address.Zipcode);

                cn.Open();

                cmd.ExecuteNonQuery();

                address.AddressId = (int)param.Value;
            }
        }

        public void Update(Address address)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddressesUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddressId", address.AddressId);
                cmd.Parameters.AddWithValue("@StateId", address.StateId);
                cmd.Parameters.AddWithValue("@Street1", address.Street1);
                if (string.IsNullOrEmpty(address.Street2))
                    cmd.Parameters.AddWithValue("@Street2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Street2", address.Street2);
                cmd.Parameters.AddWithValue("@City", address.City);
                cmd.Parameters.AddWithValue("@Zipcode", address.Zipcode);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
