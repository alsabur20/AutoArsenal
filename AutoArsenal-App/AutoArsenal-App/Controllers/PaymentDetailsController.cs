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
                                    PaymentID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    PaidAmount = reader.GetFloat(reader.GetOrdinal("Amount")),
                                    PaymentMethod = reader.GetInt32(reader.GetOrdinal("PaymentMethod")),
                                    PaymentAccount = reader.GetString(reader.GetOrdinal("PaymentAccount")),
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

    }
}
