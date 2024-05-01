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
    }
}
