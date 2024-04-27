namespace AutoArsenal_App.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? Role { get; set; }
        public int? CredentialsId { get; set; }
        public double? Salary { get; set; }
    }
}
