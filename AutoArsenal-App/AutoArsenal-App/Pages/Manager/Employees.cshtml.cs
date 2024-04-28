using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Manager
{
    public class EmplyeesModel : PageModel
    {
        //for deleting
        [BindProperty]
        public int DeleteID {  get; set; }

        //for creating new
        [BindProperty]
        public Person Person { get; set; }
        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]
        public Location Location { get; set; }

        //for viewing
        [BindProperty]
        public List<Lookup> Lookups { get; set; }
        [BindProperty]
        public List<Person> Persons { get; set; }
        [BindProperty]
        public List<Employee> Employees { get; set; }
        
        [BindProperty]
        public List<string> Cities { get; set; }
        [BindProperty]
        public List<string> Provinces { get; set; }
        public async void OnGet()
        {
            Cities = new List<string> {
            "Karachi",
            "Lahore",
            "Islamabad",
            "Faisalabad",
            "Rawalpindi",
            "Multan",
            "Gujranwala",
            "Peshawar",
            "Quetta",
            "Sialkot",
            "Hyderabad",
            "Bahawalpur",
            "Sargodha",
            "Abbottabad",
            "Sukkur",
            "Nawabshah",
            "Mingora",
            "Mirpur Khas",
            "Sheikhupura",
            "Jhang"};
            
            Provinces = new List<string>{
            "Punjab",
            "Sindh",
            "Khyber Pakhtunkhwa",
            "Balochistan",
            "Gilgit-Baltistan" };
            
            try
            {
                Lookups = await LookupController.GetLookup();
                Persons = await PersonController.GetPerson();
                Employees = await EmployeeController.GetEmployee();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }


        public async Task<IActionResult> OnPostAddEmployee()
        {
            int peronId = -1;
            int locationId = -1;
            try
            {
                if (Location != null)
                {
                    int existingLocationId = await LocationController.GetLocationId(Location);
                    if(existingLocationId != -1)
                    {
                        locationId = existingLocationId;
                    }
                    else
                    {
                        await LocationController.AddLocation(Location);
                        locationId = await LocationController.GetLocationId(Location);
                    }
                }
                if (Person != null)
                {
                    Person.LocationId = locationId;
                    await PersonController.AddPerson(Person);
                    peronId = await PersonController.GetPersonId(Person);
                }
                if (Employee != null)
                {
                    Employee.ID = peronId;
                    await EmployeeController.AddEmployee(Employee);
                }
                return RedirectToPage("/Manager/Employees");
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return RedirectToPage("/Manager/Employees");
            }
        }
    }
}
