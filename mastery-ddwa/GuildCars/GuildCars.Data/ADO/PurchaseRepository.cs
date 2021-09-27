using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public IEnumerable<Purchase> GetAll()
        {
            List<Purchase> purchases = new List<Purchase>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchasesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Purchase row = new Purchase();

                        row.PurchaseId = (int)dr["PurchaseId"];
                        row.CustomerId = (int)dr["CustomerId"];
                        row.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        row.VehicleId = (int)dr["VehicleId"];
                        row.PurchasePrice = (decimal)dr["PurchasePrice"];
                        row.PurchaseDate = DateTime.Parse(dr["PurchaseDate"].ToString());
                        row.SoldByEmail = dr["SoldByEmail"].ToString();

                        purchases.Add(row);
                    }
                }
            }

            return purchases;
        }

        public Purchase GetById(int purchaseId)
        {
            Purchase purchase = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchasesSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("PurchaseId", purchaseId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        purchase = new Purchase();

                        purchase.PurchaseId = (int)dr["PurchaseId"];
                        purchase.CustomerId = (int)dr["CustomerId"];
                        purchase.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        purchase.VehicleId = (int)dr["VehicleId"];
                        purchase.PurchasePrice = (decimal)dr["PurchasePrice"];
                        purchase.PurchaseDate = DateTime.Parse(dr["PurchaseDate"].ToString());
                        purchase.SoldByEmail = dr["SoldByEmail"].ToString();
                    }
                }
            }

            return purchase;
        }

        public void Insert(Purchase purchase)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchasesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@PurchaseId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@CustomerId", purchase.CustomerId);
                cmd.Parameters.AddWithValue("@PurchaseTypeId", purchase.PurchaseTypeId);
                cmd.Parameters.AddWithValue("@VehicleId", purchase.VehicleId);
                cmd.Parameters.AddWithValue("@PurchasePrice", purchase.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);
                cmd.Parameters.AddWithValue("@SoldByEmail", purchase.SoldByEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                purchase.PurchaseId = (int)param.Value;
            }
        }
    }
}
