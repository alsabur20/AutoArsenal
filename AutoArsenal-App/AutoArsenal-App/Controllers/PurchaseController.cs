using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public static class PurchaseController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Purchase>> GetPurchases()
        {
            List<Purchase> purchases = new List<Purchase>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Purchase";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Purchase item = new Purchase
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    DateOfPurchase = reader.GetDateTime(reader.GetOrdinal("DateOfPurchase")),
                                    PaymentID = reader.IsDBNull(reader.GetOrdinal("PaymentID")) ? -1 : reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                    AddedBy = reader.IsDBNull(reader.GetOrdinal("AddedBy")) ? -1 : reader.GetInt32(reader.GetOrdinal("AddedBy"))
                                };
                                purchases.Add(item);
                            }
                            return await Task.FromResult(purchases);
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

        // Add Purchase
        public async static Task<int> AddPurchaseAndGetId(Purchase purchase)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Purchase (DateOfPurchase, PaymentID, AddedBy) OUTPUT INSERTED.Id VALUES (@DateOfPurchase, @PaymentID, @AddedBy)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DateOfPurchase", purchase.DateOfPurchase);
                        command.Parameters.AddWithValue("@PaymentID", (object)purchase.PaymentID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AddedBy", (object)purchase.AddedBy ?? DBNull.Value);

                        // Execute the command and get the inserted ID
                        int purchaseId = (int)await command.ExecuteScalarAsync();
                        return purchaseId;
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

        // Get Purchase by ID
        public async static Task<Purchase> GetPurchase(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Purchase WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Purchase item = new Purchase
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    DateOfPurchase = reader.GetDateTime(reader.GetOrdinal("DateOfPurchase")),
                                    PaymentID = reader.IsDBNull(reader.GetOrdinal("PaymentID")) ? -1 : reader.GetInt32(reader.GetOrdinal("PaymentID"))
                                };
                                return await Task.FromResult(item);
                            }
                            else
                            {
                                return null;
                            }
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
