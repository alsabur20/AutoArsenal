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
        public int DeleteID { get; set; }

        //for creating and editing
        [BindProperty]
        public Person Person { get; set; }
        [BindProperty]
        public Employee Employee { get; set; }

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
                Persons = await PersonController.GetPersons();
                Employees = await EmployeeController.GetEmployee();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostAddEmployee()
        {
            try
            {
                Person.Status = 8;
                Employee.ID = Person.ID;
                if (await PersonController.GetPersonId(Person) == -1)
                {
                    await EmployeeController.AddEmployee(Person, Employee);
                    TempData["Success"] = "Employee added successfully";
                }
                else
                {

                    TempData["ErrorOnServer"] = "Employee already exists";
                    return RedirectToPage("/Manager/Employees");
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Manager/Employees");
        }

        //for deleting
        public async Task<IActionResult> OnPostDeleteEmployee()
        {
            try
            {
                await PersonController.DeletePerson(DeleteID);
                return RedirectToPage("/Manager/Employees");
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return RedirectToPage("/Manager/Employees");
            }
        }

        //for editing
        public async Task<IActionResult> OnPostEditEmployee()
        {
            Employee.ID = Person.ID;
            Person.Status = 8;
            try
            {
                await PersonController.UpdatePerson(Person);
                await EmployeeController.UpdateEmployee(Employee);
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
