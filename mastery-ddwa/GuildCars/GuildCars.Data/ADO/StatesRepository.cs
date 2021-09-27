using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GuildCars.Data.ADO
{
    public class StatesRepository : IStatesRepository
    {
        public IEnumerable<State> GetAll()
        {
            List<State> states = new List<State>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("StatesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        State row = new State();

                        row.StateId = dr["StateId"].ToString();
                        row.StateName = dr["StateName"].ToString();

                        states.Add(row);
                    }
                }
            }

            return states;
        }
    }
}
