namespace AutoArsenal_App.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int PaymentID { get; set; }
        public int AddedBy { get; set; }
    }
}
