using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Reports
{
    [Authorize(Roles = "Manager")]
    public class ReportsModel : PageModel
    {
        [BindProperty]
        public double TotalSales { get; set; }
        [BindProperty]
        public int ProductCount { get; set; }
        [BindProperty]
        public int EmployeeCount { get; set; }
        [BindProperty]
        public int CustomerCount { get; set; }
        [BindProperty]
        public List<Sale> Sales { get; set; }
        [BindProperty]
        public List<SaleDetails> SaleDetails { get; set; }
        [BindProperty]
        public List<Product> Products { get; set; }
        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }
        [BindProperty]
        public List<Employee> Employees { get; set; }
        [BindProperty]
        public List<Customer> Customers { get; set; }
        [BindProperty]
        public List<Person> Persons { get; set; }
        public async void OnGet()
        {
            try
            {
                Sales = await SaleController.GetSales();
                SaleDetails = await SaleDetailsController.GetSaleDetails();
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Employees = await EmployeeController.GetEmployee();
                Customers = await CustomerController.GetCustomers();
                Persons = await PersonController.GetPersons();
                TotalSales = SaleController.GetTotalSales();
                ProductCount = ProductCategoryController.GetProductCategoryCount();
                EmployeeCount = EmployeeController.GetEmployeeCount();
                CustomerCount = CustomerController.GetCustomerCount();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
