using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestaurantManagement.CatalogMicroservice.Data;
using RestaurantManagement.CatalogMicroservice.Entities.User;
using RestaurantManagement.CatalogMicroservice.Services.DailyReportService;
using RestaurantManagement.CatalogMicroservice.Services.FinalReportService;
using RestaurantManagement.CatalogMicroservice.Services.FixedExpenseService;
using RestaurantManagement.CatalogMicroservice.Services.IncomeService;
using RestaurantManagement.CatalogMicroservice.Services.OutComeService;
using RestaurantManagement.CatalogMicroservice.Services.UserService;
using RestaurantManagement.CatalogMicroservice.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
provider.Mappings[".json"] = "application/json";

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddCors();

builder.Services.AddHttpContextAccessor();



// DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);


var databaseSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();


builder.Services.AddSingleton<MongoDB.Driver.IMongoClient>(sp =>
    new MongoDB.Driver.MongoClient(databaseSettings.ConnectionString));

// Services
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IOutcomeService, OutcomeService>();
builder.Services.AddScoped<IFixedExpenseService, FixedExpenseService>();
builder.Services.AddScoped<IDailyReportService, DailyReportService>();
builder.Services.AddScoped<IFinalReportService, FinalReportService>();
builder.Services.AddScoped<TokenService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureMongoDbIdentity<AppUser, AppRole, Guid>(new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = databaseSettings.ConnectionString,   
        DatabaseName = databaseSettings.DatabaseName
    },
    IdentityOptionsAction = opt =>
    {

        opt.Password.RequiredLength = 6;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireDigit = false;
        opt.User.RequireUniqueEmail = true;
    }
});


var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials()
       .WithOrigins("http://localhost:7260", "http://localhost:7260"); 
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();


app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await SeedDatabase.Initialize(app);

app.MapFallbackToFile("/index.html");

app.Run();
