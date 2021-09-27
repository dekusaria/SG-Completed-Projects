using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class MakeRepository : IMakeRepository
    {
        public void Delete(int makeId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeId", makeId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Make> GetAll()
        {

            List<Make> makes = new List<Make>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Make row = new Make();

                        row.MakeId = (int)dr["MakeId"];
                        row.MakeName = dr["MakeName"].ToString();
                        row.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());
                        row.EmployeeEmail = dr["EmployeeEmail"].ToString();

                        makes.Add(row);
                    }
                }
            }

            return makes;
        }

        public Make GetById(int makeId)
        {
            Make make = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeId", makeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        make = new Make();

                        make.MakeId = (int)dr["MakeId"];
                        make.MakeName = dr["MakeName"].ToString();
                        make.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());
                        make.EmployeeEmail = dr["EmployeeEmail"].ToString();
                    }
                }
            }

            return make;
        }

        public void Insert(Make make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);
                cmd.Parameters.AddWithValue("@DateAdded", make.DateAdded);
                cmd.Parameters.AddWithValue("@EmployeeEmail", make.EmployeeEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                make.MakeId = (int)param.Value;
            }
        }

        public void Update(Make make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeId", make.MakeId);
                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);
                cmd.Parameters.AddWithValue("@DateAdded", make.DateAdded);
                cmd.Parameters.AddWithValue("@EmployeeEmail", make.EmployeeEmail);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
