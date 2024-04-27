namespace AutoArsenal_App.Models
{
    public class ProductCategory
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int ManufactureId { get; set; }
        public int InventoryId { get; set; }
        public double SalePrice { get; set; }
        public double UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public int Category { get; set; }
    }
}
