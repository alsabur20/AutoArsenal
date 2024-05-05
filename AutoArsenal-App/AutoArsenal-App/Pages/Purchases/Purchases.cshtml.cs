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
            return RedirectToPage();
        }

        // Return Purchase
        public async Task<IActionResult> OnPostReturnPurchase()
        {
            try
            {

            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage();
        }
    }
}
