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
                                    StockInShop = reader.IsDBNull(reader.GetOrdinal("StockInShop")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("StockInShop")),
                                    StockInWarehouse = reader.IsDBNull(reader.GetOrdinal("StockInWarehouse")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("StockInWarehouse")),
                                    WarehouseId = reader.IsDBNull(reader.GetOrdinal("WarehouseId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("WarehouseId"))
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

        //add and get id
        public async static Task<int> AddInventory(Inventory inventory)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.OpenAsync();
                    string query = "INSERT INTO Inventory (StockInShop, StockInWarehouse, WarehouseId) OUTPUT INSERTED.Id VALUES(@StockInShop, @StockInWarehouse, @WarehouseId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StockInShop", DBNull.Value);
                        command.Parameters.AddWithValue("@StockInWarehouse", DBNull.Value);
                        command.Parameters.AddWithValue("@WarehouseId", DBNull.Value);
                        return await Task.FromResult(Convert.ToInt32(command.ExecuteScalar()));
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
