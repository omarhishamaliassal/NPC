using Microsoft.EntityFrameworkCore;
using WebApplication7.Data; // Make sure this matches your actual namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register your DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add distributed memory cache for session state
builder.Services.AddDistributedMemoryCache();

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Serve CSS, JS, images, etc.

app.UseRouting();

// Add session middleware here before UseAuthorization
app.UseSession();

app.UseAuthorization();

// Set default route to AboutController's About action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=About}/{action=About}/{id?}");

// Enable attribute routing for API controllers if any
app.MapControllers();

app.Run();
