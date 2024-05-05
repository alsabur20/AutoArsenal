using AutoArsenal_App.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
