using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages
{
    [Authorize(Roles = "Manager,Cashier")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.IsInRole("Manager"))
            {
                return RedirectToPage("/Dashboards/MDashboard");
            }
            else if (User.IsInRole("Cashier"))
            {
                return RedirectToPage("/Dashboards/CDashboard");

            }
            return RedirectToPage("");
        }
    }
}
