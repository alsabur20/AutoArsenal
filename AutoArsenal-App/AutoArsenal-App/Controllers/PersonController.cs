using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class PersonController
    {
        private static IConfiguration? Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async static Task<List<Person>> GetPerson()
        {
            List<Person> person = new List<Person>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Person";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Person person1 = new Person
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Contact = reader.GetString(reader.GetOrdinal("Contact")),
                                    Gender = reader.GetInt32(reader.GetOrdinal("Gender")),
                                    Status = reader.GetInt32(reader.GetOrdinal("Status")),
                                    //handle null for location
                                    LocationId = reader.IsDBNull(reader.GetOrdinal("LocationId")) ? -1 : reader.GetInt32(reader.GetOrdinal("LocationId"))
                                };
                                person.Add(person1);
                            }
                            return await Task.FromResult(person);
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
        //add person to db
        public async static Task AddPerson(Person person)
        {
            //add person to db
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Person (FirstName, LastName, Contact, Gender, Status, LocationId) VALUES (@FirstName, @LastName, @Contact, @Gender, @Status, @LocationId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@Contact", person.Contact);
                        command.Parameters.AddWithValue("@Gender", person.Gender);
                        command.Parameters.AddWithValue("@Status", 8);
                        command.Parameters.AddWithValue("@LocationId", person.LocationId);
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
        //get person id
        public async static Task<int> GetPersonId(Person person)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Id FROM Person WHERE FirstName = @FirstName AND LastName = @LastName AND Contact = @Contact AND Gender = @Gender AND Status = @Status";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@Contact", person.Contact);
                        command.Parameters.AddWithValue("@Gender", person.Gender);
                        command.Parameters.AddWithValue("@Status", 8);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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
