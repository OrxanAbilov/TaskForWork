using Microsoft.EntityFrameworkCore;
using TaskForWork.Data;
using TaskForWork.Repositories;
using TaskForWork.Repositories.Implements;

var builder = WebApplication.CreateBuilder(args);
var connectionstring = builder.Configuration.GetConnectionString("DefaultConnString");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options=> options.UseSqlServer(connectionstring));
builder.Services.AddScoped<IItemRepository,ItemRepository>();
builder.Services.AddScoped<IInvoiceRepository,InvoiceRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Items}/{action=Index}/{id?}");
AppDbInitializer.Seed(app);
app.Run();
