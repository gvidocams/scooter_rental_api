using System;
using ScooterRental.Core.Models;

namespace ScooterRental.Core.Calculate
{
    public static class Income
    {
        public static decimal TotalRentalPrice(RentalDetail rentalDetail)
        {
            var startTime = rentalDetail.StartTime;
            var endTime = rentalDetail.EndTime.HasValue ? (DateTime)rentalDetail.EndTime : DateTime.Now;
            var pricePerMinute = rentalDetail.PricePerMinute;

            var rentedMinutes = (endTime - startTime).TotalMinutes;
            var rentedDays = (endTime - startTime).Days;

            const int MinutesInADay = 1440;

            decimal rentPrice = 0;

            if (rentedDays <= 0)
            {
                return Math.Round(RentPrice(rentedMinutes, pricePerMinute), 2);
            }

            var firstDayMinutes = MinutesInADay - startTime.TimeOfDay.TotalMinutes;

            rentPrice += RentPrice(firstDayMinutes, pricePerMinute);
            rentedMinutes -= firstDayMinutes;

            while (rentedMinutes > MinutesInADay)
            {
                rentPrice += RentPrice(firstDayMinutes, pricePerMinute);
                rentedMinutes -= MinutesInADay;
            }

            rentPrice += RentPrice(rentedMinutes, pricePerMinute);

            return Math.Round(rentPrice, 2);
        }

        public static decimal RentPrice(double minutes, decimal pricePerMinute)
        {
            const decimal RentPriceLimit = 20m;

            decimal rentPrice = pricePerMinute * (decimal)minutes;
            decimal priceForThisDay = rentPrice >= RentPriceLimit ? RentPriceLimit : rentPrice;

            return Math.Round(priceForThisDay, 2);
        }
    }
}