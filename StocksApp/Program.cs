using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using Rotativa.AspNetCore;
using Serilog;
using ServiceContracts;
using ServiceContracts.FinnhubService;
using Services.FinnhubService;
using Services.StocksService;
using StocksApp;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {


loggerConfiguration.ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
 .ReadFrom.Services(services); //read out current app's services and make them available to serilog
});

//Services
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddTransient<IBuyOrdersService, BuyOrdersService>();
builder.Services.AddTransient<ISellOrdersService, SellOrdersService>();
builder.Services.AddTransient<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
builder.Services.AddTransient<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
builder.Services.AddTransient<IFinnhubStocksService, FinnhubStocksService>();
builder.Services.AddTransient<IFinnhubSearchStocksService, FinnhubSearchStocksService>();
builder.Services.AddTransient<IStocksRepository, StocksRepository>();
builder.Services.AddTransient<IFinnhubRepository, FinnhubRepository>();

if (!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

//builder.Services.AddHttpLogging(options =>
//{
//    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
//});

builder.Services.AddHttpClient();
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

if (builder.Environment.IsEnvironment("Test") == false)
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");


//app.UseHttpLogging();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();


public partial class Program { } //make the auto-generated Program accessible programmatically