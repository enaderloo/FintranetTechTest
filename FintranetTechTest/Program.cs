using FintranetTechTest.Abstractions.Domain;
using FintranetTechTest.Application.Services;
using FintranetTechTest.Domain.Entities;
using FintranetTechTest.Domain.Enums;
using FintranetTechTest.Domain.Models;
using FintranetTechTest.Domain.Providers;
using FintranetTechTest.Domain.Repositories;
using FintranetTechTest.Infrastructure.EF.Contexts;
using FintranetTechTest.Infrastructure.EF.Repositories;
using FintranetTechTest.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CongestionTaxDbContext>(x => x.UseInMemoryDatabase("InMemoryCongestionTaxDatabase"));
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();


builder.Services.AddScoped<ICongestionTaxCalculatorService, CongestionTaxCalculatorService>();
builder.Services.AddScoped<ITaxRules, GothenburgTaxRule>();
builder.Services.AddScoped<IHoliday, HolidayChecker>();

var vehicleRepository = new VehicleRepository();
var taxRulesProvider = new GothenburgTaxRule();
var holidayChecker = new HolidayChecker();
var congestionTaxCalculatorService = new CongestionTaxCalculatorService(
    taxRulesProvider, vehicleRepository, holidayChecker);

List<string> stringDateList = new()
{
    "2013-01-14 21:00:00",
    "2013-01-15 21:00:00",
    "2013-02-07 06:23:27",
    "2013-02-07 15:27:00",
    "2013-02-08 06:27:00",
    "2013-02-08 06:20:27",
    "2013-02-08 14:35:00",
    "2013-02-08 15:29:00",
    "2013-02-08 15:47:00",
    "2013-02-08 16:01:00",
    "2013-02-08 16:48:00",
    "2013-02-08 17:49:00",
    "2013-02-08 18:29:00",
    "2013-02-08 18:35:00",
    "2013-03-26 14:25:00",
    "2013-03-28 14:07:27"
};

List<DateTime> dateTimeList = new List<DateTime>();
foreach (string dateString in stringDateList)
{
    if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null,
        System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
    {
        dateTimeList.Add(parsedDate);
    }
}


var vehicle = new Vehicle(1,VehicleType.Car);


var congestionTaxCalculationInput = new CongestionTaxCalculationInput { Dates = dateTimeList, Vehicle = vehicle };
var result = congestionTaxCalculatorService.CalculateCongestionTax(congestionTaxCalculationInput);

Console.WriteLine(result);
Console.ReadKey();



//var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
//app.Run();
