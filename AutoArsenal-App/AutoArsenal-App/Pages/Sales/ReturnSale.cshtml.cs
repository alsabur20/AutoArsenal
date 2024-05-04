using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Sales
{
    public class ReturnSaleModel : PageModel
    {
        [BindProperty]
        public int SaleId { get; set; }
        public async void OnGet(int id)
        {
            try
            {
                SaleId = id;
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
