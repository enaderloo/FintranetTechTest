

using FintranetTechTest.Abstractions.Domain;
using FintranetTechTest.Application.Services;
using FintranetTechTest.Domain.Entities;
using FintranetTechTest.Domain.Enums;
using FintranetTechTest.Domain.Models;
using FintranetTechTest.Domain.Providers;
using FintranetTechTest.Domain.Repositories;
using FintranetTechTest.Infrastructure.EF.Repositories;
using FintranetTechTest.Infrastructure.Services;
using NSubstitute;
using Shouldly;

namespace FintranetTechTest.UnitTests.Application
{
    public class CongestionTaxCalculatorServiceTests
    {
        #region ARRANGE

        private readonly IHoliday _holiday;
        private readonly GothenburgTaxRule _taxRules;
        private readonly IVehicleRepository _repository;
        private readonly CongestionTaxCalculatorService congestionTaxCalculatorService;


        public CongestionTaxCalculatorServiceTests()
        {
            _holiday = Substitute.For<IHoliday>();
            _taxRules = Substitute.For<GothenburgTaxRule>();
            _repository = Substitute.For<IVehicleRepository>();
            congestionTaxCalculatorService = new CongestionTaxCalculatorService(_taxRules, _repository, _holiday);
        }

        #endregion


        [Fact]
        public void Tax_Must_Greater_Than_Zero()
        {

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

            List<DateTime> dateTimeList = new();
            foreach (string dateString in stringDateList)
            {
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateTimeList.Add(parsedDate);
                }
            }


            var vehicle = new Vehicle(1, VehicleType.Car);


            var congestionTaxCalculationInput = new CongestionTaxCalculationInput { Dates = dateTimeList, Vehicle = vehicle };
            var result = congestionTaxCalculatorService.CalculateCongestionTax(congestionTaxCalculationInput);

            // Assert
            result.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void Tax_Must_Be_Zero_When_Dates_Are_Weekend()
        {
            // ARRANGE
            List<string> stringDateList = new()
            {
               
                "2013-01-05 08:24:00"
            };

            List<DateTime> dateTimeList = new();
            foreach (string dateString in stringDateList)
            {
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateTimeList.Add(parsedDate);
                }
            }

            var vehicle = new Vehicle(1, VehicleType.Car);
            var congestionTaxCalculationInput = new CongestionTaxCalculationInput { Dates = dateTimeList, Vehicle = vehicle };

            // ACT
            var result = congestionTaxCalculatorService.CalculateCongestionTax(congestionTaxCalculationInput);

            // Assert
            result.ShouldBe(0);
        }

        [Fact]
        public void Tax_Must_Be_Zero_When_Vehicle_Is_Exempted()
        {
            // ARRANGE
            List<string> stringDateList = new()
            {
                "2013-01-10 08:24:00"
            };

            List<DateTime> dateTimeList = new();
            foreach (string dateString in stringDateList)
            {
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateTimeList.Add(parsedDate);
                }
            }

            var vehicle = new Vehicle(1, VehicleType.Diplomat);
            var congestionTaxCalculationInput = new CongestionTaxCalculationInput { Dates = dateTimeList, Vehicle = vehicle };

            // ACT
            var result = congestionTaxCalculatorService.CalculateCongestionTax(congestionTaxCalculationInput);

            // Assert
            result.ShouldBe(0);
        }

        [Fact]
        public void Return_60_SEK_Tax_When_Vehicle_That_Passes_Several_Tolling_Stations_Per_Day()
        {
            // ARRANGE
            List<string> stringDateList = new()
            {
                "2013-02-08 06:27:00",
                "2013-02-08 06:20:27",
                "2013-02-08 14:35:00",
                "2013-02-08 15:29:00",
                "2013-02-08 15:47:00",
                "2013-02-08 16:01:00",
                "2013-02-08 16:48:00",
                "2013-02-08 17:49:00",
                "2013-02-08 18:29:00",
                "2013-02-08 18:35:00"
            };

            List<DateTime> dateTimeList = new();
            foreach (string dateString in stringDateList)
            {
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateTimeList.Add(parsedDate);
                }
            }

            var vehicle = new Vehicle(1, VehicleType.Car);
            var congestionTaxCalculationInput = new CongestionTaxCalculationInput { Dates = dateTimeList, Vehicle = vehicle };

            // ACT
            var result = congestionTaxCalculatorService.CalculateCongestionTax(congestionTaxCalculationInput);

            // Assert
            result.ShouldBe(60);
        }

        [Fact]
        public void Return_More_Than_60_SEK_Tax_When_Vehicle_That_Passes_Several_Tolling_Stations_On_Different_Days()
        {
            // ARRANGE
            List<string> stringDateList = new()
            {
                "2013-02-08 06:27:00",

                "2013-02-09 06:20:27", // Weekend
                "2013-02-10 14:35:00", // Weekend

                "2013-02-11 15:29:00",
                "2013-02-12 15:47:00",
                "2013-02-13 16:01:00",
                "2013-02-14 16:48:00",
                "2013-02-15 17:49:00",

                "2013-02-16 18:29:00",// Weekend
                "2013-02-17 18:35:00" // Weekend
            };

            List<DateTime> dateTimeList = new();
            foreach (string dateString in stringDateList)
            {
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateTimeList.Add(parsedDate);
                }
            }

            var vehicle = new Vehicle(1, VehicleType.Car);
            var congestionTaxCalculationInput = new CongestionTaxCalculationInput { Dates = dateTimeList, Vehicle = vehicle };

            // ACT
            var result = congestionTaxCalculatorService.CalculateCongestionTax(congestionTaxCalculationInput);

            // Assert
            result.ShouldBe(60);
        }

    }
}
