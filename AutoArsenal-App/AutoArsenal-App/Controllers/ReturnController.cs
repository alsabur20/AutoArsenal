﻿using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class ReturnController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async static Task<List<Return>> GetReturns()
        {
            List<Return> returns = new List<Return>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM [Return]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Return r1 = new Return
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    DateOfReturn = reader.GetDateTime(reader.GetOrdinal("DateOfReturn")),
                                    ReturnType = reader.GetInt32(reader.GetOrdinal("ReturnType")),
                                    AddedBy = reader.GetInt32(reader.GetOrdinal("AddedBy"))
                                };
                                returns.Add(r1);
                            }
                            return await Task.FromResult(returns);
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

        public async static Task<int> AddReturnAndGetId(Return rtn)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO [Return] (DateOfReturn, ReturnType, AddedBy) OUTPUT INSERTED.Id VALUES (@date, @type, @AddedBy)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", rtn.DateOfReturn);
                        command.Parameters.AddWithValue("@type", rtn.ReturnType);
                        command.Parameters.AddWithValue("@AddedBy", rtn.AddedBy);

                        // Execute the command and get the inserted id
                        return (int)await command.ExecuteScalarAsync();
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
