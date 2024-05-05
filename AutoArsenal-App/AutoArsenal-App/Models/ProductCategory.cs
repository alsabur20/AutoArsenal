namespace AutoArsenal_App.Models
{
    public class ProductCategory
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int ManufacturerId { get; set; }
        public int InventoryId { get; set; }
        public double SalePrice { get; set; }
        public string Image { get; set; }
        public int Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
