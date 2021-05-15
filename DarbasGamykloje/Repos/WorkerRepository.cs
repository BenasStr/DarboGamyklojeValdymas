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
    public class WorkerRepository
    {
        public WorkerView GetWorkerById(int id)
        {
            WorkerView worker = new WorkerView();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = "SELECT * FROM worker WHERE id_Worker = ?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            worker.salary = Convert.ToInt32(dt.Rows[0]["salary"]);
            worker.isDeleted = Convert.ToBoolean(dt.Rows[0]["isDeleted"]);
            worker.numberOfDaysWorked = Convert.ToInt32(dt.Rows[0]["numberOfDaysWorked"]);
            worker.checkedSalaryCount = Convert.ToInt32(dt.Rows[0]["checkedSalaryCount"]);
            worker.id_Worker = Convert.ToInt32(dt.Rows[0]["id_Worker"]);
            worker.fk_Managerid_Manager = Convert.ToInt32(dt.Rows[0]["fk_Managerid_Manager"]);
            worker.fk_LivingSpaceid_LivingSpace = Convert.ToInt32(dt.Rows[0]["fk_LivingSpaceid_LivingSpace"]);
            worker.fk_RegisteredUserid_RegisteredUser = Convert.ToInt32(dt.Rows[0]["fk_RegisteredUserid_RegisteredUser"]);
            worker.fk_Factoryid_Factory = Convert.ToInt32(dt.Rows[0]["fk_Factoryid_Factory"]);
            return worker;
        }

        public List<WorkerView> GetFactoryWorkers(int id)
        {
            List<WorkerView> workers = new List<WorkerView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = "SELECT * FROM worker WHERE fk_Factoryid_Factory = ?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                workers.Add(new WorkerView
                {
                    salary = Convert.ToInt32(dr["salary"]),
                    isDeleted = Convert.ToBoolean(dr["isDeleted"]),
                    numberOfDaysWorked = Convert.ToInt32(dr["numberOfDaysWorked"]),
                    checkedSalaryCount = Convert.ToInt32(dr["checkedSalaryCount"]),
                    id_Worker = Convert.ToInt32(dr["id_Worker"]),
                    fk_Managerid_Manager = Convert.ToInt32(dr["fk_Managerid_Manager"]),
                    fk_LivingSpaceid_LivingSpace = Convert.ToInt32(dr["fk_LivingSpaceid_LivingSpace"]),
                    fk_RegisteredUserid_RegisteredUser = Convert.ToInt32(dr["fk_RegisteredUserid_RegisteredUser"]),
                    fk_Factoryid_Factory = Convert.ToInt32(dr["fk_Factoryid_Factory"])
                });
            }
            return workers;
        }
        public List<WorkerView> GetFactoryWorkersThatAreFreeBetween(int id, DateTime from, DateTime to)
        {
            List<WorkerView> workers = new List<WorkerView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery =   "SELECT * FROM worker " +
                                "WHERE fk_Factoryid_Factory = ?id " +
                                "AND worker.id_Worker NOT IN (" +
                                    "SELECT schedule.fk_Workerid_Worker " +
                                    "FROM `schedule` " +
                                    "WHERE schedule.startDate < ?to " +
                                    "AND schedule.endDate > ?from)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlCommand.Parameters.Add("?from", MySqlDbType.Date).Value = from;
            mySqlCommand.Parameters.Add("?to", MySqlDbType.Date).Value = to;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                workers.Add(new WorkerView
                {
                    salary = Convert.ToInt32(dr["salary"]),
                    isDeleted = Convert.ToBoolean(dr["isDeleted"]),
                    numberOfDaysWorked = Convert.ToInt32(dr["numberOfDaysWorked"]),
                    checkedSalaryCount = Convert.ToInt32(dr["checkedSalaryCount"]),
                    id_Worker = Convert.ToInt32(dr["id_Worker"]),
                    fk_Managerid_Manager = Convert.ToInt32(dr["fk_Managerid_Manager"]),
                    fk_LivingSpaceid_LivingSpace = Convert.ToInt32(dr["fk_LivingSpaceid_LivingSpace"]),
                    fk_RegisteredUserid_RegisteredUser = Convert.ToInt32(dr["fk_RegisteredUserid_RegisteredUser"]),
                    fk_Factoryid_Factory = Convert.ToInt32(dr["fk_Factoryid_Factory"])
                });
            }
            return workers;
        }
        public int GetFactoryWorkerCountBetween(int id, DateTime from, DateTime to)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery =   "SELECT COUNT(DISTINCT worker.id_Worker) " +
                                "FROM `worker` INNER JOIN schedule ON schedule.fk_Workerid_Worker = worker.id_Worker " +
                                "WHERE worker.fk_Factoryid_Factory = 1 " +
                                  "AND schedule.startDate < '2021-05-05' " +
                                  "AND schedule.endDate > '2021-05-03'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlCommand.Parameters.Add("?from", MySqlDbType.Date).Value = from;
            mySqlCommand.Parameters.Add("?to", MySqlDbType.Date).Value = to;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            return Convert.ToInt32(dt.Rows[0]["COUNT(DISTINCT worker.id_Worker)"]);
;
        }
    }
}