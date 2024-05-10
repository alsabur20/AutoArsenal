using AutoArsenal_App.Models;
using System.Data.SqlClient;

namespace AutoArsenal_App.Controllers
{
    public class EmployeeController
    {
        private static IConfiguration Configuration { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async static Task<List<Employee>> GetEmployee()
        {
            List<Employee> employee = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM View_Employees";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee1 = new Employee
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                                    Role = reader.GetInt32(reader.GetOrdinal("Role")),
                                    CredentialsId = reader.IsDBNull(reader.GetOrdinal("CredentialsId")) ? -1 : reader.GetInt32(reader.GetOrdinal("CredentialsId")),
                                    Salary = reader.GetDouble(reader.GetOrdinal("Salary")),
                                };
                                employee.Add(employee1);
                            }
                            return await Task.FromResult(employee);
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
        //add employee to db
        public async static Task AddEmployee(Person person, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC sp_AddEmployee @FirstName, @LastName, @Contact, @Gender, @JoiningDate, @Role, @Salary, @StreetAddress, @Country, @City, @Province";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@Contact", person.Contact);
                        command.Parameters.AddWithValue("@Gender", person.Gender);
                        command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                        command.Parameters.AddWithValue("@Role", employee.Role);
                        command.Parameters.AddWithValue("@Salary", employee.Salary);
                        command.Parameters.AddWithValue("@StreetAddress", person.StreetAddress);
                        command.Parameters.AddWithValue("@Country", person.Country);
                        command.Parameters.AddWithValue("@City", person.City);
                        command.Parameters.AddWithValue("@Province", person.Province);
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
        //edit empployee
        public async static Task UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC sp_UpdateEmployee @Id, @JoiningDate, @Role, @Salary";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employee.ID);
                        command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                        command.Parameters.AddWithValue("@Role", employee.Role);
                        command.Parameters.AddWithValue("@Salary", employee.Salary);
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
        public async static Task<Employee> GetEmployeeByCredential(int credentialId)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Employee WHERE CredentialsId = @CredentialsId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CredentialsId", credentialId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employee = new Employee
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("Id")),
                                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                                    Role = reader.GetInt32(reader.GetOrdinal("Role")),
                                    CredentialsId = reader.IsDBNull(reader.GetOrdinal("CredentialsId")) ? -1 : reader.GetInt32(reader.GetOrdinal("CredentialsId")),
                                    Salary = reader.GetDouble(reader.GetOrdinal("Salary")),
                                };
                            }
                            return await Task.FromResult(employee);
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
        // count of employees
        public static int GetEmployeeCount()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Employee";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        count = (int) command.ExecuteScalar();
                        return count;
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
