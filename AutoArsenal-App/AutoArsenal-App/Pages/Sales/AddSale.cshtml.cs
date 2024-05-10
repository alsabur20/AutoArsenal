using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AutoArsenal_App.Pages.Sales
{
    [Authorize(Roles = "Manager,Cashier")]
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
        public Customer Customer { get; set; }

        [BindProperty]
        public List<string> Cities { get; set; }
        [BindProperty]
        public List<string> Provinces { get; set; }
        [BindProperty]
        public List<Inventory> Inventories { get; set; }

        private string employeeId;
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
                Inventories = await InventoryController.GetInventory();
            
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
                    await CustomerController.AddCustomer(Person, Customer);
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

        // Integrating payment
        [BindProperty]
        public PaymentDetails Pay { get; set; }

        [BindProperty]
        public double Total { get; set; }

        public async Task<IActionResult> OnPostSaveSales()
        {
            try
            {
                Sale sale = JsonConvert.DeserializeObject<Sale>(Sales);
                List<SaleDetails> saleDetails = JsonConvert.DeserializeObject<List<SaleDetails>>(SaleDetails);

                // get employee id from session
                ClaimsPrincipal currentUser = this.User;

                Claim userIdClaim = currentUser.FindFirst("UserId");

                if (userIdClaim != null)
                {
                    // Get the value of the UserId claim
                    employeeId = userIdClaim.Value;

                    // Now you can use the userIdValue wherever you need it
                }

                Pay.PaymentID = await PaymentController.AddPaymentAndGetID(Total);
                Pay.PaymentType = await LookupController.GetLookupId("Sale", "Type");
                Pay.DateOfPayment = DateTime.Now;
                if(Pay.PaymentAccount == null)
                {
                    Pay.PaymentAccount = "Cash";
                }
                await PaymentDetailsController.AddPaymentDetails(Pay);

                sale.DateOfSale = DateTime.Now;
                sale.EmployeeID = Convert.ToInt32(employeeId);
                sale.PaymentID = Pay.PaymentID;

                // Set the SaleID for all sale details
                foreach (var sd in saleDetails)
                {
                    ProductCategory pc = await ProductCategoryController.GetProductCategoryById(sd.ProductCategoryID);
                    Inventory inventory = await InventoryController.GetInventoryById(pc.InventoryId);
                    if (inventory.StockInShop < sd.Quantity)
                    {
                        TempData["ErrorOnServer"] = "Not enough stock in shop";
                        return RedirectToPage("/Sales/AddSale");
                    }
                    inventory.StockInShop -= sd.Quantity;
                    await InventoryController.UpdateInventory(inventory);
                }
                
                int saleID = await SaleController.AddSaleAndGetId(sale);
                foreach (var sd in saleDetails)
                {
                    sd.SaleID = saleID;
                }
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
