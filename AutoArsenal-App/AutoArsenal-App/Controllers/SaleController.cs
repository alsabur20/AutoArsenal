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
                                    PaymentID = reader.IsDBNull(reader.GetOrdinal("PaymentID")) ? -1 : reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
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
        // Add sale
        public async static Task<int> AddSaleAndGetId(Sale sale)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Sale (EmployeeId, CustomerId, DateOfSale, PaymentID, IsDeleted) OUTPUT INSERTED.Id VALUES (@EmployeeId, @CustomerId, @DateOfSale, @PaymentID, @IsDeleted)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", (object)sale.EmployeeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerId", sale.CustomerID);
                        command.Parameters.AddWithValue("@DateOfSale", sale.DateOfSale);
                        command.Parameters.AddWithValue("@PaymentID", (object)sale.PaymentID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsDeleted", sale.IsDeleted);

                        // Execute the command and get the inserted ID
                        int saleId = (int)await command.ExecuteScalarAsync();
                        return saleId;
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
        // Get sale by ID
        public async static Task<Sale> GetSale(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Sale WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Sale item = new Sale
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                    CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                    DateOfSale = reader.GetDateTime(reader.GetOrdinal("DateOfSale")),
                                    PaymentID = reader.IsDBNull(reader.GetOrdinal("PaymentID")) ? -1 : reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
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
        // Delete sale
        public async static Task DeleteSale(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "Update Sale SET IsDeleted = '1' WHERE Id = @Id";
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
    }
}
