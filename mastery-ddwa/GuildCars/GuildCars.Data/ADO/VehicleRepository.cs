using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace GuildCars.Data.ADO
{
    public class VehicleRepository : IVehicleRepository
    {
        public void Delete(int vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Vehicle> GetAll()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Vehicle row = new Vehicle();

                        row.VehicleId = (int)dr["VehicleId"];
                        row.Vin = dr["Vin"].ToString();
                        row.VehicleTypeId = (int)dr["VehicleTypeId"];
                        row.ModelId = (int)dr["ModelId"];
                        row.ColorId = (int)dr["ColorId"];
                        row.InteriorId = (int)dr["InteriorId"];
                        row.BodyStyleId = (int)dr["BodyStyleId"];
                        row.TransmissionId = (int)dr["TransmissionId"];
                        row.Year = (int)dr["Year"];
                        row.Mileage = (int)dr["Mileage"];
                        row.Msrp = (decimal)dr["Msrp"];
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.Description = dr["Description"].ToString();
                        row.ImageFileName = dr["ImageFileName"].ToString();
                        row.IsFeatured = (bool)dr["IsFeatured"];
                        row.IsSold = (bool)dr["IsSold"];

                        vehicles.Add(row);
                    }
                }
            }

            return vehicles;
        }

        public Vehicle GetById(int vehicleId)
        {
            Vehicle vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new Vehicle();
                        vehicle.VehicleId = (int)dr["VehicleId"];
                        vehicle.Vin = dr["Vin"].ToString();
                        vehicle.VehicleTypeId = (int)dr["VehicleTypeId"];
                        vehicle.ModelId = (int)dr["ModelId"];
                        vehicle.ColorId = (int)dr["ColorId"];
                        vehicle.InteriorId = (int)dr["InteriorId"];
                        vehicle.BodyStyleId = (int)dr["BodyStyleId"];
                        vehicle.TransmissionId = (int)dr["TransmissionId"];
                        vehicle.Year = (int)dr["Year"];
                        vehicle.Mileage = (int)dr["Mileage"];
                        vehicle.Msrp = (decimal)dr["Msrp"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.ImageFileName = dr["ImageFileName"].ToString();
                        vehicle.IsFeatured = (bool)dr["IsFeatured"];
                        vehicle.IsSold = (bool)dr["IsSold"];
                    }
                }
            }

            return vehicle;
        }

        public IEnumerable<VehicleItem> GetByVehicleType(int vehicleTypeId)
        {
            List<VehicleItem> vehicles = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectByVehicleType", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleTypeId", vehicleTypeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleItem row = new VehicleItem();

                        row.VehicleId = (int)dr["VehicleId"];
                        row.Vin = dr["Vin"].ToString();
                        row.VehicleTypeName = dr["VehicleTypeName"].ToString();
                        row.ModelName = dr["ModelName"].ToString();
                        row.MakeName = dr["MakeName"].ToString();
                        row.ColorName = dr["ColorName"].ToString();
                        row.InteriorType = dr["InteriorType"].ToString();
                        row.BodyStyleType = dr["BodyStyleType"].ToString();
                        row.TransmissionType = dr["TransmissionType"].ToString();
                        row.Year = (int)dr["Year"];
                        row.Mileage = (int)dr["Mileage"];
                        row.Msrp = (decimal)dr["Msrp"];
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.Description = dr["Description"].ToString();
                        row.ImageFileName = dr["ImageFileName"].ToString();
                        row.IsFeatured = (bool)dr["IsFeatured"];
                        row.IsSold = (bool)dr["IsSold"];

                        vehicles.Add(row);
                    }
                }
            }

            return vehicles;
        }

        public VehicleItem GetDetails(int vehicleId)
        {
            VehicleItem vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new VehicleItem();

                        vehicle.VehicleId = (int)dr["VehicleId"];
                        vehicle.Vin = dr["Vin"].ToString();
                        vehicle.VehicleTypeName = dr["VehicleTypeName"].ToString();
                        vehicle.ModelName = dr["ModelName"].ToString();
                        vehicle.MakeName = dr["MakeName"].ToString();
                        vehicle.ColorName = dr["ColorName"].ToString();
                        vehicle.InteriorType = dr["InteriorType"].ToString();
                        vehicle.BodyStyleType = dr["BodyStyleType"].ToString();
                        vehicle.TransmissionType = dr["TransmissionType"].ToString();
                        vehicle.Year = (int)dr["Year"];
                        vehicle.Mileage = (int)dr["Mileage"];
                        vehicle.Msrp = (decimal)dr["Msrp"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.ImageFileName = dr["ImageFileName"].ToString();
                        vehicle.IsFeatured = (bool)dr["IsFeatured"];
                        vehicle.IsSold = (bool)dr["IsSold"];
                    }
                }
            }

            return vehicle;
        }

        public IEnumerable<VehicleShortItem> GetFeaturedVehicles()
        {
            List<VehicleShortItem> vehicles = new List<VehicleShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectFeatured", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleShortItem row = new VehicleShortItem();

                        row.VehicleId = (int)dr["VehicleId"];
                        row.Vin = dr["Vin"].ToString();
                        row.ModelName = dr["ModelName"].ToString();
                        row.MakeName = dr["MakeName"].ToString();
                        row.Year = (int)dr["Year"];
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.ImageFileName = dr["ImageFileName"].ToString();

                        vehicles.Add(row);
                    }
                }
            }

            return vehicles;
        }

        public void Insert(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@VehicleId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Vin", vehicle.Vin);
                cmd.Parameters.AddWithValue("@VehicleTypeId", vehicle.VehicleTypeId);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@ColorId", vehicle.ColorId);
                cmd.Parameters.AddWithValue("@InteriorId", vehicle.InteriorId);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@TransmissionId", vehicle.TransmissionId);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@Msrp", vehicle.Msrp);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@ImageFileName", vehicle.ImageFileName);
                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsSold", vehicle.IsSold);

                cn.Open();

                cmd.ExecuteNonQuery();

                vehicle.VehicleId = (int)param.Value;
            }
        }

        public IEnumerable<VehicleItem> SearchVehicles(VehicleSearchParameters param)
        {
            List<VehicleItem> vehicles = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSearch", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (param.VehicleTypeId == 0 || param.VehicleTypeId == null)
                    cmd.Parameters.AddWithValue("@VehicleTypeId", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@VehicleTypeId", param.VehicleTypeId);

                if (string.IsNullOrEmpty(param.SearchTerm))
                    cmd.Parameters.AddWithValue("@SearchTerm", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@SearchTerm", param.SearchTerm);

                if (param.MinPrice == 0M || param.MinPrice == null)
                    cmd.Parameters.AddWithValue("@MinPrice", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MinPrice", param.MinPrice);

                if (param.MaxPrice == 0M || param.MaxPrice == null)
                    cmd.Parameters.AddWithValue("@MaxPrice", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MaxPrice", param.MaxPrice);

                if (param.MinYear == 0M || param.MinYear == null)
                    cmd.Parameters.AddWithValue("@MinYear", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MinYear", param.MinYear);

                if (param.MaxYear == 0M || param.MaxYear == null)
                    cmd.Parameters.AddWithValue("@MaxYear", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MaxYear", param.MaxYear);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleItem row = new VehicleItem();

                        row.VehicleId = (int)dr["VehicleId"];
                        row.Vin = dr["Vin"].ToString();
                        row.VehicleTypeName = dr["VehicleTypeName"].ToString();
                        row.ModelName = dr["ModelName"].ToString();
                        row.MakeName = dr["MakeName"].ToString();
                        row.ColorName = dr["ColorName"].ToString();
                        row.InteriorType = dr["InteriorType"].ToString();
                        row.BodyStyleType = dr["BodyStyleType"].ToString();
                        row.TransmissionType = dr["TransmissionType"].ToString();
                        row.Year = (int)dr["Year"];
                        row.Mileage = (int)dr["Mileage"];
                        row.Msrp = (decimal)dr["Msrp"];
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.ImageFileName = dr["ImageFileName"].ToString();

                        vehicles.Add(row);
                    }
                }
            }

            return vehicles;
        }

        public void Update(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@Vin", vehicle.Vin);
                cmd.Parameters.AddWithValue("@VehicleTypeId", vehicle.VehicleTypeId);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@ColorId", vehicle.ColorId);
                cmd.Parameters.AddWithValue("@InteriorId", vehicle.InteriorId);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@TransmissionId", vehicle.TransmissionId);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@Msrp", vehicle.Msrp);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@ImageFileName", vehicle.ImageFileName);
                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsSold", vehicle.IsSold);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void VehicleIsSold(int vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesIsSoldUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
