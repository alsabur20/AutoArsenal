using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class WarehouseController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Warehouse>> GetWarehouses()
        {
            List<Warehouse> warehouses = new List<Warehouse>();
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
                                Warehouse item = new Warehouse
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    //handle null for location
                                    LocationId = reader.IsDBNull(reader.GetOrdinal("LocationId")) ? -1 : reader.GetInt32(reader.GetOrdinal("LocationId"))
                                };
                                warehouses.Add(item);
                            }
                            return await Task.FromResult(warehouses);
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
