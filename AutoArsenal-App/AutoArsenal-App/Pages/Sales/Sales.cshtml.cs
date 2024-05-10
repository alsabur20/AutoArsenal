using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Sales
{
    [Authorize(Roles = "Manager,Cashier")]
    public class SalesModel : PageModel
    {
        [BindProperty]
        public int DeleteId { get; set; }
        
        [BindProperty]
        public List<Sale> Sales { get; set; }
        
        [BindProperty]
        public List<Person> Persons { get; set; }
        
        [BindProperty]
        public List<double> remainings { get; set; }
        
        [BindProperty]
        public List<Payment> Payments { get; set; }
        
        [BindProperty]
        public List<PaymentDetails> PaymentDetails { get; set; }


        public async void OnGet()
        {
            try
            {
                Sales = await SaleController.GetSales();
                Persons = await PersonController.GetPersons();
                Payments = await PaymentController.GetPayments();
                PaymentDetails = await PaymentDetailsController.GetPaymentDetails();
                
                if (Sales.Count > 0)
                {
                    remainings = new List<double>();
                    for (int i = 0; i < Sales.Count; i++)
                    {
                        remainings.Add(await PaymentDetailsController.GetRemaining(Sales[i].PaymentID));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        // Add Quantity in payments
        [BindProperty]
        public int SaleID { get; set; }

        public async Task<IActionResult> OnPostAddPayment()
        {
            try
            {

            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Sales/Sales");
        }
    }
}
