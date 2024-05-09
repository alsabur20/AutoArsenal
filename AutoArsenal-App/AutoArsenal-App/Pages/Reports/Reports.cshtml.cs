using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Reports
{
    [Authorize(Roles = "Manager")]
    public class ReportsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
