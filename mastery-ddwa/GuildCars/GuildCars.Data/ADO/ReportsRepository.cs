using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class ReportsRepository : IReportsRepository
    {
        public IEnumerable<InventoryReport> GetInventoryByVehicleType(int vehicleTypeId)
        {
            List<InventoryReport> reports = new List<InventoryReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReportsSelectByVehicleType", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleTypeId", vehicleTypeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReport row = new InventoryReport();

                        row.Year = (int)dr["Year"];
                        row.Make = dr["MakeName"].ToString();
                        row.Model = dr["ModelName"].ToString();
                        row.Count = (int)dr["Count"];
                        row.StockValue = (decimal)dr["StockValue"];

                        reports.Add(row);
                    }
                }
            }

            return reports;
        }

        public IEnumerable<SalesReport> GetSalesReports()
        {
            List<SalesReport> reports = new List<SalesReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesReportsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReport row = new SalesReport();

                        row.UserName = dr["Name"].ToString();
                        row.TotalSales = (decimal)dr["TotalSales"];
                        row.TotalVehicles = (int)dr["TotalVehicles"];

                        reports.Add(row);
                    }
                }
            }

            return reports;
        }

        public IEnumerable<SalesReport> SearchSalesReports(SalesSearchParameters param)
        {
            List<SalesReport> reports = new List<SalesReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesReportsSelectBySearch", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (string.IsNullOrEmpty(param.UserEmail))
                    cmd.Parameters.AddWithValue("@UserEmail", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@UserEmail", param.UserEmail);

                if (param.MinDate == null)
                    cmd.Parameters.AddWithValue("@MinDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MinDate", param.MinDate);

                if (param.MaxDate == null)
                    cmd.Parameters.AddWithValue("@MaxDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MaxDate", param.MaxDate);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReport row = new SalesReport();

                        row.UserName = dr["Name"].ToString();
                        row.TotalSales = (decimal)dr["TotalSales"];
                        row.TotalVehicles = (int)dr["TotalVehicles"];

                        reports.Add(row);
                    }
                }
            }

            return reports;
        }
    }
}
