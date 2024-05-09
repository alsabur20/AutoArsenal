using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class PaymentDetailsController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<PaymentDetails>> GetPaymentDetails()
        {
            List<PaymentDetails> payments = new List<PaymentDetails>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM PaymentDetails";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PaymentDetails item = new PaymentDetails
                                {
                                    PaymentID = reader.GetInt32(reader.GetOrdinal("PaymentId")),
                                    PaidAmount = reader.GetDouble(reader.GetOrdinal("Amount")),
                                    PaymentMethod = reader.GetInt32(reader.GetOrdinal("PaymentMethod")),
                                    PaymentAccount = reader.IsDBNull(reader.GetOrdinal("PaymentAccount")) ? (string)null : reader.GetString(reader.GetOrdinal("PaymentAccount")),
                                    PaymentType = reader.GetInt32(reader.GetOrdinal("PaymentType")),
                                    DateOfPayment = reader.GetDateTime(reader.GetOrdinal("DateOfPayment"))
                                };
                                payments.Add(item);
                            }
                            return await Task.FromResult(payments);
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

        // Get Remaining Amount
        public async static Task<double> GetRemaining(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT SUM(Amount) FROM PaymentDetails WHERE PaymentId = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        return await Task.FromResult(Convert.ToDouble(command.ExecuteScalar()));
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

        // Adding payment details
        public async static Task AddPaymentDetails(PaymentDetails pay)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO PaymentDetails (PaymentId, Amount, PaymentMethod, PaymentAccount, PaymentType, DateOfPayment) 
                                    VALUES (@id, @Amount, @PaymentMethod, @PaymentAccount, @PaymentType, @DateOfPayment)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", pay.PaymentID);
                        command.Parameters.AddWithValue("@Amount", pay.PaidAmount);
                        command.Parameters.AddWithValue("@PaymentMethod", pay.PaymentMethod);
                        command.Parameters.AddWithValue("@PaymentAccount", pay.PaymentAccount);
                        command.Parameters.AddWithValue("@PaymentType", pay.PaymentType);
                        command.Parameters.AddWithValue("@DateOfPayment", pay.DateOfPayment);
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
