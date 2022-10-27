using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRental.Core.Calculate;
using ScooterRental.Core.Models;
using ScooterRental.Models;

namespace ScooterRental.Tests
{
    [TestClass]
    public class TotalIncomeTests
    {
        private List<RentalDetail> _rentalDetails;
        private IncomeReportRequest _request;


        [TestInitialize]
        public void Setup()
        {
            _rentalDetails = new List<RentalDetail>();
            _request = new IncomeReportRequest();
        }

        [TestMethod]
        public void CalculateIncome_ShouldCalculateIncomeFromOneScooterRent()
        {
            // Arrange
            _rentalDetails.Add(
                new RentalDetail
                {
                    StartTime = DateTime.Now.AddMinutes(-2),
                    EndTime = DateTime.Now,
                    PricePerMinute = 0.2m
                }
            );

            _request.IncludeNotCompletedRentals = false;

            // Act
            var result = Total.TotalIncome(_rentalDetails, _request);

            // Assert
            result.Should().Be(0.4m);
        }
        
        [TestMethod]
        public void CalculateIncome_ShouldCalculateIncomeFromTwoEndedScooterReports()
        {
            // Arrange
            var rentalDetail = new RentalDetail
            {
                StartTime = DateTime.Now.AddMinutes(-4),
                EndTime = DateTime.Now.AddMinutes(-2),
                PricePerMinute = 0.2m
            };

            _rentalDetails.Add(rentalDetail);
            _rentalDetails.Add(rentalDetail);

            _request.IncludeNotCompletedRentals = false;

            // Act
            var result = Total.TotalIncome(_rentalDetails, _request);

            // Assert
            result.Should().Be(0.8m);
        }
        
        [TestMethod]
        public void CalculateIncome_CalculatesIncomeOnlyFromYear2022()
        {
            // Arrange
            var rentalDetail1 = new RentalDetail
            {
                StartTime = new DateTime(2022, 1, 1, 0, 0, 0),
                EndTime = new DateTime(2022, 1, 1, 0, 2, 0),
                PricePerMinute = 0.2m
            };

            _rentalDetails.Add(rentalDetail1);

            var rentalDetail2 = new RentalDetail
            {
                StartTime = new DateTime(2021, 1, 1, 0, 0, 0),
                EndTime = new DateTime(2021, 1, 2, 0, 2, 0),
                PricePerMinute = 0.2m
            };
            
            _rentalDetails.Add(rentalDetail2);

            var rentalDetail3 = new RentalDetail
            {
                StartTime = new DateTime(2021, 1, 1, 0, 0, 0),
                PricePerMinute = 0.2m
            };
            
            _rentalDetails.Add(rentalDetail3);

            _request.year = 2022;
            _request.IncludeNotCompletedRentals = false;

            // Act
            var result = Total.TotalIncome(_rentalDetails, _request);

            // Assert
            result.Should().Be(0.4m);
        }
        
        [TestMethod]
        public void CalculateIncome_IncludeAllReportsAndNotCompletedRentals()
        {
            // Arrange

            var rentalDetail1 = new RentalDetail
            {
                StartTime = DateTime.Now.AddMinutes(-2),
                PricePerMinute = 0.2m
            };

            var rentalDetail2 = new RentalDetail
            {
                StartTime = new DateTime(2021, 1, 1, 0, 0, 0),
                EndTime = new DateTime(2021, 1, 1, 0, 2, 0),
                PricePerMinute = 0.2m
            };

            _rentalDetails.Add(rentalDetail1);
            _rentalDetails.Add(rentalDetail2);

            _request.IncludeNotCompletedRentals = true;

            // Act
            var result = Total.TotalIncome(_rentalDetails, _request);

            // Assert
            result.Should().Be(0.8m);
        }
        
        [TestMethod]
        public void CalculateIncome_IncludeAllReportsAndNotCompletedRentalsYear2021()
        {
            // Arrange

            var rentalDetail1 = new RentalDetail
            {
                StartTime = new DateTime(2021, 1, 1, 0, 0, 0),
                PricePerMinute = 0.2m
            };

            var rentalDetail2 = new RentalDetail
            {
                StartTime = new DateTime(2021, 1, 1, 0, 0, 0),
                EndTime = new DateTime(2021, 1, 1, 0, 2, 0),
                PricePerMinute = 0.2m
            };
            
            _rentalDetails.Add(rentalDetail1);
            _rentalDetails.Add(rentalDetail2);

            _request.year = 2021;
            _request.IncludeNotCompletedRentals = true;

            // Act
            var result = Total.TotalIncome(_rentalDetails, _request);

            // Assert
            result.Should().Be(0.4m);
        }
        
    }
}