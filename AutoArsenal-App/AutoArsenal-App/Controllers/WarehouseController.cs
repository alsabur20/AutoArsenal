﻿using AutoArsenal_App.Models;
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
                    string query = "SELECT * FROM Warehouse";
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
                                    StreetAddress = reader.IsDBNull(reader.GetOrdinal("StreetAddress")) ? null : reader.GetString(reader.GetOrdinal("StreetAddress")),
                                    Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
                                    City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                                    Province = reader.IsDBNull(reader.GetOrdinal("Province")) ? null : reader.GetString(reader.GetOrdinal("Province"))

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
        //add warehouse to db
        public async static Task AddWarehouse(Warehouse warehouse)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Warehouse (Name, StreetAddress, Country, City, Province) VALUES (@Name, @StreetAddress, @Country, @City, @Province)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", warehouse.Name);
                        command.Parameters.AddWithValue("@StreetAddress", warehouse.StreetAddress);
                        command.Parameters.AddWithValue("@Country", warehouse.Country);
                        command.Parameters.AddWithValue("@City", warehouse.City);
                        command.Parameters.AddWithValue("@Province", warehouse.Province);
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
