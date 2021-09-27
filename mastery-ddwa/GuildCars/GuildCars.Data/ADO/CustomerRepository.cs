using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Delete(int customerId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CustomersDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CustomersSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Customer row = new Customer();

                        row.CustomerId = (int)dr["CustomerId"];
                        if (dr["AddressId"] != DBNull.Value)
                            row.AddressId = (int)dr["AddressId"];
                        else
                            row.AddressId = null;
                        row.CustomerFirstName = dr["CustomerFirstName"].ToString();
                        row.CustomerLastName = dr["CustomerLastName"].ToString();
                        row.CustomerPhone = dr["CustomerPhone"] != DBNull.Value ? dr["CustomerPhone"].ToString() : null;
                        row.CustomerEmail = dr["CustomerEmail"] != DBNull.Value ? dr["CustomerEmail"].ToString() : null;

                        customers.Add(row);
                    }
                }
            }

            return customers;
        }

        public Customer GetById(int customerId)
        {
            Customer customer = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CustomersSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        customer = new Customer();

                        customer.CustomerId = (int)dr["CustomerId"];
                        if (dr["AddressId"] != DBNull.Value)
                            customer.AddressId = (int)dr["AddressId"];
                        else
                            customer.AddressId = null;
                        customer.CustomerFirstName = dr["CustomerFirstName"].ToString();
                        customer.CustomerLastName = dr["CustomerLastName"].ToString();
                        customer.CustomerPhone = dr["CustomerPhone"] != DBNull.Value ? dr["CustomerPhone"].ToString() : null;
                        customer.CustomerEmail = dr["CustomerEmail"] != DBNull.Value ? dr["CustomerEmail"].ToString() : null;
                    }
                }
            }

            return customer;
        }

        public void Insert(Customer customer)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CustomersInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CustomerId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                if (customer.AddressId != null)
                    cmd.Parameters.AddWithValue("@AddressId", customer.AddressId);
                else
                    cmd.Parameters.AddWithValue("@AddressId", DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerFirstName", customer.CustomerFirstName);
                cmd.Parameters.AddWithValue("@CustomerLastName", customer.CustomerLastName);
                if (customer.CustomerPhone != null)
                    cmd.Parameters.AddWithValue("@CustomerPhone", customer.CustomerPhone);
                else
                    cmd.Parameters.AddWithValue("@CustomerPhone", DBNull.Value);
                if (customer.CustomerEmail != null)
                    cmd.Parameters.AddWithValue("@CustomerEmail", customer.CustomerEmail);
                else
                    cmd.Parameters.AddWithValue("@CustomerEmail", DBNull.Value);

                cn.Open();

                cmd.ExecuteNonQuery();

                customer.CustomerId = (int)param.Value;
            }
        }

        public void Update(Customer customer)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CustomersUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                if (customer.AddressId != null)
                    cmd.Parameters.AddWithValue("@AddressId", customer.AddressId);
                else
                    cmd.Parameters.AddWithValue("AddressId", DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerFirstName", customer.CustomerFirstName);
                cmd.Parameters.AddWithValue("@CustomerLastName", customer.CustomerLastName);
                cmd.Parameters.AddWithValue("@CustomerPhone", customer.CustomerPhone);
                cmd.Parameters.AddWithValue("@CustomerEmail", customer.CustomerEmail);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
