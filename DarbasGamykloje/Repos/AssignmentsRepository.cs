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
    public class AssignmentsRepository
    {
        public List<AssignmentsListView> GetAssignments(int id)
        {
            List<AssignmentsListView> Assignments = new List<AssignmentsListView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = "SELECT * FROM assignments WHERE fk_Scheduleid_Schedule = ?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Assignments.Add(new AssignmentsListView
                {
                    startTime = Convert.ToDateTime(dr["startTime"]),
                    endTime = Convert.ToDateTime(dr["endTime"]),
                    isCompleted = Convert.ToBoolean(dr["isCompleted"]),
                    id_Assignments = Convert.ToInt32(dr["id_Assignments"]),
                    fk_Workspaceid_Workspace = Convert.ToInt32(dr["fk_Workspaceid_Workspace"]),
                    fk_Scheduleid_Schedule = Convert.ToInt32(dr["fk_Scheduleid_Schedule"])
                });
            }

            return Assignments;
        }

        public AssignmentDetailsView GetDetailedAssignmentById(int id)
        {
            AssignmentDetailsView Assignments = new AssignmentDetailsView();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = "SELECT a.name, a.description from workspace AS a, assignments AS b WHERE a.id_Workspace = b.fk_Workspaceid_Workspace AND b.id_Assignments = ?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            Assignments.name = Convert.ToString(dt.Rows[0]["name"]);
            Assignments.description = Convert.ToString(dt.Rows[0]["description"]);

            return Assignments;
        }

        public int CountsCompletedAssignments(int id)
        {
            int count = 0;

            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);

            string sqlQuery = "SELECT COUNT(assignments.isCompleted) AS count " +
                " FROM schedule" +
                " INNER JOIN assignments" +
                " ON assignments.fk_Scheduleid_Schedule = schedule.id_Schedule" +
                " WHERE schedule.fk_Workerid_Worker = ?id" +
                " AND assignments.isCompleted = 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);

            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            count = Convert.ToInt32(dt.Rows[0]["count"]);
            return count;
        }
    }
}