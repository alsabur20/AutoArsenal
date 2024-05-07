using AutoArsenal_App.Models;
using System;
using System.Data;
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
                                    StockInShop = reader.IsDBNull(reader.GetOrdinal("StockInShop")) ? -1 : reader.GetInt32(reader.GetOrdinal("StockInShop")),
                                    StockInWarehouse = reader.IsDBNull(reader.GetOrdinal("StockInWarehouse")) ? -1 : reader.GetInt32(reader.GetOrdinal("StockInWarehouse")),
                                    WarehouseId = reader.IsDBNull(reader.GetOrdinal("WarehouseId")) ? -1 : reader.GetInt32(reader.GetOrdinal("WarehouseId"))
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
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("sp_AddInventory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Set parameter values from the provided inventory object
                        command.Parameters.AddWithValue("@StockInShop", inventory.StockInShop);
                        command.Parameters.AddWithValue("@StockInWarehouse", inventory.StockInWarehouse);
                        command.Parameters.AddWithValue("@WarehouseId", inventory.WarehouseId);

                        // Declare an OUTPUT parameter to capture the inserted ID
                        SqlParameter outputParam = new SqlParameter("@Id", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        await command.ExecuteNonQueryAsync();

                        // Retrieve the inserted ID from the OUTPUT parameter
                        return Convert.ToInt32(outputParam.Value);
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

        // Updating Quantity
        public async static Task UpdateInventory(int id, int Quantity)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE Inventory SET StockInShop = StockInShop  + @Quantity WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
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

        // Removing Quantity
        public async static Task RemovingItemsFromInventory(int id, int Quantity)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE Inventory 
                                    SET StockInShop = CASE WHEN StockInShop >= @Quantity THEN StockInShop - @Quantity ELSE 0 END,
                                        StockInWarehouse = CASE WHEN StockInShop < @Quantity AND StockInWarehouse >= @Quantity - StockInShop THEN StockInWarehouse - (@Quantity - StockInShop) ELSE StockInWarehouse END
                                    WHERE Id = @Id; ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
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
