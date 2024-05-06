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


        // Adding Customer 
        [BindProperty]
        public Person Person { get; set; }
        
        [BindProperty]
        public Customer customer { get; set; }

        [BindProperty]
        public List<string> Cities { get; set; }
        [BindProperty]
        public List<string> Provinces { get; set; }

        public async void OnGet()
        {
            // For Adding Customers
            Cities = new List<string> {
            "Karachi",
            "Lahore",
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
            "Nawabshah",
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

            try
            {
                Lookups = await LookupController.GetLookup();
                Persons = await PersonController.GetPersons();
                Customers = await CustomerController.GetCustomers();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }

            // For Adding Sale
            try
            {
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
        public async Task<IActionResult> OnPostAddCustomer()
        {
            try
            {
                Person.Status = 8;
                if (await PersonController.GetPersonId(Person) == -1)
                {
                    await CustomerController.AddCustomer(Person, customer);
                }
                else
                {

                    TempData["ErrorOnServer"] = "Customer already exists";
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Sales/AddSale");
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
