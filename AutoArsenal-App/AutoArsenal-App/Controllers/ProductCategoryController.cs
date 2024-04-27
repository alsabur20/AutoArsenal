using AutoArsenal_App.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AutoArsenal_App.Controllers
{
    public class ProductCategoryController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<ProductCategory>> GetProducts()
        {
            List<ProductCategory> products = new List<ProductCategory>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT * FROM ProductCategory";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductCategory item = new ProductCategory
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    ManufactureId = reader.GetInt32(reader.GetOrdinal("ManufacturerId")),
                                    InventoryId = reader.GetInt32(reader.GetOrdinal("InventoryId")),
                                    UnitPrice = reader.GetDouble(reader.GetOrdinal("UnitPrice")),
                                    SalePrice = reader.GetDouble(reader.GetOrdinal("SalePrice")),
                                    ImagePath = reader.GetString(reader.GetOrdinal("Image")),
                                    Category = reader.GetInt32(reader.GetOrdinal("Category")),
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
