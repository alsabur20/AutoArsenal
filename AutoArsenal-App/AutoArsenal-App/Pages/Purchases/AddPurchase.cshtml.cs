using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AutoArsenal_App.Pages.Purchases
{
    public class AddPurchaseModel : PageModel
    {
        [BindProperty]
        public string Purchases { get; set; }
        
        [BindProperty]
        public string PurchaseDetails { get; set; }

        [BindProperty]
        public string ManufacturerName { get; set; }
        
        [BindProperty]
        public List<Product> Products { get; set; }
        
        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }
        
        [BindProperty]
        public List<Lookup> Lookups { get; set; }
        
        [BindProperty]
        public List<Person> Persons { get; set; }
        
        [BindProperty]
        public List<Warehouse> Warehouses { get; set; }

        [BindProperty]
        public List<Manufacturer> Manufacturers { get; set; }
        
        public async void OnGet()
        {
            try
            {
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Lookups = await LookupController.GetLookup();
                Persons = await PersonController.GetPersons();
                Warehouses = await WarehouseController.GetWarehouses();
                Manufacturers = await ManufacturerController.GetManufacturers();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
        
        
        public async Task<IActionResult> OnPostSavePurchases()
        {
            try
            {
                Purchase purchase = JsonConvert.DeserializeObject<Purchase>(Purchases);
                List<PurchaseDetails> purchaseDetails = JsonConvert.DeserializeObject<List<PurchaseDetails>>(PurchaseDetails);

                purchase.DateOfPurchase = DateTime.Now;
                purchase.AddedBy = 1;
                purchase.PaymentID = null;

                int purchaseID = await PurchaseController.AddPurchaseAndGetId(purchase);

                // Set the purchaseID for all purchase details
                foreach (var sd in purchaseDetails)
                {
                    sd.PurchaseID = purchaseID;
                }

                // Add all purchase details in a batch
                await PurchaseDetailsController.AddPurchaseDetails(purchaseDetails);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/purchases/purchases");
        }

    }
}
