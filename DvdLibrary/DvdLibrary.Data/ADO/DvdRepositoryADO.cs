using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Tables;
using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace DvdLibrary.Data.ADO
{
    public class DvdRepositoryADO : IDvdRepository
    {
        public void Delete(int dvdId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<DvdItem> GetAll()
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.ReleaseYear = (int)dr["ReleaseYear"];
                        if (dr["DirectorName"] != DBNull.Value)
                            row.DirectorName = dr["DirectorName"].ToString();
                        if (dr["RatingName"] != DBNull.Value)
                            row.RatingName = dr["RatingName"].ToString();
                        if (dr["Notes"] != DBNull.Value)
                            row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public DvdItem GetDvdById(int dvdId)
        {
            DvdItem dvd = null;

            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        dvd = new DvdItem();
                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseYear = (int)dr["ReleaseYear"];
                        if (dr["DirectorName"] != DBNull.Value)
                            dvd.DirectorName = dr["DirectorName"].ToString();
                        if (dr["RatingName"] != DBNull.Value)
                            dvd.RatingName = dr["RatingName"].ToString();
                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();
                    }
                }
            }

            return dvd;
        }

        public void Insert(DvdItem dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@DvdId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.ReleaseYear);

                if (string.IsNullOrEmpty(dvd.DirectorName))
                    cmd.Parameters.AddWithValue("@DirectorName", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@DirectorName", dvd.DirectorName);

                if (string.IsNullOrEmpty(dvd.RatingName))
                    cmd.Parameters.AddWithValue("@RatingName", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@RatingName", dvd.RatingName);
                if (string.IsNullOrEmpty(dvd.Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                cn.Open();

                cmd.ExecuteNonQuery();

                dvd.DvdId = (int)param.Value;
            }
        }

        public IEnumerable<DvdItem> SearchByDirector(string directorName)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSelectByDirectorName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DirectorName", directorName);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.ReleaseYear = (int)dr["ReleaseYear"];
                        if (dr["DirectorName"] != DBNull.Value)
                            row.DirectorName = dr["DirectorName"].ToString();
                        if (dr["RatingName"] != DBNull.Value)
                            row.RatingName = dr["RatingName"].ToString();
                        if (dr["Notes"] != DBNull.Value)
                            row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public IEnumerable<DvdItem> SearchByRating(string ratingName)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSelectByRatingName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RatingName", ratingName);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.ReleaseYear = (int)dr["ReleaseYear"];
                        if (dr["DirectorName"] != DBNull.Value)
                            row.DirectorName = dr["DirectorName"].ToString();
                        if (dr["RatingName"] != DBNull.Value)
                            row.RatingName = dr["RatingName"].ToString();
                        if (dr["Notes"] != DBNull.Value)
                            row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public IEnumerable<DvdItem> SearchByReleaseYear(int releaseYear)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSelectByReleaseYear", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReleaseYear", releaseYear);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.ReleaseYear = (int)dr["ReleaseYear"];
                        if (dr["DirectorName"] != DBNull.Value)
                            row.DirectorName = dr["DirectorName"].ToString();
                        if (dr["RatingName"] != DBNull.Value)
                            row.RatingName = dr["RatingName"].ToString();
                        if (dr["Notes"] != DBNull.Value)
                            row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public IEnumerable<DvdItem> SearchByTitle(string title)
        {
            List<DvdItem> dvds = new List<DvdItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSelectByTitle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", title);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdItem row = new DvdItem();

                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.ReleaseYear = (int)dr["ReleaseYear"];
                        if (dr["DirectorName"] != DBNull.Value)
                            row.DirectorName = dr["DirectorName"].ToString();
                        if (dr["RatingName"] != DBNull.Value)
                            row.RatingName = dr["RatingName"].ToString();
                        if (dr["Notes"] != DBNull.Value)
                            row.Notes = dr["Notes"].ToString();

                        dvds.Add(row);
                    }
                }
            }

            return dvds;
        }

        public void Update(DvdItem dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvd.DvdId);
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.ReleaseYear);
                if (string.IsNullOrEmpty(dvd.DirectorName))
                    cmd.Parameters.AddWithValue("@DirectorName", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@DirectorName", dvd.DirectorName);

                if (string.IsNullOrEmpty(dvd.RatingName))
                    cmd.Parameters.AddWithValue("@RatingName", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@RatingName", dvd.RatingName);
                if (string.IsNullOrEmpty(dvd.Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
