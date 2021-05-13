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
    }
}