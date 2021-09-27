using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class ColorsRepository : IColorsRepository
    {
        public IEnumerable<Color> GetAll()
        {
            List<Color> colors = new List<Color>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Color row = new Color();

                        row.ColorId = (int)dr["ColorId"];
                        row.ColorName = dr["ColorName"].ToString();

                        colors.Add(row);
                    }
                }
            }

            return colors;
        }
    }
}
