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
                Cities = new List<string> {
                "Karachi",
                "Lahore",
                "Layyah",
                "Islamabad",
                "Faisalabad",
                "Rawalpindi",
                "Multan",
                "Gujranwala",
                "Peshawar",
                "Quetta",
                "Sialkot",
                "Hyderabad",
                "Bahawalpur",
                "Sargodha",
                "Abbottabad",
                "Sukkur",
                "NawabShah",
                "Mingora",
                "Mirpur Khas",
                "Sheikhupura",
                "Jhang"};

                Provinces = new List<string>{
                "Punjab",
                "Sindh",
                "Khyber Pakhtunkhwa",
                "Balochistan",
                "Gilgit-Baltistan" };

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
                    Inventory inventory = new Inventory();
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

        // For Adding Manufacturer
        [BindProperty]
        public Manufacturer manufacturer { get; set; }
        [BindProperty]
        public List<string> Cities { get; set; }
        [BindProperty]
        public List<string> Provinces { get; set; }
        public async Task<IActionResult> OnPostAddManufacturer()
        {
            try
            {
                await ManufacturerController.AddManufacturer(manufacturer);

                TempData["Success"] = "Manufacturer added successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return Page();
        }
    }
}
