using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRental.Core.Models;
using ScooterRental.Core.Validations;

namespace ScooterRental.Tests
{
    [TestClass]
    public class ScooterValidatorTests
    {
        private Scooter _scooter;
        private ScooterValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _scooter = new Scooter();
            _validator = new ScooterValidator();
        }

        [TestMethod]
        public void PricePerMinute_Passes0_ReturnsFalse()
        {
            _scooter.PricePerMinute = 0;

            _validator.Validate(_scooter).IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Name_Passes1_ReturnsTrue()
        {
            _scooter.PricePerMinute = 1;

            _validator.Validate(_scooter).IsValid.Should().BeTrue();
        }
    }
}