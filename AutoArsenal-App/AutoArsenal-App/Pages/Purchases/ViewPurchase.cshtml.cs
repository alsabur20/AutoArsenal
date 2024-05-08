using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Purchases
{
    [Authorize(Roles = "Manager")]
    public class ViewPurchaseModel : PageModel
    {
        [BindProperty]
        public string DateOfPurchase { get; set; }

        [BindProperty]
        public int PurchaseID { get; set; }

        [BindProperty]
        public Purchase Purchase { get; set; }

        public PurchaseDetails details { get; set; }

        [BindProperty]
        public List<PurchaseDetails> PurchaseDetails { get; set; }

        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }

        [BindProperty]
        public List<Manufacturer> People { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }

        [BindProperty]
        public double Total { get; set; }

        public async void OnGet(int id)
        {
            try
            {
                PurchaseID = id;
                Purchase = await PurchaseController.GetPurchase(id);
                PurchaseDetails = await PurchaseDetailsController.GetPurchaseDetailsByID(Purchase.ID);
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Lookups = await LookupController.GetLookup();
                People = await ManufacturerController.GetManufacturers();

                details = PurchaseDetails.Find(pd => pd.PurchaseID == PurchaseID); 
                Total = 0;
                DateOfPurchase = Purchase.DateOfPurchase.ToString("MMM dd, yyyy");
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }


        public async Task<IActionResult> OnPostAddQuantity()
        {
            try
            {

            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage();
        }

    }
}
