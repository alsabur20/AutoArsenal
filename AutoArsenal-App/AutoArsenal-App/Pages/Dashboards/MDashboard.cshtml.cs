using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Dashboards
{
    [Authorize(Roles = "Manager")]
    public class DashboardModel : PageModel
    {
        public async void OnGet()
        {
        }
    }
}
