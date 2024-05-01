using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Manager
{
    public class CustomersModel : PageModel
    {
        //for deleting
        [BindProperty]
        public int DeleteID {  get; set; }

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
                Customers = await CustomerController.GetCustomer();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
    }
}
