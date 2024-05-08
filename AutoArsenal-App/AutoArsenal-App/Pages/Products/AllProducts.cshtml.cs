using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoArsenal_App.Pages.Products
{
    [Authorize(Roles = "Manager")]
    public class ProductsModel : PageModel
    {
        // for editing
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public ProductCategory ProductCategory { get; set; }
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
        [BindProperty]
        public List<Inventory> Inventories { get; set; }

        public async Task OnGet()
        {
            try
            {
                Lookups = await LookupController.GetLookup();
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Manufacturers = await ManufacturerController.GetManufacturers();
                Inventories = await InventoryController.GetInventory();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostUpdateProduct(Product product)
        {
            try
            {
                await ProductController.UpdateProduct(product);
                await ProductCategoryController.UpdateProductCategory(ProductCategory);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Products/AllProducts");
        }

        public async Task<IActionResult> OnPostDeleteProduct()
        {
            try
            {
                await ProductCategoryController.DeleteProductCategory(DeleteID);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Products/AllProducts");
        }


    }
}
