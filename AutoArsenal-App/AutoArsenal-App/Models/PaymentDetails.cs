namespace AutoArsenal_App.Models
{
    public class PaymentDetails
    {
        public int PaymentID { get; set; }
        public float PaidAmount { get; set; }
        public int PaymentMethod {  get; set; }
        public string PaymentAccount {  get; set; }
        public int PaymentType { get; set; }
        public DateTime DateOfPayment { get; set; }
    }
}
