using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public static class SaleController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Sale>> GetSales()
        {
            List<Sale> sales = new List<Sale>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Sale";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Sale item = new Sale
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                    CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                    DateOfSale = reader.GetDateTime(reader.GetOrdinal("DateOfSale")),
                                    PaymentID = reader.IsDBNull(reader.GetOrdinal("PaymentID")) ? -1 : reader.GetInt32(reader.GetOrdinal("PaymentID"))
                                };
                                sales.Add(item);
                            }
                            return await Task.FromResult(sales);
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
