using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class InteriorsRepository : IInteriorsRepository
    {
        public IEnumerable<Interior> GetAll()
        {
            List<Interior> interiors = new List<Interior>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InteriorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Interior row = new Interior();

                        row.InteriorId = (int)dr["InteriorId"];
                        row.InteriorType = dr["InteriorType"].ToString();

                        interiors.Add(row);
                    }
                }
            }

            return interiors;
        }
    }
}
