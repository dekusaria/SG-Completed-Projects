using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class BodyStyleRepository : IBodyStyleRepository
    {
        public IEnumerable<BodyStyle> GetAll()
        {
            List<BodyStyle> bodystyles = new List<BodyStyle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BodyStylesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyStyle row = new BodyStyle();

                        row.BodyStyleId = (int)dr["BodyStyleId"];
                        row.BodyStyleType = dr["BodyStyleType"].ToString();

                        bodystyles.Add(row);
                    }
                }
            }

            return bodystyles;
        }
    }
}
