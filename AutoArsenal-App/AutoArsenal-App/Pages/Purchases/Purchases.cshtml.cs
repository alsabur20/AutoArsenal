using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Purchases
{
    public class PurchasesModel : PageModel
    {
        [BindProperty]
        public List<Purchase> Purchases { get; set; }
        
        [BindProperty]
        public List<Person> Persons { get; set; }


        [BindProperty]
        public List<Payment> Payments { get; set; }

        [BindProperty]
        public List<PaymentDetails> PaymentDetails { get; set; }


        public async void OnGet()
        {
            try
            {
                Purchases = await PurchaseController.GetPurchases();
                Persons = await PersonController.GetPersons();
                Payments = await PaymentController.GetPayments();
                PaymentDetails = await PaymentDetailsController.GetPaymentDetails();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }


        // Add Payment
        [BindProperty]
        public int PaymentID { get; set; }

        public async Task<IActionResult> OnPostAddPayment()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                
            }
            return RedirectToPage("/Purchases/Purchases");
        }

        // Return Purchase
        [BindProperty]
        public int PurchaseId { get; set; }

        public async Task<IActionResult> OnPostReturnPurchase()
        {
            try
            {
                List<ProductCategory> ProductCategories = await ProductCategoryController.GetProductCategories();
                List<PurchaseDetails> purchaseDetails = await PurchaseDetailsController.GetPurchaseDetails();

                int type = await LookupController.GetLookupId("Sale", "Type");
                int id = await ReturnController.AddReturnAndGetId(DateTime.Now, type);
                
                foreach (var prch in purchaseDetails)
                {
                    if (PurchaseId == prch.PurchaseID)
                    {
                        ReturnDetails rtrn = new ReturnDetails();
                        rtrn.ReturnID = id;
                        rtrn.ProductCategoryID = prch.ProductCategoryID;
                        rtrn.ReturnQuantity = prch.Quantity;
                        await ReturnDetailsController.AddReturnDetails(rtrn);                        

                        ProductCategory prod = ProductCategories.Find(pc => pc.ID == prch.ProductCategoryID);
                        Inventory inventory = await InventoryController.GetInventoryById(prod.InventoryId);
                        if(prch.Quantity > inventory.StockInShop + inventory.StockInWarehouse)
                        {
                            TempData["ErrorOnServer"] = "Quantity to return is greater than stock.";
                            return RedirectToPage("/Purchases/Purchases");
                        }
                        else if(prch.Quantity <= inventory.StockInShop)
                        {
                            inventory.StockInShop -= prch.Quantity;
                        }
                        else if (prch.Quantity > inventory.StockInShop && prch.Quantity < inventory.StockInShop + inventory.StockInWarehouse)
                        {
                            int shopStock = inventory.StockInShop;
                            inventory.StockInShop = 0;
                            inventory.StockInWarehouse -= prch.Quantity - shopStock;
                        }
                        await InventoryController.UpdateInventory(inventory);
                    }
                }

                await PurchaseController.DeletePurchase(PurchaseId);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Purchases/Purchases");
        }
    }
}
