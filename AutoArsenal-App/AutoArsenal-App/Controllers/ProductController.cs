using AutoArsenal_App.Models;
using System.Data.SqlClient;
using System.Linq;

namespace AutoArsenal_App.Controllers
{
    public class ProductController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Product>> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT * FROM Product";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product item = new Product
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    ProductName = reader.GetString(reader.GetOrdinal("Name")),
                                    ProductDescription = reader.GetString(reader.GetOrdinal("Description")),
                                };
                                products.Add(item);
                            }
                            return await Task.FromResult(products);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + ex.StackTrace);
                }
                finally 
                {
                    connection.Close();
                }
            }
        }
        //add product and get id
        public async static Task<int> AddProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Product (Name, Description) OUTPUT INSERTED.Id VALUES (@Name, @Description);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.ProductName);
                        command.Parameters.AddWithValue("@Description", product.ProductDescription);
                        return await Task.FromResult(Convert.ToInt32(command.ExecuteScalar()));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + ex.StackTrace);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        //update product
        public async static Task UpdateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Product SET Name = @Name, Description = @Description WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", product.ID);
                        command.Parameters.AddWithValue("@Name", product.ProductName);
                        command.Parameters.AddWithValue("@Description", product.ProductDescription);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + ex.StackTrace);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
