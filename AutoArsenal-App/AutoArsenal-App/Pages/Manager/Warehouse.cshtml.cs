using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArsenal_App.Pages.Manager
{
    public class WarehouseModel : PageModel
    {
        // for deleting warehouse
        [BindProperty]
        public int DeleteID { get; set; }
        // for displaying warehouses
        [BindProperty]
        public List<Warehouse> Warehouses { get; set; }
        // for adding or editing warehouse
        [BindProperty]
        public Warehouse Warehouse { get; set; }

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
                Warehouses = await WarehouseController.GetWarehouses();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }
        public async Task<IActionResult> OnPostAddWarehouse()
        {
            try
            {
                await WarehouseController.AddWarehouse(Warehouse);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateWarehouse()
        {
            try
            {
                await WarehouseController.UpdateWarehouse(Warehouse);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteWarehouse()
        {
            try
            {
                await WarehouseController.DeleteWarehouse(DeleteID);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage();
        }
    }
}
