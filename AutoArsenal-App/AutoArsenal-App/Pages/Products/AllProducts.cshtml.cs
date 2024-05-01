using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoArsenal_App.Pages.Products
{
    public class ProductsModel : PageModel
    {
        // for deleting
        [BindProperty]
        public int DeleteID { get; set; }

        // for viewing
        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }

        [BindProperty]
        public List<Manufacturer> Manufacturers { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                Lookups = await LookupController.GetLookup();
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategory();
                Manufacturers = await ManufacturerController.GetManufacturers();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostUpdateProductAsync(Product product)
        {
            try
            {
                
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteProductAsync()
        {
            try
            {
                
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return Page();
            }
        }


    }
}
