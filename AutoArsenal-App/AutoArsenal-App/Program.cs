using AutoArsenal_App.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("Cashier", policy => policy.RequireRole("Cashier"));
    // Add more roles and policies as needed
});

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

CredentialController.Initialize(builder.Configuration);
LookupController.Initialize(builder.Configuration);

CustomerController.Initialize(builder.Configuration);
EmployeeController.Initialize(builder.Configuration);
PersonController.Initialize(builder.Configuration);

ProductController.Initialize(builder.Configuration);
ProductCategoryController.Initialize(builder.Configuration);

InventoryController.Initialize(builder.Configuration);
ManufacturerController.Initialize(builder.Configuration);
WarehouseController.Initialize(builder.Configuration);

SaleController.Initialize(builder.Configuration);
SaleDetailsController.Initialize(builder.Configuration);

PurchaseController.Initialize(builder.Configuration);
PurchaseDetailsController.Initialize(builder.Configuration);

PaymentController.Initialize(builder.Configuration);
PaymentDetailsController.Initialize(builder.Configuration);

ReturnController.Initialize(builder.Configuration);
ReturnDetailsController.Initialize(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
