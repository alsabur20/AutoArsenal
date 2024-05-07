using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class ReturnDetailsController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async static Task<List<ReturnDetails>> GetReturns()
        {
            List<ReturnDetails> returnDetails = new List<ReturnDetails>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM ReturnDetails";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnDetails r1 = new ReturnDetails
                                {
                                    ReturnID = reader.GetInt32(reader.GetOrdinal("ReturnId")),
                                    ProductCategoryID = reader.GetInt32(reader.GetOrdinal("ProductCategory")),
                                    ReturnQuantity = reader.GetInt32(reader.GetOrdinal("ReturnQuantity")),
                                };
                                returnDetails.Add(r1);
                            }
                            return await Task.FromResult(returnDetails);
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

        // Adding ReturnDetails
        public async static Task AddReturnDetails(ReturnDetails returnDetails)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    // Ye change krna h db change krne k baad

                    connection.Open();
                    string query = "INSERT INTO ReturnDetails (ReturnID, ProductCategory, ReturnQuantity, ReturnPrice) VALUES (@ReturnID, @ProductCategoryID, @ReturnQuantity, 0)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnID", returnDetails.ReturnID);
                        command.Parameters.AddWithValue("@ProductCategoryID", returnDetails.ProductCategoryID);
                        command.Parameters.AddWithValue("@ReturnQuantity", returnDetails.ReturnQuantity);
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
