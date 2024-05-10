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

        [BindProperty]
        public int PaidAmount { get; set; } = 0;
        [BindProperty]
        public int PID { get; set; }
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
                    foreach (var sale in Sales)
                    {
                        if (!sale.IsDeleted)
                        {
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

        // Add Quantity in payments
        [BindProperty]
        public int SaleID { get; set; }

        public async Task<IActionResult> OnPostAddPayment()
        {
            try
            {
                PaymentDetails p = new PaymentDetails
                {
                    PaymentID = PID,
                    PaidAmount = PaidAmount,
                    PaymentMethod = 7,
                    PaymentAccount = "cash",
                    PaymentType = 13,
                    DateOfPayment = DateTime.Now
                };
                await PaymentDetailsController.AddPaymentDetails(p);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Sales/Sales");
        }
    }
}
