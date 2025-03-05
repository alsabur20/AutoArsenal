using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Dashboards
{
    [Authorize(Roles = "Cashier")]
    public class CDashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
