using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;


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

        private string employeeId = "";
        // Return Data
        [BindProperty]
        public int Category { get; set; }
        [BindProperty]
        public int OriginalQuantity { get; set; }
        [BindProperty]
        public int ReturnQuantity { get; set; }

        public async Task<IActionResult> OnPostReturnSale()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;

                Claim userIdClaim = currentUser.FindFirst("UserId");

                if (userIdClaim != null)
                {
                    // Get the value of the UserId claim
                    employeeId = userIdClaim.Value;

                    // Now you can use the userIdValue wherever you need it
                }
                ProductCategories = await ProductCategoryController.GetProductCategories();

                int type = await LookupController.GetLookupId("Sale", "Type");
                Return rtn = new()
                {
                    ReturnType = type,
                    DateOfReturn = DateTime.Now,
                    AddedBy = int.Parse(employeeId)
                };
                int id = await ReturnController.AddReturnAndGetId(rtn);


                ReturnDetails rtrn = new ReturnDetails
                {
                    ReturnID = id,
                    ProductCategoryID = Category,
                    ReturnQuantity = ReturnQuantity
                };

                await ReturnDetailsController.AddReturnDetails(rtrn);

                ProductCategory prod = ProductCategories.Find(pc => pc.ID == Category);
                Inventory inventory = await InventoryController.GetInventoryById(prod.InventoryId);
                inventory.StockInShop += ReturnQuantity;
                await InventoryController.UpdateInventory(inventory);
                await SaleController.UpdateSale(SaleID, OriginalQuantity - ReturnQuantity, Category);
                await SaleController.UpdateSaleStatus();
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
                Return rtn = new()
                {
                    ReturnType = type,
                    DateOfReturn = DateTime.Now,
                    AddedBy = int.Parse(employeeId)
                };
                int id = await ReturnController.AddReturnAndGetId(rtn);
                foreach (var saleDetail in SaleDetails)
                {
                    if (SaleID == saleDetail.SaleID)
                    {
                        ReturnDetails rtrn = new ReturnDetails
                        {
                            ReturnID = id,
                            ProductCategoryID = saleDetail.ProductCategoryID,
                            ReturnQuantity = saleDetail.Quantity
                        };
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
