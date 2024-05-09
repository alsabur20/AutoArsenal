using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class PaymentController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Payment>> GetPayments()
        {
            List<Payment> payments = new List<Payment>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Payment";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Payment item = new Payment
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    TotalAmount = reader.GetDouble(reader.GetOrdinal("Amount"))
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

        public async static Task<int> AddPaymentAndGetID(double amount)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Payment (Amount) OUTPUT INSERTED.Id VALUES (@amount)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@amount", amount);

                        // Execute the command and get the inserted id
                        return (int)await command.ExecuteScalarAsync();
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
