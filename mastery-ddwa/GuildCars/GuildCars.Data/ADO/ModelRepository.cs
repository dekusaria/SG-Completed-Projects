using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class ModelRepository : IModelRepository
    {
        public void Delete(int modelId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModelId", modelId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Model> GetAll()
        {
            List<Model> models = new List<Model>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model row = new Model();

                        row.ModelId = (int)dr["ModelId"];
                        row.MakeId = (int)dr["MakeId"];
                        row.ModelName = dr["ModelName"].ToString();
                        row.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());
                        row.EmployeeEmail = dr["EmployeeEmail"].ToString();

                        models.Add(row);
                    }
                }
            }

            return models;
        }

        public Model GetById(int modelId)
        {
            Model model = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModelId", modelId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        model = new Model();

                        model.ModelId = (int)dr["ModelId"];
                        model.MakeId = (int)dr["MakeId"];
                        model.ModelName = dr["ModelName"].ToString();
                        model.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());
                        model.EmployeeEmail = dr["EmployeeEmail"].ToString();
                    }
                }
            }

            return model;
        }

        public IEnumerable<Model> GetModelsByMakeId(int makeId)
        {
            List<Model> models = new List<Model>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsSelectByMakeId", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeId", makeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model row = new Model();

                        row.ModelId = (int)dr["ModelId"];
                        row.MakeId = (int)dr["MakeId"];
                        row.ModelName = dr["ModelName"].ToString();
                        row.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());
                        row.EmployeeEmail = dr["EmployeeEmail"].ToString();

                        models.Add(row);
                    }
                }
            }

            return models;
        }

        public void Insert(Model model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeId", model.MakeId);
                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@DateAdded", model.DateAdded);
                cmd.Parameters.AddWithValue("@EmployeeEmail", model.EmployeeEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                model.ModelId = (int)param.Value;
            }
        }

        public void Update(Model model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModelId", model.ModelId);
                cmd.Parameters.AddWithValue("@MakeId", model.MakeId);
                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@DateAdded", model.DateAdded);
                cmd.Parameters.AddWithValue("@EmployeeEmail", model.EmployeeEmail);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
