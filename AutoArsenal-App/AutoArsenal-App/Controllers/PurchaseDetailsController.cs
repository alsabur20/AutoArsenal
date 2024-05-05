using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public static class PurchaseDetailsController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<PurchaseDetails>> GetPurchaseDetails()
        {
            List<PurchaseDetails> purchaseDetails = new List<PurchaseDetails>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM PurchaseDetail";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PurchaseDetails item = new PurchaseDetails
                                {
                                    PurchaseID = reader.GetInt32(reader.GetOrdinal("PurchaseId")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    ProductCategoryID = reader.GetInt32(reader.GetOrdinal("ProductCategoryId")),
                                    ManufacturerID = reader.GetInt32(reader.GetOrdinal("ManufacturerId")),
                                    UnitPrice = reader.GetDouble(reader.GetOrdinal("UnitPrice"))

                                };
                                purchaseDetails.Add(item);
                            }
                            return await Task.FromResult(purchaseDetails);
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

        // Add Purchase details
        public async static Task AddPurchaseDetails(List<PurchaseDetails> purchaseDetails)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO PurchaseDetail (PurchaseID, Quantity, ProductCategoryID, UnitPrice, ManufacturerID) VALUES (@purchaseID, @Quantity, @ProductCategoryID, @UnitPrice, @ManufacturerID)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (var prch in purchaseDetails)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@SaleID", prch.PurchaseID);
                            command.Parameters.AddWithValue("@Quantity", prch.Quantity);
                            command.Parameters.AddWithValue("@ProductCategoryID", prch.ProductCategoryID);
                            command.Parameters.AddWithValue("@UnitPrice", prch.UnitPrice);
                            command.Parameters.AddWithValue("@ManufacturerID", prch.ManufacturerID);
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

        // Get Purchase details of a specific Purchase
        public async static Task<List<PurchaseDetails>> GetPurchaseDetailsByID(int pID)
        {
            List<PurchaseDetails> purchaseDetails = new List<PurchaseDetails>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM PurchaseDetail WHERE PurchaseId = @PurchaseID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseID", pID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PurchaseDetails item = new PurchaseDetails
                                {
                                    PurchaseID = reader.GetInt32(reader.GetOrdinal("PurchaseId")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    ProductCategoryID = reader.GetInt32(reader.GetOrdinal("ProductCategoryId")),
                                    ManufacturerID = reader.GetInt32(reader.GetOrdinal("ManufacturerId")),
                                    UnitPrice = reader.GetFloat(reader.GetOrdinal("UnitPrice"))
                                };
                                purchaseDetails.Add(item);
                            }
                            return await Task.FromResult(purchaseDetails);
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
