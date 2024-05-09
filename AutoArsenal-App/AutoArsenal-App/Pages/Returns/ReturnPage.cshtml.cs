using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Returns
{
    public class ReturnPageModel : PageModel
    {
        [BindProperty]
        public List<Product> products { get; set; }

        [BindProperty]
        public List<ProductCategory> productDetails { get; set; }

        [BindProperty]
        public List<Return> returns { get; set; }

        [BindProperty]
        public List<ReturnDetails> returnDetails { get; set; }

        public async void OnGet()
        {
            try
            {
                products = await ProductController.GetProducts();
                returns = await ReturnController.GetReturns();
                productDetails = await ProductCategoryController.GetProductCategories();
                returnDetails = await ReturnDetailsController.GetReturnDetails();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
