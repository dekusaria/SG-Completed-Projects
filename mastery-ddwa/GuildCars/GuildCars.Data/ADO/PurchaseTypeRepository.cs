using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class PurchaseTypeRepository : IPurchaseTypeRepository
    {
        public IEnumerable<PurchaseType> GetAll()
        {
            List<PurchaseType> purchaseTypes = new List<PurchaseType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PurchaseType row = new PurchaseType();

                        row.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        row.PurchaseTypeName = dr["PurchaseTypeName"].ToString();

                        purchaseTypes.Add(row);
                    }
                }
            }

            return purchaseTypes;
        }
    }
}
