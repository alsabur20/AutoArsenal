namespace AutoArsenal_App.Models
{
    public class PurchaseDetails
    {
        public int PurchaseID { get; set; }
        public int Quantity { get; set; }
        public int ProductCategoryID { get; set; }
        public int ManufacturerID { get; set; }
        public double UnitPrice { get; set; }
    }
}
