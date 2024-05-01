using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Sales
{
    public class ViewSaleModel : PageModel
    {
        [BindProperty]
        public string CustomerName { get; set; }
        [BindProperty]
        public int SaleID { get; set; }
        [BindProperty]
        public Sale Sale { get; set; }
        [BindProperty]
        public List<SaleDetails> SaleDetails { get; set; }
        [BindProperty]
        public List<Product> Products { get; set; }
        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }
        [BindProperty]
        public List<Person> People { get; set; }
        [BindProperty]
        public List<Lookup> Lookups { get; set; }
        [BindProperty]
        public double Total { get; set; }
        public async void OnGet(int id)
        {
            try
            {
                SaleID = id;
                Sale = await SaleController.GetSale(id);
                SaleDetails = await SaleDetailsController.GetSaleDetails(id);
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Lookups = await LookupController.GetLookup();
                People = await PersonController.GetPersons();
                Person p = People.Find(p => p.ID == Sale.CustomerID);
                CustomerName = p.FirstName + " " + p.LastName;
                Total = 0;
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
