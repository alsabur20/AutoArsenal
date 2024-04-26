using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Manager
{
    public class DashboardModel : PageModel
    {
        public required List<Lookup> Lookup { get; set; }
        public async void OnGet()
        {
            try
            {
                Lookup = await LookupController.GetLookup();
                foreach (Lookup lookup in Lookup)
                {
                    Console.WriteLine(lookup.Value);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }
    }
}
