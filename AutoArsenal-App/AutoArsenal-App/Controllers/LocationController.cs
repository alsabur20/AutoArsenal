using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class LocationController
    {
        private static IConfiguration Configuration { get; set; }
        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Location>> GetLocation()
        {
            List<Location> location = new List<Location>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Location";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Location location1 = new Location
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    //handle null of street address, country, city, province
                                    StreetAddress = reader.IsDBNull(reader.GetOrdinal("StreetAddress")) ? "" : reader.GetString(reader.GetOrdinal("StreetAddress")),
                                    Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? "" : reader.GetString(reader.GetOrdinal("Country")),
                                    City = reader.IsDBNull(reader.GetOrdinal("City")) ? "" : reader.GetString(reader.GetOrdinal("City")),
                                    Province = reader.IsDBNull(reader.GetOrdinal("Province")) ? "" : reader.GetString(reader.GetOrdinal("Province"))
                                };
                                location.Add(location1);
                            }
                            return await Task.FromResult(location);
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
        public async static Task AddLocation(Location location)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Location (StreetAddress, Country, City, Province) VALUES (@StreetAddress, @Country, @City, @Province)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StreetAddress", location.StreetAddress);
                        command.Parameters.AddWithValue("@Country", location.Country);
                        command.Parameters.AddWithValue("@City", location.City);
                        command.Parameters.AddWithValue("@Province", location.Province);
                        await Task.FromResult(command.ExecuteNonQuery());
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
        public async static Task<int> GetLocationId(Location location)
        {
            //get location id
            using(SqlConnection connectiono = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connectiono.Open();
                    string query = "SELECT Id FROM Location WHERE StreetAddress = @StreetAddress AND Country = @Country AND City = @City AND Province = @Province";
                    using(SqlCommand command = new SqlCommand(query, connectiono))
                    {
                        command.Parameters.AddWithValue("@StreetAddress", location.StreetAddress);
                        command.Parameters.AddWithValue("@Country", location.Country);
                        command.Parameters.AddWithValue("@City", location.City);
                        command.Parameters.AddWithValue("@Province", location.Province);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                return await Task.FromResult(reader.GetInt32(reader.GetOrdinal("Id")));
                            }
                            else
                            {
                                return await Task.FromResult(-1);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message + ex.StackTrace);
                }
                finally
                {
                    connectiono.Close();
                }
            }
        }
        //edit location
        public async static Task UpdateLocation(Location location)
        {
            using(SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC stp_UpdateLocation @Id, @StreetAddress, @Country, @City, @Province";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", location.ID);
                        command.Parameters.AddWithValue("@StreetAddress", location.StreetAddress);
                        command.Parameters.AddWithValue("@Country", location.Country);
                        command.Parameters.AddWithValue("@City", location.City);
                        command.Parameters.AddWithValue("@Province", location.Province);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch(Exception ex)
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
