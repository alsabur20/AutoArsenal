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

        
        
        // Showing Data for Drop downs
        [BindProperty]
        public List<Inventory> Inventories { get; set; }

        [BindProperty]
        public List<Manufacturer> Manufacturers { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }


        public async Task OnGetAsync()
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
        public async Task<IActionResult> OnPostAddProductInDB()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string json = await reader.ReadToEndAsync();
                string product = JsonConvert.DeserializeObject<string>(json);

                await Console.Out.WriteLineAsync(product);
            }

            return RedirectToPage("/Products/AllProducts");
        }
    }
}
