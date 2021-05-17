using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using DarbasGamykloje.Models;
using MySql.Data.MySqlClient;

namespace DarbasGamykloje.Repos
{
    public class ProductRepository
    {
        public List<Product> GetAllProducts()
        {
            List<Product> Factories = new List<Product>();

            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);

            string sqlQuery = @"SELECT * FROM product";

            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Factories.Add(new Product
                {
                    Name = Convert.ToString(dr["name"]),
                    IsDeleted = Convert.ToInt32(dr["isDeleted"]),
                    Id_Product = Convert.ToInt32(dr["id_Product"]),
                    fk_FactoryId = Convert.ToInt32(dr["fk_Factoryid_Factory"]),
                    Value = Convert.ToDouble(dr["value"])
                });
            }

            return Factories;
        }

        public double GetProductValue(int id_factory)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connStr);

            string sqlQuery = @"SELECT value FROM product WHERE product.fk_Factoryid_Factory = ?id";

            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);

            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id_factory;
            mySqlConnection.Open();

            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            double value = Convert.ToDouble(dt.Rows[0]["value"]);

            return value;
        }
    }
}