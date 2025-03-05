using AutoArsenal_App.Controllers;
using AutoArsenal_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DinkToPdf;

namespace AutoArsenal_App.Pages.Sales
{
    [Authorize(Roles = "Manager,Cashier")]
    public class SalesModel : PageModel
    {
        [BindProperty]
        public int DeleteId { get; set; }


        [BindProperty]
        public List<Sale> Sales { get; set; }

        [BindProperty]
        public List<Person> Persons { get; set; }

        [BindProperty]
        public List<double> remainings { get; set; }

        [BindProperty]
        public List<double> totals { get; set; }

        [BindProperty]
        public List<Payment> Payments { get; set; }

        [BindProperty]
        public List<PaymentDetails> PaymentDetails { get; set; }

        [BindProperty]
        public List<Lookup> Lookups { get; set; }

        
        public async void OnGet()
        {
            try
            {
                Sales = await SaleController.GetSales();
                Persons = await PersonController.GetPersons();
                Payments = await PaymentController.GetPayments();
                PaymentDetails = await PaymentDetailsController.GetPaymentDetails();
                Lookups = await LookupController.GetLookup();

                if (Sales.Count > 0)
                {
                    totals = new List<double>();
                    remainings = new List<double>();
                    foreach (var sale in Sales)
                    {
                        if (!sale.IsDeleted)
                        {
                            totals.Add(Payments.Find(p => p.ID == sale.PaymentID).TotalAmount);
                            List<PaymentDetails> pds = PaymentDetails.FindAll(pd => pd.PaymentID == sale.PaymentID);
                            remainings.Add(pds.Sum(p => p.PaidAmount));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
        }

        // Add Payments
        [BindProperty]
        public PaymentDetails pay { get; set; }
        
        public async Task<IActionResult> OnPostAddPayment()
        {
            try
            {
                if (pay.PaymentAccount == null)
                    pay.PaymentAccount = "Cash";
                pay.PaymentType = await LookupController.GetLookupId("Sale", "Type");
                pay.DateOfPayment = DateTime.Now;
                pay.PaymentMethod = await LookupController.GetLookupId("Cash", "Payment_Method");
                await PaymentDetailsController.AddPaymentDetails(pay);
            }
            catch (Exception ex)
            {
                TempData["ErrorOnServer"] = ex.Message;
            }
            return RedirectToPage("/Sales/Sales");
        }



        public async Task<IActionResult> OnPostDownloadPdf()
        {
            // Check if both sales and persons are available
            Sales = await SaleController.GetSales();
            Persons = await PersonController.GetPersons();

            if (Sales != null && Persons != null)
            {
                var htmlContent = "<table><thead><tr><th>Count</th><th>ID</th><th>Employee</th><th>Customer</th><th>Date of Sale</th><th>Remaining</th><th>Payment ID</th></tr></thead><tbody>";

                int count = 1;
                foreach (Models.Sale sale in Sales)
                {
                    if (!sale.IsDeleted)
                    {
                        Models.Person employee = Persons.Find(p => p.ID == sale.EmployeeID);
                        Models.Person customer = Persons.Find(p => p.ID == sale.CustomerID);

                        htmlContent += "<tr>";
                        htmlContent += $"<td>{count}</td>";
                        htmlContent += $"<td>{sale.ID}</td>";
                        htmlContent += $"<td>{employee.FirstName} {employee.LastName}</td>";
                        htmlContent += $"<td>{customer.FirstName} {customer.LastName}</td>";
                        htmlContent += $"<td>{sale.DateOfSale.ToString("yyyy-MM-dd hh:mm:ss")}</td>";
                        htmlContent += $"<td>{remainings[count - 1]}</td>";
                        htmlContent += $"<td>{sale.PaymentID}</td>";
                        htmlContent += "</tr>";
                        count++;
                    }
                }

                htmlContent += "</tbody></table>";

                var globalSettings = new GlobalSettings();
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlContent
                };

                var document = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                var converter = new SynchronizedConverter(new PdfTools());
                var pdfBytes = converter.Convert(document);

                return File(pdfBytes, "application/pdf", "SalesData.pdf");
            }
            else
            {
                return Content("No sales data available.");
            }
        }
    }
}

