using FluentValidation;
using ScooterRental.Core.Models;

namespace ScooterRental.Core.Validations
{
    public class ScooterValidator : AbstractValidator<Scooter>
    {
        public ScooterValidator()
        {
            RuleFor(scooter => scooter.PricePerMinute).NotNull().GreaterThan(0);
        }
    }
}