using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DvdLibrary.Data.ADO
{
    public class DirectorRepositoryADO : IDirectorRepository
    {
        public IEnumerable<Director> GetAll()
        {
            List<Director> directors = new List<Director>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DirectorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Director row = new Director();
                        row.DirectorId = (int)dr["DirectorId"];
                        row.DirectorName = dr["DirectorName"].ToString();
                        directors.Add(row);
                    }
                }
            }

            return directors;
        }

        public Director GetDirectorById(int directorId)
        {
            Director director = new Director();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DirectorsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DirectorId", directorId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        director.DirectorId = (int)dr["DirectorId"];
                        director.DirectorName = dr["DirectorName"].ToString();
                    }
                }
            }

            return director;
        }

        public Director GetDirectorByName(string directorName)
        {
            Director director = new Director();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DirectorsSelectByName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DirectorName", directorName);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        director.DirectorId = (int)dr["DirectorId"];
                        director.DirectorName = dr["DirectorName"].ToString();
                    }
                }
            }

            return director;
        }
    }
}
