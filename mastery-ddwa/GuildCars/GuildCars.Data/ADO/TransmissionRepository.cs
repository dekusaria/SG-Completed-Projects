using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class TransmissionRepository : ITransmissionRepository
    {
        public IEnumerable<Transmission> GetAll()
        {
            List<Transmission> transmissions = new List<Transmission>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TransmissionsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Transmission row = new Transmission();

                        row.TransmissionId = (int)dr["TransmissionId"];
                        row.TransmissionType = dr["TransmissionType"].ToString();

                        transmissions.Add(row);
                    }
                }
            }

            return transmissions;
        }
    }
}
