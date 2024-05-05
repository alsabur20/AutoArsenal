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

        public async static Task<List<ProductCategory>> GetProductCategories()
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
                                    ManufacturerId = reader.GetInt32(reader.GetOrdinal("ManufacturerId")),
                                    InventoryId = reader.GetInt32(reader.GetOrdinal("InventoryId")),
                                    SalePrice = reader.GetDouble(reader.GetOrdinal("SalePrice")),
                                    Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    Category = reader.GetInt32(reader.GetOrdinal("Category")),
                                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
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
        //add productcategory
        public async static Task AddProductCategory(ProductCategory productCategory)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO ProductCategory(ProductId, ManufacturerId, InventoryId, SalePrice, Image, Category, IsDeleted) VALUES(@ProductId, @ManufacturerId, @InventoryId, @SalePrice, @Image, @Category, @IsDeleted)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productCategory.ProductId);
                        command.Parameters.AddWithValue("@ManufacturerId", productCategory.ManufacturerId);
                        command.Parameters.AddWithValue("@InventoryId", productCategory.InventoryId);
                        command.Parameters.AddWithValue("@SalePrice", productCategory.SalePrice);
                        if (string.IsNullOrEmpty(productCategory.Image))
                        {
                            command.Parameters.AddWithValue("@Image", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Image", productCategory.Image);
                        }
                        command.Parameters.AddWithValue("@Category", productCategory.Category);
                        command.Parameters.AddWithValue("@IsDeleted", productCategory.IsDeleted);
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
        // delete productcategory
        public async static Task DeleteProductCategory(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE ProductCategory SET IsDeleted = 1 WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
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
        //update productcategory
        public async static Task UpdateProductCategory(ProductCategory productCategory)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE ProductCategory SET ManufacturerId = @ManufacturerId, SalePrice = @SalePrice, Image = @Image WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", productCategory.ID);
                        command.Parameters.AddWithValue("@ManufacturerId", productCategory.ManufacturerId);
                        command.Parameters.AddWithValue("@SalePrice", productCategory.SalePrice);
                        if (string.IsNullOrEmpty(productCategory.Image))
                        {
                            command.Parameters.AddWithValue("@Image", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Image", productCategory.Image);
                        }
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
