using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public static class SaleDetailsController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<SaleDetails>> GetSaleDetails()
        {
            List<SaleDetails> saleDetails = new List<SaleDetails>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM SaleDetails";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SaleDetails item = new SaleDetails
                                {
                                    SaleID = reader.GetInt32(reader.GetOrdinal("SaleID")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    ProductCategoryID = reader.GetInt32(reader.GetOrdinal("ProductCategoryID"))
                                };
                                saleDetails.Add(item);
                            }
                            return await Task.FromResult(saleDetails);
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
        // Add sale details
        public async static Task AddSaleDetails(List<SaleDetails> saleDetails)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO SaleDetails (SaleID, Quantity, ProductCategoryID) VALUES (@SaleID, @Quantity, @ProductCategoryID)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (var sd in saleDetails)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@SaleID", sd.SaleID);
                            command.Parameters.AddWithValue("@Quantity", sd.Quantity);
                            command.Parameters.AddWithValue("@ProductCategoryID", sd.ProductCategoryID);
                            await command.ExecuteNonQueryAsync();
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

        // get sale details of a specific sale
        public async static Task<List<SaleDetails>> GetSaleDetails(int saleID)
        {
            List<SaleDetails> saleDetails = new List<SaleDetails>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM SaleDetails WHERE SaleID = @SaleID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SaleID", saleID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SaleDetails item = new SaleDetails
                                {
                                    SaleID = reader.GetInt32(reader.GetOrdinal("SaleID")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    ProductCategoryID = reader.GetInt32(reader.GetOrdinal("ProductCategoryID"))
                                };
                                saleDetails.Add(item);
                            }
                            return await Task.FromResult(saleDetails);
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
