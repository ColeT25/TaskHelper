using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskHelperWebApp.Data;
var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()){
    builder.Services.AddDbContext<TaskContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DEVTaskContext") ?? throw new InvalidOperationException("Connection string 'DEVTaskContext' not found.")));
    }
else{
    builder.Services.AddDbContext<TaskContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TaskContext") ?? throw new InvalidOperationException("Connection string 'TaskContext' not found.")));
}


// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
