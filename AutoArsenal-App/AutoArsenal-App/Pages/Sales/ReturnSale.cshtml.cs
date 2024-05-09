using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AutoArsenal_App.Pages.Sales
{
    [Authorize(Roles = "Manager,Cashier")]
    public class ReturnSaleModel : PageModel
    {
        [BindProperty]
        public string CustomerName { get; set; }

        [BindProperty]
        public string DateOfSale { get; set; }

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
                DateOfSale = Sale.DateOfSale.ToString("MMM dd, yyyy");
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        // Return Data
        [BindProperty]
        public int category { get; set; }
        [BindProperty]
        public int originalQuantity { get; set; }
        [BindProperty]
        public int returnQuantity { get; set; }

        public async Task<IActionResult> OnPostReturnSale()
        {
            try
            {
                ProductCategories = await ProductCategoryController.GetProductCategories();

                int type = await LookupController.GetLookupId("Sale", "Type");
                int id = await ReturnController.AddReturnAndGetId(DateTime.Now, type);

                ReturnDetails rtrn = new ReturnDetails();
                rtrn.ReturnID = id;
                rtrn.ProductCategoryID = category;
                rtrn.ReturnQuantity = returnQuantity;

                await ReturnDetailsController.AddReturnDetails(rtrn);

                ProductCategory prod = ProductCategories.Find(pc => pc.ID == category);
                Inventory inventory = await InventoryController.GetInventoryById(prod.InventoryId);
                inventory.StockInShop += returnQuantity;
                await InventoryController.UpdateInventory(inventory);
                await SaleController.UpdateSale(SaleID, originalQuantity - returnQuantity, category);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Sales/Sales");
        }

        public async Task<IActionResult> OnPostReturnAll()
        {
            try
            {
                ProductCategories = await ProductCategoryController.GetProductCategories();
                int type = await LookupController.GetLookupId("Sale", "Type");
                int id = await ReturnController.AddReturnAndGetId(DateTime.Now, type);
                foreach (var saleDetail in SaleDetails)
                {
                    if (SaleID == saleDetail.SaleID)
                    {
                        ReturnDetails rtrn = new ReturnDetails();
                        rtrn.ReturnID = id;
                        rtrn.ProductCategoryID = saleDetail.ProductCategoryID;
                        rtrn.ReturnQuantity = saleDetail.Quantity;
                        await ReturnDetailsController.AddReturnDetails(rtrn);

                        ProductCategory prod = ProductCategories.Find(pc => pc.ID == saleDetail.ProductCategoryID);
                        Inventory inventory = await InventoryController.GetInventoryById(prod.InventoryId);
                        inventory.StockInShop += saleDetail.Quantity;
                        await InventoryController.UpdateInventory(inventory);
                    }
                }

                await SaleController.DeleteSale(SaleID);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }

            return RedirectToPage("/Sales/Sales");
        }
    }
}
