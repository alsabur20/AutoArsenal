using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Payments
{
    public class ViewPaymentDetailsModel : PageModel
    {
        [BindProperty]
        public int PayId { get; set; }

        [BindProperty]
        public string PayType { get; set; }

        [BindProperty]
        public List<PaymentDetails> paymentDetails { get; set; }

        [BindProperty]
        public List<Lookup> lookups { get; set; }

        public async void OnGet(int id)
        {
            try
            {
                PayId = id;
                paymentDetails = await PaymentDetailsController.GetPaymentDetails();
                lookups = await LookupController.GetLookup();

                PaymentDetails py = paymentDetails.Find(pd => pd.PaymentID == PayId);
                PayType = lookups.Find(lk => lk.ID == py.PaymentType).Value;

            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
