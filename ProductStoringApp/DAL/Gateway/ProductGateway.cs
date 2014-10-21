using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductStoringApp.DAL.DAO;

namespace ProductStoringApp.DAL.Gateway
{
    class ProductGateway
    {
        private SqlConnection connection;

        public ProductGateway()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlCon"].ConnectionString);
        }

        public string Save(Product aProduct)
        {
            connection.Open();
            string query = string.Format("INSERT INTO t_Product VALUES('{0}','{1}',{2})",aProduct.Code,aProduct.Name,aProduct.Quantity);
            SqlCommand command = new SqlCommand(query, connection);
            int affectedRow = command.ExecuteNonQuery();
            connection.Close();
            if (affectedRow > 0)
            {
                return "Product has been save sucessfull";
            }
            return "Product not save for some problem";
        }

        public List<Product> GetAllProduct(Product aProduct)
        {
            connection.Open();
            string query = string.Format("SELECT * FROM t_Product");
            SqlCommand command = new SqlCommand(query, connection);
            List<Product> products = new List<Product>();
            SqlDataReader aReader = command.ExecuteReader();
            bool HasHow = aReader.HasRows;

            if (HasHow)
            {
                while (aReader.Read())
                {
                    Product product = new Product();
                    product.Id = (int)aReader[0];
                    product.Code = aReader[1].ToString();
                    product.Name = aReader[2].ToString();
                    product.Quantity = (int)aReader[3];
                    products.Add(product);
                }
            }
            connection.Close();
            return products;
        }

        public bool HasThisProductNameStored(string name)
        {
            connection.Open();
            string query = string.Format("SELECT Name FROM t_Product WHERE Name='{0}'", name);
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader aReader = command.ExecuteReader();
            bool HasHow = aReader.HasRows;
            connection.Close();
            return HasHow;
        }

        public bool HasThisProductCodeStored(string code)
        {
            connection.Open();
            string query = string.Format("SELECT Code FROM t_Product WHERE Code='{0}'", code);
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader aReader = command.ExecuteReader();
            bool HasHow = aReader.HasRows;
            connection.Close();
            return HasHow;
        }

        public int GetTotalQuantity()
        {
            connection.Open();
            string query = string.Format("SELECT SUM(Quantity) AS TotalQuantity FROM t_Product");
            SqlCommand command = new SqlCommand(query, connection);
            object totalQuantity = command.ExecuteScalar();
            connection.Close();
            return (int) totalQuantity;
        }
    }
}
