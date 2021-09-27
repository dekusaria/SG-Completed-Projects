using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        public IEnumerable<VehicleType> GetAll()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleType row = new VehicleType();

                        row.VehicleTypeId = (int)dr["VehicleTypeId"];
                        row.VehicleTypeName = dr["VehicleTypeName"].ToString();

                        vehicleTypes.Add(row);
                    }
                }
            }

            return vehicleTypes;
        }
    }
}
