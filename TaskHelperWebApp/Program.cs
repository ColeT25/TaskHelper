using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp.Data;
using TaskHelperWebApp.Services;
using TaskHelperWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore.InMemory;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()){
    builder.Services.AddDbContext<TasksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TasksContext") ?? throw new InvalidOperationException("Connection string 'TasksContext' not found.")));
}
else if (builder.Environment.IsProduction()){
    builder.Services.AddDbContext<TasksContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("SQLCONNSTR_TaskHelperDB") ?? throw new InvalidOperationException("Connection String Could Not Be Retrieved From Azure Environment Variable for TasksContext")));
}

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adding services for business logic
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IBoardsService, BoardsService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();

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
