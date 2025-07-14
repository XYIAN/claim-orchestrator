using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework Core with SQLite
builder.Services.AddDbContext<ClaimOrchestrator.Data.ClaimContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=claims.db"));

// Add custom services
builder.Services.AddScoped<ClaimOrchestrator.Services.IClaimProcessingService, ClaimOrchestrator.Services.ClaimProcessingService>();
builder.Services.AddScoped<ClaimOrchestrator.Services.IValidationService, ClaimOrchestrator.Services.ValidationService>();
builder.Services.AddScoped<ClaimOrchestrator.Services.IEligibilityService, ClaimOrchestrator.Services.EligibilityService>();

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
