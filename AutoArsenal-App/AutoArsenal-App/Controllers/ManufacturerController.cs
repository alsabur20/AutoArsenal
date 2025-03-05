using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class ManufacturerController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Manufacturer>> GetManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Manufacturer";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Manufacturer item = new Manufacturer
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Contact = reader.GetString(reader.GetOrdinal("Contact")),
                                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
                                    //handle null for location
                                    StreetAddress = reader.IsDBNull(reader.GetOrdinal("StreetAddress")) ? null : reader.GetString(reader.GetOrdinal("StreetAddress")),
                                    Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
                                    City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                                    Province = reader.IsDBNull(reader.GetOrdinal("Province")) ? null : reader.GetString(reader.GetOrdinal("Province")),
                                };
                                manufacturers.Add(item);
                            }
                            return await Task.FromResult(manufacturers);
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

        // Delete Manufacturer
        public async static Task DeleteManufacturer(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "Update Manufacturer SET IsDeleted = 1 WHERE Id = @Id";
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

        // Update Manufacturer
        public async static Task UpdateManufacturer(Manufacturer manu)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "Update Manufacturer SET Contact = @Contact, StreetAddress = @Street, Country = @Country, City = @City, Province = @Province WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", manu.ID);
                        command.Parameters.AddWithValue("@Contact", manu.Contact);
                        command.Parameters.AddWithValue("@Street", manu.StreetAddress);
                        command.Parameters.AddWithValue("@Country", manu.Country);
                        command.Parameters.AddWithValue("@City", manu.City);
                        command.Parameters.AddWithValue("@Province", manu.Province);
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

        // Add Manufacturer
        public async static Task AddManufacturer(Manufacturer manu)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Manufacturer (Name, Contact, StreetAddress, Country, City, Province, IsDeleted) VALUES (@Name, @Contact, @Street, @Country, @City, @Province, 0)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", manu.Name);
                        command.Parameters.AddWithValue("@Contact", manu.Contact);
                        command.Parameters.AddWithValue("@Street", manu.StreetAddress);
                        command.Parameters.AddWithValue("@Country", manu.Country);
                        command.Parameters.AddWithValue("@City", manu.City);
                        command.Parameters.AddWithValue("@Province", manu.Province);
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
