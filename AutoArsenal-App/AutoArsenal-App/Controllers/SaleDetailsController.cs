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
    }
}
