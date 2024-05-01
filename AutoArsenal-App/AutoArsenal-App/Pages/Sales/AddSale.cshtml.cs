using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AutoArsenal_App.Pages.Sales
{
    public class AddSaleModel : PageModel
    {
        [BindProperty]
        public string Sales { get; set; }
        [BindProperty]
        public string SaleDetails { get; set; }
        [BindProperty]
        public List<Product> Products { get; set; }
        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }
        [BindProperty]
        public List<Lookup> Lookups { get; set; }
        [BindProperty]
        public List<Person> Persons { get; set; }
        [BindProperty]
        public List<Customer> Customers { get; set; }
        public async void OnGet()
        {
            try
            {
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Lookups = await LookupController.GetLookup();
                Persons = await PersonController.GetPersons();
                Customers = await CustomerController.GetCustomer();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
        public async Task<IActionResult> OnPostSaveSales()
        {
            try
            {
                Sale sale = JsonConvert.DeserializeObject<Sale>(Sales);
                List<SaleDetails> saleDetails = JsonConvert.DeserializeObject<List<SaleDetails>>(SaleDetails);

                sale.DateOfSale = DateTime.Now;
                sale.EmployeeID = 1;
                sale.PaymentID = null;

                int saleID = await SaleController.AddSaleAndGetId(sale);

                // Set the SaleID for all sale details
                foreach (var sd in saleDetails)
                {
                    sd.SaleID = saleID;
                }

                // Add all sale details in a batch
                await SaleDetailsController.AddSaleDetails(saleDetails);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Sales/Sales");
        }

    }
}
