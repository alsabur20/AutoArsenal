﻿using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class LookupController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async static Task<List<Lookup>> GetLookup()
        {
            List<Lookup> lookup = new List<Lookup>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Lookup";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Lookup lookup1 = new Lookup
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Category = reader.GetString(reader.GetOrdinal("Category")),
                                    Value = reader.GetString(reader.GetOrdinal("Value")),
                                };
                                lookup.Add(lookup1);
                            }
                            return await Task.FromResult(lookup);
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
        public async static Task<string> GetLookupValue(int id)
        {
            string lookupValue = string.Empty;
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Value FROM Lookup WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lookupValue = reader.GetString(reader.GetOrdinal("Value"));
                            }
                            return await Task.FromResult(lookupValue);
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

        // Getting Id
        public async static Task<int> GetLookupId(string value, string category)
        {
            int Id = 0;
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Id FROM Lookup WHERE Value = @value AND Category = @category";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value", value);
                        command.Parameters.AddWithValue("@Category", category);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            }
                            return await Task.FromResult(Id);
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
