using Microsoft.AspNetCore.Authentication.Cookies;
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

builder.Services.AddHttpClient<IIncomeService, IncomeService>();
builder.Services.AddHttpClient<IOutcomeService, OutcomeService>();
builder.Services.AddHttpClient<IFixedExpenseService, FixedExpenseService>();
builder.Services.AddHttpClient<IDailyReportService, DailyReportService>();
builder.Services.AddHttpClient<IFinalReportService, FinalReportService>();
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
