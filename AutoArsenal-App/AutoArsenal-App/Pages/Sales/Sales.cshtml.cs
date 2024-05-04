using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Sales
{
    public class SalesModel : PageModel
    {
        [BindProperty]
        public int DeleteId { get; set; }
        public List<Sale> Sales { get; set; }
        [BindProperty]
        public List<Person> Persons { get; set; }
        public async void OnGet()
        {
            try
            {
                Sales = await SaleController.GetSales();
                Persons = await PersonController.GetPersons();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
        public async Task<IActionResult> OnPostDeleteSale()
        {
            try
            {
                await SaleController.DeleteSale(DeleteId);
                return RedirectToPage("/Sales/Sales");
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return RedirectToPage("/Sales/Sales");
            }
        }
    }
}
