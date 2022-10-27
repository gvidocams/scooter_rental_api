using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRental.Core.Models;
using ScooterRental.Core.Validations;

namespace ScooterRental.Tests
{
    [TestClass]
    public class CompanyValidator_Tests
    {
        private RentalCompany _company;
        private CompanyValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new CompanyValidator();
            _company = new RentalCompany();
        }

        [TestMethod]
        public void Name_PassesNullValue_ReturnsFalse()
        {
            _company.Name = null;

            _validator.Validate(_company).IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Name_PassesEmptyValue_ReturnsFalse()
        {
            _company.Name = "";

            _validator.Validate(_company).IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Name_PassesValidValue_ReturnsTrue()
        {
            _company.Name = "If";

            _validator.Validate(_company).IsValid.Should().BeTrue();
        }
    }
}