using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using TestTaskProducts.Models;

namespace TestTaskProducts.Services
{
    public class DBService
    {
        readonly SqlCeConnection _connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        public DBService()
        {
            _connection.Open();
        }

        public void CreateProductsTable()
        {
            string query = $@"CREATE TABLE Products (ID uniqueidentifier NOT NULL, Denomination nvarchar(1000) NOT NULL, Price int NOT NULL, Quantity int NOT NULL, PRIMARY KEY (ID));";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public void DropProductsTable()
        {
            string query = $@"DROP TABLE Products;";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public void AddProduct(Product product)
        {
            string query = $@"INSERT INTO Products VALUES ('{product.Id}', '{product.Denomination}', '{product.Price}', '{product.Quantity}');";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteProduct(Guid id)
        {
            string query = $@"DELETE FROM Products WHERE ID = '{id}';";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateProduct(Guid id, string denomination, int price, int quantity)
        {
            string query = @"UPDATE Products SET ";

            if (denomination != null)
            {
                if (price == 0 && quantity == 0)
                    query += $@"Denomination = '{denomination}' ";
                else
                    query += $@"Denomination = '{denomination}', ";
            }

            if (price != 0)
            {
                if (quantity == 0)
                    query += $@"Price = {price} ";
                else
                    query += $@"Price = {price}, ";
            }

            if (quantity != 0)
            {
                query += $@"Quantity = {quantity} ";
            }

            query += $"WHERE ID = '{id}';";

            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public Product GetProduct(Guid id)
        {
            var query = $@"SELECT * FROM Products WHERE ID = '{id}';";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            SqlCeDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var product = new Product()
                {
                    Id = reader.GetGuid(0),
                    Denomination = reader.GetString(1),
                    Price = reader.GetInt32(2),
                    Quantity = reader.GetInt32(3)
                };
                return product;
            }
            return null;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            var query = $@"SELECT * FROM Products;";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            SqlCeDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product() { Id = reader.GetGuid(0), Denomination = reader.GetString(1), Price = reader.GetInt32(2), Quantity = reader.GetInt32(3) });
            }
            return products;
        }

        public void DeleteAllProducts()
        {
            string query = $@"DELETE FROM Products;";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }
    }
}