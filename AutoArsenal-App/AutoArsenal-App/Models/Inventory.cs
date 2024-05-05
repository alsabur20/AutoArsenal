namespace AutoArsenal_App.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        public int? StockInShop { get; set; }
        public int? StockInWarehouse { get; set; }
        public int? WarehouseId { get; set; }
    }
}
