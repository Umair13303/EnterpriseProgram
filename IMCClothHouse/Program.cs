using SharedUI.Global; // Ensure SharedUI is referenced in Dependencies

var builder = WebApplication.CreateBuilder(args);

// 1. Standard Services
builder.Services.AddControllersWithViews();

// 2. Services for your Shared Layout (SharedUI)
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<ISessionService, SessionService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // MUST be between UseRouting and UseAuthorization
app.UseAuthorization();

// 3. MAP THE AREA ROUTE (The missing piece for your 404)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// 4. Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();