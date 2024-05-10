using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoArsenal_App.Pages.Stock
{
    [Authorize(Roles = "Manager")]
    public class ManufacturersModel : PageModel
    {
        //for deleting
        [BindProperty]
        public int DeleteID { get; set; }

        //for creating and editing
        [BindProperty]
        public Manufacturer Manufacturer { get; set; }

        //for viewing
        [BindProperty]
        public List<Manufacturer> Manufacturers { get; set; }

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
                Manufacturers = await ManufacturerController.GetManufacturers();
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
                await ManufacturerController.AddManufacturer(Manufacturer);

                TempData["Success"] = "Manufacturer added successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Manager/Manufactorers");
        }

        // For editing
        public async Task<IActionResult> OnPostEditManufacturer()
        {
            try
            {
                await ManufacturerController.UpdateManufacturer(Manufacturer);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Manager/Manufactorers");
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
            return RedirectToPage("/Manager/Manufactorers");
        }

    }
}
