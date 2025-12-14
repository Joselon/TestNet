using Microsoft.EntityFrameworkCore;
using TestWebBlazorServer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["data:conxstring"];
builder.Services.AddDbContext<TodoContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddTransient<ITodoService, TodoService>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseRouting();

app.MapBlazorHub();
app.MapRazorPages().WithStaticAssets();
app.MapFallbackToPage("/_Host");

app.Run();
