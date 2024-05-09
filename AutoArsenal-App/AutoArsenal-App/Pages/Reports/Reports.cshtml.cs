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
        public List<Sale> Sales { get; set; }
        [BindProperty]
        public List<SaleDetails> SaleDetails { get; set; }
        [BindProperty]
        public List<Product> Products { get; set; }
        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }
        public async void OnGet()
        {
            try
            {
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Sales = await SaleController.GetSales();
                SaleDetails = await SaleDetailsController.GetSaleDetails();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
