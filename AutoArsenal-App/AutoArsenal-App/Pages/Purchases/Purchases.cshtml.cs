using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AutoArsenal_App.Pages.Purchases
{
    [Authorize(Roles = "Manager")]
    public class PurchasesModel : PageModel
    {
        [BindProperty]
        public List<Purchase> Purchases { get; set; }

        [BindProperty]
        public List<PurchaseDetails> purchaseDetails { get; set; }
        
        [BindProperty]
        public List<Person> Persons { get; set; }


        [BindProperty]
        public List<Payment> Payments { get; set; }

        [BindProperty]
        public List<PaymentDetails> PaymentDetails { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }


        [BindProperty]
        public List<double> remainings { get; set; }
        [BindProperty]
        public List<double> totals { get; set; }
        
        
        private string employeeId;

        public async void OnGet()
        {
            try
            {
                Purchases = await PurchaseController.GetPurchases();
                Persons = await PersonController.GetPersons();
                Payments = await PaymentController.GetPayments();
                PaymentDetails = await PaymentDetailsController.GetPaymentDetails();
                purchaseDetails = await PurchaseDetailsController.GetPurchaseDetails();
                Lookups = await LookupController.GetLookup();

                if (Purchases != null)
                {
                    totals = new List<double>();
                    remainings = new List<double>();
                    foreach (var purchase in Purchases)
                    {
                        if (!purchase.IsDeleted)
                        {
                            totals.Add(Payments.Find(p => p.ID == purchase.PaymentID).TotalAmount);
                            List<PaymentDetails> pds = PaymentDetails.FindAll(pd => pd.PaymentID == purchase.PaymentID);
                            remainings.Add(pds.Sum(p => p.PaidAmount));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }


        // Add Payment
        [BindProperty]
        public PaymentDetails pay {  get; set; }
        
        public async Task<IActionResult> OnPostAddPayment()
        {
            try
            {
                if (pay.PaymentAccount == null)
                    pay.PaymentAccount = "Cash";
                pay.PaymentType = await LookupController.GetLookupId("Purchase", "Type");
                pay.DateOfPayment = DateTime.Now;
                await PaymentDetailsController.AddPaymentDetails(pay);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
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
                ClaimsPrincipal currentUser = this.User;
                Claim userIdClaim = currentUser.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    // Get the value of the UserId claim
                    employeeId = userIdClaim.Value;
                }

                List<ProductCategory> ProductCategories = await ProductCategoryController.GetProductCategories();
                purchaseDetails = await PurchaseDetailsController.GetPurchaseDetails();

                int type = await LookupController.GetLookupId("Purchase", "Type");
                Return rtn = new Return
                {
                    ReturnType = type,
                    DateOfReturn = DateTime.Now,
                    AddedBy = int.Parse(employeeId)
                };
                int id = await ReturnController.AddReturnAndGetId(rtn);
                
                foreach (var prch in purchaseDetails)
                {
                    if (PurchaseId == prch.PurchaseID)
                    {
                        ReturnDetails rtrn = new ReturnDetails();
                        rtrn.ReturnID = id;
                        rtrn.ProductCategoryID = prch.ProductCategoryID;
                        rtrn.ReturnQuantity = prch.ReceivedQuantity;
                        await ReturnDetailsController.AddReturnDetails(rtrn);                        

                        ProductCategory prod = ProductCategories.Find(pc => pc.ID == prch.ProductCategoryID);
                        Inventory inventory = await InventoryController.GetInventoryById(prod.InventoryId);
                        if(prch.ReceivedQuantity > inventory.StockInShop + inventory.StockInWarehouse)
                        {
                            TempData["ErrorOnServer"] = "Quantity to return is greater than stock.";
                            return RedirectToPage("/Purchases/Purchases");
                        }
                        else if(prch.ReceivedQuantity <= inventory.StockInShop)
                        {
                            inventory.StockInShop -= prch.ReceivedQuantity;
                        }
                        else if (prch.ReceivedQuantity > inventory.StockInShop && prch.ReceivedQuantity < inventory.StockInShop + inventory.StockInWarehouse)
                        {
                            int shopStock = inventory.StockInShop;
                            inventory.StockInShop = 0;
                            inventory.StockInWarehouse -= prch.ReceivedQuantity - shopStock;
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
