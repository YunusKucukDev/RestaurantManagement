using Microsoft.AspNetCore.Authentication.Cookies;
using RestaurantManagement.WebUI.Services.AccountService;
using RestaurantManagement.WebUI.Services.DailyReportService;
using RestaurantManagement.WebUI.Services.FinalReportService;
using RestaurantManagement.WebUI.Services.FixedExpense;
using RestaurantManagement.WebUI.Services.IncomeService;
using RestaurantManagement.WebUI.Services.OutcomeService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Login/Index"; // Giriş yapmamışsa buraya at
        opt.AccessDeniedPath = "/Error/Index";
        opt.Cookie.Name = "RestaurantUserCookie";
        opt.ExpireTimeSpan = TimeSpan.FromHours(3); // Cookie'nin geçerlilik süresi
    });

builder.Services.AddSession(); // Token'ı geçici saklamak için

// Appsettings'den URL'i bir kez oku
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

builder.Services.AddHttpClient<IIncomeService, IncomeService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IOutcomeService, OutcomeService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IFixedExpenseService, FixedExpenseService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IDailyReportService, DailyReportService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IFinalReportService, FinalReportService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddSession(); // Builder taraf�na

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
