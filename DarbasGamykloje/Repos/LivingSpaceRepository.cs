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

        public bool AddNewLivingSpace(AddLivingSpaceview LivingSpaceView)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"INSERT INTO livingspace ('adress', 'roomnumber', 'maxcapacity') VALUES (?adress, ?roomnumber, ?maxcapacity)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?adress", MySqlDbType.VarChar).Value = LivingSpaceView.adress;
            mySqlCommand.Parameters.Add("?roomnumber", MySqlDbType.Int32).Value = LivingSpaceView.roomNumber;
            mySqlCommand.Parameters.Add("?maxcapacity", MySqlDbType.Int32).Value = LivingSpaceView.maxCapacity;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

    }
}