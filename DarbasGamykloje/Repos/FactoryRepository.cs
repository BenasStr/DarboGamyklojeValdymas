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
    public class FactoryRepository
    {
        public List<FactoryListView> GetAllFactories()
        {
            List<FactoryListView> Factories = new List<FactoryListView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"SELECT  * FROM factory";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Factories.Add(new FactoryListView
                {
                    maxCapacity = Convert.ToInt32(dr["maxCapacity"]),
                    id_Factory = Convert.ToInt32(dr["id_Factory"]),
                    fk_Managerid_Manager = Convert.ToInt32(dr["fk_Managerid_Manager"])
                });
            }

            return Factories;
        }

        public bool addWorkspace(AddWorkspaceView model)
        {
            List<FactoryListView> Factories = new List<FactoryListView>();
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);
            string sqlQuery = @"INSERT INTO `workspace`(`name`, `description`, `id_Workspace`, `fk_Factoryid_Factory`) VALUES 
                                (?name,?description,NULL, ?fk_Factoryid_Factory)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);

            mySqlCommand.Parameters.Add("?name", MySqlDbType.VarChar).Value = model.name;
            mySqlCommand.Parameters.Add("?description", MySqlDbType.VarChar).Value = model.description;
            mySqlCommand.Parameters.Add("?fk_Factoryid_Factory", MySqlDbType.Int32).Value = model.fk_Factoryid_Factory;
       

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }
    }
}