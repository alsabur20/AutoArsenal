using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AutoArsenal_App.Pages.Purchases
{
    [Authorize(Roles = "Manager")]
    public class AddPurchaseModel : PageModel
    {
        [BindProperty]
        public string Purchases { get; set; }

        [BindProperty]
        public string PurchaseDetails { get; set; }

        [BindProperty]
        public int WarehouseId { get; set; }

        [BindProperty]
        public string ManufacturerName { get; set; }

        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }

        [BindProperty]
        public List<Inventory> Inventories { get; set; }

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
                Inventories = await InventoryController.GetInventory();
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

        // Add Payment
        [BindProperty]
        public PaymentDetails Pay { get; set; }

        public async Task<IActionResult> OnPostSavePurchases()
        {
            try
            {
                Purchase purchase = JsonConvert.DeserializeObject<Purchase>(Purchases);
                List<PurchaseDetails> purchaseDetails = JsonConvert.DeserializeObject<List<PurchaseDetails>>(PurchaseDetails);

                purchase.DateOfPurchase = DateTime.Now;
                purchase.AddedBy = 1;
                purchase.PaymentID = await PaymentController.AddPaymentAndGetID(purchaseDetails.Sum(pd => pd.Quantity * pd.UnitPrice));

                Pay.PaymentID = purchase.PaymentID;
                if (Pay.PaymentAccount == null)
                    Pay.PaymentAccount = "Cash";
                Pay.PaymentType = await LookupController.GetLookupId("Purchase", "Type");
                Pay.DateOfPayment = DateTime.Now;
                await PaymentDetailsController.AddPaymentDetails(Pay);

                int purchaseID = await PurchaseController.AddPurchaseAndGetId(purchase);
                ProductCategories = await ProductCategoryController.GetProductCategories();

                // Set the purchaseID for all purchase details                
                foreach (var pd in purchaseDetails)
                {
                    pd.PurchaseID = purchaseID;
                    pd.ReceivedQuantity = 0;

                    Inventory inv = new Inventory();
                    inv.StockInShop = 0;
                    inv.StockInWarehouse = 0;
                    inv.WarehouseId = WarehouseId;
                    
                    ProductCategory p = ProductCategories.Find(pro => pro.ID == pd.ProductCategoryID);
                    p.InventoryId =  await InventoryController.AddInventory(inv);
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
