namespace AutoArsenal_App.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public int? EmployeeID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime DateOfSale { get; set; }
        public int? PaymentID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
