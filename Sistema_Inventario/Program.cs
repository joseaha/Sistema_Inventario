using Microsoft.EntityFrameworkCore;
using static Sistema_Inventario.Datos.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
