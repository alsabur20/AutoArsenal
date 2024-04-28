using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public static class CustomerController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async static Task<List<Customer>> GetCustomer()
        {
            List<Customer> customer = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Customer";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer1 = new Customer
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    IsTrustworthy = reader.IsDBNull(reader.GetOrdinal("IsTrustworthy")) ? false : reader.GetBoolean(reader.GetOrdinal("IsTrustworthy")),
                                    Discount = reader.IsDBNull(reader.GetOrdinal("Discount")) ? 0.00 : reader.GetDouble(reader.GetOrdinal("Discount")),
                                    Credit = reader.IsDBNull(reader.GetOrdinal("Credit")) ? 0.00 : reader.GetDouble(reader.GetOrdinal("Credit"))
                                };
                                customer.Add(customer1);
                            }
                            return await Task.FromResult(customer);
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
        //add customer to db
        public async static Task AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Customer (Id, IsTrustworthy, Discount, Credit) VALUES (@Id, @IsTrustworthy, @Discount, @Credit)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", customer.ID);
                        command.Parameters.AddWithValue("@IsTrustworthy", customer.IsTrustworthy);
                        command.Parameters.AddWithValue("@Discount", customer.Discount);
                        command.Parameters.AddWithValue("@Credit", customer.Credit);
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
