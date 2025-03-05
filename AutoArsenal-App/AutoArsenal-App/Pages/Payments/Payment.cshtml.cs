using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Payments
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public List<Payment> Payments { get; set; }
        public async void OnGet()
        {
            try
            {
                Payments = await PaymentController.GetPayments();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
