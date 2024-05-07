using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AutoArsenal_App.Pages.Products
{
    public class AddProductModel : PageModel
    {
        // For deleting
        [BindProperty]
        public int DeleteID { get; set; }

        [BindProperty]
        public string Product { get; set; }
        [BindProperty]
        public string ProductCategory { get; set; }

        // Showing Data for Drop downs
        [BindProperty]
        public List<Inventory> Inventories { get; set; }

        [BindProperty]
        public List<Manufacturer> Manufacturers { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }


        public async void OnGet()
        {
            try
            {
                Inventories = await InventoryController.GetInventory();
                Manufacturers = await ManufacturerController.GetManufacturers();
                Lookups = await LookupController.GetLookup();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
        // Add product in DB
        public async Task<IActionResult> OnPostAddProduct()
        {
            try
            {
                Product product = JsonConvert.DeserializeObject<Product>(Product);
                List<ProductCategory> productCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(ProductCategory);

                int pId = await ProductController.AddProduct(product);
                foreach (var pc in productCategories)
                {
                    Inventory inventory = new();
                    inventory.StockInShop = 0;
                    inventory.StockInWarehouse = 0;
                    pc.ProductId = pId;
                    pc.InventoryId = await InventoryController.AddInventory(inventory);
                    await ProductCategoryController.AddProductCategory(pc);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message + ex.StackTrace;
            }
            return RedirectToPage("/Products/AllProducts");
        }
    }
}
