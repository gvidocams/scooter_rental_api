using FluentValidation;
using ScooterRental.Core.Models;

namespace ScooterRental.Core.Validations
{
    public class CompanyValidator : AbstractValidator<RentalCompany>
    {
        public CompanyValidator()
        {
            RuleFor(company => company.Name).NotEmpty().NotNull();
        }
    }
}