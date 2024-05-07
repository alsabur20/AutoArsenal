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
        //for deleting
        [BindProperty]
        public int DeleteID { get; set; }

        //for creating and editing
        [BindProperty]
        public Manufacturer manufacturer { get; set; }

        //for viewing
        [BindProperty]
        public List<Manufacturer> manufacturers { get; set; }

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
                manufacturers = await ManufacturerController.GetManufacturers();
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
                await ManufacturerController.AddManufacturer(manufacturer);

                TempData["Success"] = "Manufacturer added successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return Page();
        }
        
        // For editing
        public async Task<IActionResult> OnPostEditManufacturer()
        {
            try
            {
                await ManufacturerController.UpdateManufacturer(manufacturer);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return Page(); ;
        }

        // For deleting
        public async Task<IActionResult> OnPostDeleteManufacturer()
        {
            try
            {
                await ManufacturerController.DeleteManufacturer(DeleteID);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return Page(); ;
        }

    }
}
