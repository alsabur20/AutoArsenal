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
        public async static Task<List<Customer>> GetCustomers()
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
        public async static Task AddCustomer(Person person, Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC sp_AddCustomer @FirstName, @LastName, @Contact, @Gender, @StreetAddress, @Country, @City, @Province, @IsTrustworthy, @Credit";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@Contact", person.Contact);
                        command.Parameters.AddWithValue("@Gender", person.Gender);
                        command.Parameters.AddWithValue("@StreetAddress", person.StreetAddress);
                        command.Parameters.AddWithValue("@Country", person.Country);
                        command.Parameters.AddWithValue("@City", person.City);
                        command.Parameters.AddWithValue("@Province", person.Province);
                        command.Parameters.AddWithValue("@IsTrustworthy", customer.IsTrustworthy);
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
        // ount customers
        public static int GetCustomerCount()
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Customer";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return (int)command.ExecuteScalar();
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
