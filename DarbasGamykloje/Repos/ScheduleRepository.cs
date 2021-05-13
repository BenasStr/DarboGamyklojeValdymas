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
    public class ScheduleRepository
    {
        public List<ScheduleListView> GetScheduleById(int id)
        {
            List<ScheduleListView> Schedule = new List<ScheduleListView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"SELECT * FROM schedule WHERE fk_Workerid_Worker = ?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Schedule.Add(new ScheduleListView
                {
                    startDate = Convert.ToDateTime(dr["startDate"]),
                    endDate = Convert.ToDateTime(dr["endDate"]),
                    id_Schedule = Convert.ToInt32(dr["id_Schedule"]),
                    fk_Workerid_Worker = Convert.ToInt32(dr["fk_Workerid_Worker"])
                });
            }

            return Schedule;
        }
    }
}