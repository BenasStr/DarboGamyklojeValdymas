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

        public double SumProfitFromFactories(int id)
        {
            double sum = 0;

            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);

            string sqlQuery = @"SELECT SUM(product.value) as sum 
                                FROM product INNER JOIN factory ON factory.id_Factory = product.fk_Factoryid_Factory
                                INNER JOIN workspace ON factory.id_Factory = workspace.fk_Factoryid_Factory
                                INNER JOIN assignments ON workspace.id_Workspace = assignments.fk_Workspaceid_Workspace
                                WHERE assignments.isCompleted = 1
                                AND factory.id_Factory = ?id";

            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);

            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();

            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            sum = Convert.ToDouble(dt.Rows[0]["sum"]);

            return sum;
        }
    }
}