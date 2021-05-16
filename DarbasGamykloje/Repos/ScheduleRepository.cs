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
        public ScheduleListView GetScheduleByItemId(int id)
        {
            ScheduleListView Schedule = new ScheduleListView();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"SELECT * FROM schedule WHERE id_Schedule = ?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            Schedule.startDate = Convert.ToDateTime(dt.Rows[0]["startDate"]);
            Schedule.endDate = Convert.ToDateTime(dt.Rows[0]["endDate"]);
            Schedule.id_Schedule = Convert.ToInt32(dt.Rows[0]["id_Schedule"]);
            Schedule.fk_Workerid_Worker = Convert.ToInt32(dt.Rows[0]["fk_Workerid_Worker"]);

            return Schedule;
        }

        public List<ScheduleListView> GetScheduleByFactoryId(int id)
        {
            List<ScheduleListView> Schedule = new List<ScheduleListView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"SELECT * FROM schedule 
                                INNER JOIN worker ON schedule.fk_Workerid_Worker = worker.id_Worker  
                                WHERE worker.fk_Factoryid_Factory = ?id";
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
        public bool addSchedule(ScheduleListView model)
        {
            List<FactoryListView> Factories = new List<FactoryListView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"INSERT INTO `schedule`(`startDate`, `endDate`, `id_Schedule`, `fk_Workerid_Worker`) VALUES 
                                (?startDate,?endDate,NULL, ?fk_Workerid_Worker)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);

            mySqlCommand.Parameters.Add("?startDate", MySqlDbType.VarChar).Value = model.startDate;
            mySqlCommand.Parameters.Add("?endDate", MySqlDbType.VarChar).Value = model.endDate;
            mySqlCommand.Parameters.Add("?fk_Workerid_Worker", MySqlDbType.Int32).Value = model.fk_Workerid_Worker;


            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool updateSchedule(ScheduleListView schedule)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE schedule a SET a.startDate=?startDate,
                                                      a.endDate=?endDate,
                                                      a.id_Schedule =?id_Schedule,
                                                      a.fk_Workerid_Worker =?fk_Workerid_Worker  
                                                WHERE a.id_Schedule   =" + schedule.id_Schedule;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?startDate", MySqlDbType.Date).Value = schedule.startDate;
            mySqlCommand.Parameters.Add("?endDate", MySqlDbType.Date).Value = schedule.endDate;
            mySqlCommand.Parameters.Add("?id_Schedule", MySqlDbType.Int32).Value = schedule.id_Schedule;
            mySqlCommand.Parameters.Add("?fk_Workerid_Worker", MySqlDbType.Int32).Value = schedule.fk_Workerid_Worker;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }


    }
}