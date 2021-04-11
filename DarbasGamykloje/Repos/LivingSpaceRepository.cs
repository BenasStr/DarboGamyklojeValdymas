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

        public List<EditLivingSpaceView> GetLivingSpaceById(string id)
        {
            List<EditLivingSpaceView> LivingSpaces = new List<EditLivingSpaceView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"SELECT * FROM livingspace WHERE adress = '" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                LivingSpaces.Add(new EditLivingSpaceView
                {
                    adress = Convert.ToString(dr["adress"]),
                    roomNumber = Convert.ToInt32(dr["roomNumber"]),
                    maxCapacity = Convert.ToInt32(dr["maxCapacity"]),
                    id_LivingSpace = Convert.ToInt32(dr["id_LivingSpace"]),
                }); 
            }
            return LivingSpaces;
        }

        public EditLivingSpaceView GetRoomById(int id)
        {
            EditLivingSpaceView LivingSpaces = new EditLivingSpaceView();

            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"SELECT * FROM livingspace WHERE id_LivingSpace = '" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                LivingSpaces.adress = Convert.ToString(dr["adress"]);
                LivingSpaces.roomNumber = Convert.ToInt32(dr["roomNumber"]);
                LivingSpaces.maxCapacity = Convert.ToInt32(dr["maxCapacity"]);
                LivingSpaces.id_LivingSpace = Convert.ToInt32(dr["id_LivingSpace"]);
            }

            return LivingSpaces;
        }

        public bool EditExistingRoom(EditLivingSpaceView Room)
        {

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE livingspace a SET a.maxCapacity=?maxCapacity WHERE a.id_LivingSpace=?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?maxCapacity", MySqlDbType.VarChar).Value = Room.maxCapacity;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = Room.id_LivingSpace;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}