using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AutoArsenal_App.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credentials credentials { get; set; }
        private Employee employee { get; set; }

        private string Role { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                bool isValid = await ValidateUser(credentials.Username, credentials.Password);
                if (isValid)
                {
                    var claims = new List<Claim>
                {
                    new Claim("UserId", employee.ID.ToString()),
                    new Claim(ClaimTypes.Name, credentials.Username.ToString()),
                    new Claim(ClaimTypes.Role, Role)
                };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    if (Role == "Manager")
                    {
                        return RedirectToPage("../Manager/Dashboard");
                    }
                    else if (Role == "Cashier")
                    {
                        return RedirectToPage("../Index");
                    }
                    else
                        return RedirectToPage("../Index");
                }
                TempData["ErrorOnServer"] += "Invalid Credentials..!!";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] += ex.Message;

                return Page();
            }
        }

        private async Task<bool> ValidateUser(string username, string password)
        {
            try
            {
                credentials = await CredentialController.GetCredential(username, password);
                if (credentials.ID > 0)
                {
                    employee = await EmployeeController.GetEmployeeByCredential(credentials.ID);
                    if (employee.ID > 0)
                    {
                        Role = await LookupController.GetLookupValue(employee.Role);
                        return true;
                    }
                    else
                    {
                        TempData["ErrorOnServer"] = "Employee not found";
                        return false;
                    }
                }
                else
                {
                    TempData["ErrorOnServer"] = "Credentials not found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
                return false;
            }
        }
    }
}
