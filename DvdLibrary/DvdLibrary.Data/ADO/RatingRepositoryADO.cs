using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DvdLibrary.Data.ADO
{
    public class RatingRepositoryADO : IRatingRepository
    {
        public IEnumerable<Rating> GetAll()
        {
            List<Rating> ratings = new List<Rating>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("RatingsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Rating row = new Rating();
                        row.RatingId = (int)dr["RatingId"];
                        row.RatingName = dr["RatingName"].ToString();

                        ratings.Add(row);
                    }
                }
            }

            return ratings;
        }

        public Rating GetRatingById(int ratingId)
        {
            Rating rating = new Rating();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("RatingsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RatingId", ratingId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        rating.RatingId = (int)dr["RatingId"];
                        rating.RatingName = dr["RatingName"].ToString();
                    }
                }
            }

            return rating;
        }
    }
}
