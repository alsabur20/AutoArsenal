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
                    string query = "SELECT * FROM Employee";
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
        public async static Task AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Employee (Id, JoiningDate, Role, Salary) VALUES (@Id, @JoiningDate, @Role, @Salary)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employee.ID);
                        command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                        command.Parameters.AddWithValue("@Role", employee.Role);
                        //command.Parameters.AddWithValue("@CredentialsId", employee.CredentialsId);
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
        //edit empployee
        public async static Task UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC stp_UpdateEmployee @Id, @JoiningDate, @Role, @Salary";
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
    }
}
