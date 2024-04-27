using AutoArsenal_App.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace AutoArsenal_App.Controllers
{
    public class InventoryController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async static Task<List<Inventory>> GetInventory()
        {
            List<Inventory> inventory = new List<Inventory>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Inventory";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Inventory item = new Inventory
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    StockInShop = reader.GetInt32(reader.GetOrdinal("StockInShop")),
                                    StockInWarehouse = reader.GetInt32(reader.GetOrdinal("StockInWarehouse")),
                                    WarehouseId = reader.GetInt32(reader.GetOrdinal("WarehouseId")),
                                };
                                inventory.Add(item);
                            }
                            return await Task.FromResult(inventory);
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
