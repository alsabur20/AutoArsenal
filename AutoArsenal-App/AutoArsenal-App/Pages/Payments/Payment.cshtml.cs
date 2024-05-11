using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Serialization;

namespace AutoArsenal_App.Pages.Payments
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public List<Payment> payments { get; set; }

        [BindProperty]
        public List<double> remain { get; set; }

        [BindProperty]
        public List<string> status { get; set; }

        public async void OnGet()
        {
            try
            {
                remain = new List<double>();
                status = new List<string>();

                payments = await PaymentController.GetPayments();
                for (int i = 0; i < payments.Count; i++)
                {
                    remain.Add(await PaymentDetailsController.GetRemaining(payments[i].ID));
                    
                    if (remain[i] < payments[i].TotalAmount)
                        status.Add("InComplete");
                    else
                        status.Add("Complete");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
