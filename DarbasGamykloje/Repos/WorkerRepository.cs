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

        public List<WorkerList> GetWorkerByFactoryId(int id)
        {
            List<WorkerList> worker = new List<WorkerList>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);

            string sqlQuery = "SELECT registereduser.name, registereduser.surname, worker.id_Worker" +
                " FROM factory" +
                " INNER JOIN worker ON worker.fk_Factoryid_Factory = ?id" +
                " INNER JOIN registereduser ON worker.fk_RegisteredUserid_RegisteredUser = registereduser.id_RegisteredUser";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);

            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();

            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                worker.Add(new WorkerList
                {
                    Name = Convert.ToString(item["name"]),
                    Surname = Convert.ToString(item["surname"]),
                    id_Worker = Convert.ToInt32(item["id_Worker"]),
                    //Salary = Convert.ToInt32(item["salary"])
                });
            }

            return worker;
        }

        public string CheckWorkerType(int id)
        {
            int count = 0;

            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);

            string sqlQuery = "SELECT COUNT(motivatedworker.fk_Workerid_Worker) as count FROM motivatedworker WHERE motivatedworker.fk_Workerid_Worker = ?id";

            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);

            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();

            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            count = Convert.ToInt32(dt.Rows[0]["count"]);

            if (count > 0)
                return "MotivatedWorker";

            return "MotivatingWorker";
        }
    }
}