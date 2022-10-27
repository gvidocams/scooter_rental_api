using System;
using System.Collections.Generic;
using ScooterRental.Core.Models;
using ScooterRental.Models;

namespace ScooterRental.Core.Calculate
{
    public class Total
    {
        public static decimal TotalIncome(List<RentalDetail> rentalDetails, IncomeReportRequest report)
        {
            var includeNotCompletedRentals = report.IncludeNotCompletedRentals;
            var year = report.year;

            decimal income = 0;

            if (year.HasValue)
            {
                foreach (var rentalDetail in rentalDetails)
                {
                    if ((rentalDetail.EndTime.HasValue && rentalDetail.EndTime.Value.Year == year) ||
                        (includeNotCompletedRentals && DateTime.Now.Year == year))
                    {
                        income += Income.TotalRentalPrice(rentalDetail);
                    }
                }
            }
            else
            {
                foreach (var rentalDetail in rentalDetails)
                {
                    if (rentalDetail.EndTime.HasValue || includeNotCompletedRentals)
                    {
                        income += Income.TotalRentalPrice(rentalDetail);
                    }
                }
            }

            return income;
        }
    }
}