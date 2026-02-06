using OrganisationSetup.Models.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection; // Required for sharing login

var builder = WebApplication.CreateBuilder(args);

// 1. Connection String
builder.Services.AddDbContext<IMSOrganisationSetupContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IMSSharedConnection")));

// 2. DATA PROTECTION (The "Secret Sauce" for shared login)
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<IMSOrganisationSetupContext>()
    .SetApplicationName("SharedERPApp"); // MUST match the name in OrganisationSetup

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISessionService, SessionService>();

// 3. SHARED SESSION CONFIG
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    // IMPORTANT: This Name must be EXACTLY the same in both projects
    options.Cookie.Name = ".SharedERP.Session";
    options.Cookie.Path = "/";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// UseSession MUST come after UseRouting and before UseAuthorization
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=OSUAuthentication}/{action=OSULogin}/{id?}",
    defaults: new { area = "OSAUser" });

app.Run();