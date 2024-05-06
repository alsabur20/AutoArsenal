using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoArsenal_App.Pages.Manager
{
    public class ManufacturersModel : PageModel
    {
   

        // For creating and editing
        [BindProperty]
        public Manufacturer Manufacturer { get; set; }

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
                // Additional logic for fetching manufacturer data can be added here if needed
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostAddManufacturer()
        {
            try
            {
                // Additional logic for adding a manufacturer can be added here
                TempData["Success"] = "Manufacturer added successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Manager/Manufacturers");
        }

        // For deleting
        public async Task<IActionResult> OnPostDeleteManufacturer()
        {
            try
            {
                // Additional logic for deleting a manufacturer can be added here
                return RedirectToPage("/Manager/Manufacturers");
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return RedirectToPage("/Manager/Manufacturers");
            }
        }

        // For editing
        public async Task<IActionResult> OnPostEditManufacturer()
        {
            try
            {
                // Additional logic for editing a manufacturer can be added here
                return RedirectToPage("/Manager/Manufacturers");
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return RedirectToPage("/Manager/Manufacturers");
            }
        }
    }
}
