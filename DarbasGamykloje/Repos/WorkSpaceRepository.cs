using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using DarbasGamykloje.ViewModels;
using DarbasGamykloje.ViewModels.WorkSpace;
using MySql.Data.MySqlClient;

namespace DarbasGamykloje.Repos
{
    public class WorksSpaceRepository
    {
        public bool AddNewWorkSpace(AddWorkspaceView WorkSpaceView)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = "INSERT INTO workspace (`name`, `description`, `fk_Factoryid_Factory`) VALUES (?name, ?description, ?fk_Factoryid_Factory)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?name", MySqlDbType.VarChar).Value = WorkSpaceView.name;
            mySqlCommand.Parameters.Add("?description", MySqlDbType.Int32).Value = WorkSpaceView.description;
            mySqlCommand.Parameters.Add("?fk_Factoryid_Factory", MySqlDbType.Int32).Value = WorkSpaceView.fk_Factoryid_Factory;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public List<AddWorkspaceView> GetFactoryWorkspaces(int id)
        {
            List<AddWorkspaceView> workspaces = new List<AddWorkspaceView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = "SELECT * FROM workspace WHERE fk_Factoryid_Factory = ?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                workspaces.Add(new AddWorkspaceView
                {
                    name = Convert.ToString(dr["name"]),
                    description = Convert.ToString(dr["description"]),
                    id_Workspace = Convert.ToInt32(dr["id_Workspace"]),
                    fk_Factoryid_Factory = Convert.ToInt32(dr["fk_Factoryid_Factory"])
                });
            }
            return workspaces;
        }

    }
}