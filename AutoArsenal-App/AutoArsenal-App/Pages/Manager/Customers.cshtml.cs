using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Manager
{
    [Authorize(Roles = "Manager")]
    public class CustomersModel : PageModel
    {
        //for deleting
        [BindProperty]
        public int DeleteID { get; set; }

        //for viewing
        [BindProperty]
        public List<Lookup> Lookups { get; set; }
        [BindProperty]
        public List<Person> Persons { get; set; }
        [BindProperty]
        public List<Customer> Customers { get; set; }

        public async void OnGet()
        {
            try
            {
                Lookups = await LookupController.GetLookup();
                Persons = await PersonController.GetPersons();
                Customers = await CustomerController.GetCustomers();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostDeleteCustomer()
        {
            try
            {
                await PersonController.DeletePerson(DeleteID);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Manager/Customers");
        }
    }
}