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
        public List<double> totals { get; set; }

        [BindProperty]
        public List<Payment> Payments { get; set; }

        [BindProperty]
        public List<PaymentDetails> PaymentDetails { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }

        
        public async void OnGet()
        {
            try
            {
                Sales = await SaleController.GetSales();
                Persons = await PersonController.GetPersons();
                Payments = await PaymentController.GetPayments();
                PaymentDetails = await PaymentDetailsController.GetPaymentDetails();
                Lookups = await LookupController.GetLookup();

                if (Sales.Count > 0)
                {
                    totals = new List<double>();
                    remainings = new List<double>();
                    foreach (var sale in Sales)
                    {
                        if (!sale.IsDeleted)
                        {
                            totals.Add(Payments.Find(p => p.ID == sale.PaymentID).TotalAmount);
                            List<PaymentDetails> pds = PaymentDetails.FindAll(pd => pd.PaymentID == sale.PaymentID);
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

        // Add Payments
        [BindProperty]
        public PaymentDetails pay { get; set; }
        
        public async Task<IActionResult> OnPostAddPayment()
        {
            try
            {
                if (pay.PaymentAccount == null)
                    pay.PaymentAccount = "Cash";
                pay.PaymentType = await LookupController.GetLookupId("Sale", "Type");
                pay.DateOfPayment = DateTime.Now;
                pay.PaymentMethod = await LookupController.GetLookupId("Cash", "Payment_Method");
                await PaymentDetailsController.AddPaymentDetails(pay);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Sales/Sales");
        }
    }
}
