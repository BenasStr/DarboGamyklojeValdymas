using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using DarbasGamykloje.ViewModels;
using MySql.Data.MySqlClient;

namespace DarbasGamykloje.Repos
{
    public class LivingSpaceRepository
    {

        public List<LivingSpaceListView> GetLivingSpaces()
        {
            List<LivingSpaceListView> LivingSpaces = new List<LivingSpaceListView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"SELECT DISTINCT adress FROM livingspace";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                LivingSpaces.Add(new LivingSpaceListView
                {
                    adress = Convert.ToString(dr["adress"]),
                });
            }

            return LivingSpaces;
        }
    }
}