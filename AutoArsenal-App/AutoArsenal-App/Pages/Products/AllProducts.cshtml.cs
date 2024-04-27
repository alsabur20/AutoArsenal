using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Products
{
    public class ProductsModel : PageModel
    {


        // For Viewing
        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Lookup> Lookups { get; set; }
        public List<Inventory> Inventories { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }

        public async void OnGet()
        {
            try
            {
                Lookups = await LookupController.GetLookup();
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategory();
                Inventories = await InventoryController.GetInventory();
                Manufacturers = await ManufacturerController.GetManufacturers();

            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
