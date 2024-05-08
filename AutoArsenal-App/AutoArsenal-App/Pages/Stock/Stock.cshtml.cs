using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Stock
{
    [Authorize(Roles = "Manager")]
    public class StockModel : PageModel
    {
        // for moving stock
        [BindProperty]
        public int ProductCategoryId { get; set; }
        [BindProperty]
        public int From { get; set; }
        [BindProperty]
        public int To { get; set; }
        [BindProperty]
        public int Quantity { get; set; }

        // for viewing
        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public List<ProductCategory> ProductCategories { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }

        [BindProperty]
        public List<Warehouse> Warehouses { get; set; }
        [BindProperty]
        public List<Inventory> Inventories { get; set; }
        public async void OnGet()
        {
            try
            {
                Lookups = await LookupController.GetLookup();
                Products = await ProductController.GetProducts();
                ProductCategories = await ProductCategoryController.GetProductCategories();
                Warehouses = await WarehouseController.GetWarehouses();
                Inventories = await InventoryController.GetInventory();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
        public async Task<IActionResult> OnPostMoveStock()
        {
            try
            {
                ProductCategory productCategory = await ProductCategoryController.GetProductCategoryById(ProductCategoryId);
                if (productCategory == null)
                {
                    TempData["ErrorOnServer"] = "Product category not found";
                }
                else
                {
                    Inventory inventory = await InventoryController.GetInventoryById(productCategory.InventoryId);
                    if (inventory.StockInShop == -1)
                        inventory.StockInShop = 0;
                    if (inventory.StockInWarehouse == -1)
                        inventory.StockInWarehouse = 0;
                    if (inventory == null)
                    {
                        TempData["ErrorOnServer"] = "Inventory not found";
                    }
                    else
                    {
                        int stockInShop = inventory.StockInShop;
                        int stockInWarehouse = inventory.StockInWarehouse;
                        if (From == 0)
                        {
                            // stock check validations
                            if (stockInShop < Quantity)
                            {
                                TempData["ErrorOnServer"] = "Not enough stock in shop";
                                return RedirectToPage("/Stock/Stock");
                            }
                            inventory.WarehouseId = To;
                            inventory.StockInShop -= Quantity;
                            inventory.StockInWarehouse += Quantity;
                        }
                        else
                        {
                            if (stockInWarehouse < Quantity)
                            {
                                TempData["ErrorOnServer"] = "Not enough stock in warehouse";
                                return RedirectToPage("/Stock/Stock");
                            }
                            inventory.WarehouseId = From;
                            inventory.StockInShop += Quantity;
                            inventory.StockInWarehouse -= Quantity;
                        }
                        await InventoryController.UpdateInventory(inventory);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Stock/Stock");
        }
    }
}
