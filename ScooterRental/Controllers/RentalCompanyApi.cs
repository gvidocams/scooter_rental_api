using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScooterRental.Core.CrossTables;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Core.Validations;
using ScooterRental.Models;

namespace ScooterRental.Controllers
{
    [Route("api/rental-company")]
    [ApiController]
    public class RentalCompanyApi : BaseScooterRentalController
    {
        private ICompanyService _companyService;
        private IEntityService<Scooter> _scooterEntityService;
        private CompanyValidator _companyValidator;
        private ScooterValidator _scooterValidator;

        public RentalCompanyApi(
            ICompanyService companyService,
            IEntityService<Scooter> scooterEntityService,
            CompanyValidator companyValidator,
            ScooterValidator scooterValidator,
            IMapper mapper) : base(mapper)
        {
            _companyService = companyService;
            _scooterEntityService = scooterEntityService;
            _companyValidator = companyValidator;
            _scooterValidator = scooterValidator;
        }

        [HttpPost]
        public IActionResult CreateRentalCompany(CompanyRequest request)
        {
            var company = _mapper.Map<RentalCompany>(request);

            if (!_companyValidator.Validate(company).IsValid)
            {
                return BadRequest();
            }

            if (_companyService.Exists(company))
            {
                return Conflict();
            }

            _companyService.Create(company);

            var response = _mapper.Map<CompanyResponse>(company);

            return Created("", response);
        }

        [HttpPost, Route("{companyId}")]
        public IActionResult AddScooter(int companyId, ScooterRequest request)
        {
            var scooter = _mapper.Map<Scooter>(request);

            if (!_scooterValidator.Validate(scooter).IsValid)
            {
                return BadRequest();
            }

            if (!_companyService.Exists(companyId))
            {
                return NotFound($"Company with id:{companyId} doesn't exist!");
            }

            scooter = _companyService.Create(companyId, scooter);
            
            var response = _mapper.Map<ScooterResponse>(scooter);

            return Created("", response);
        }

        [HttpPatch, Route("{companyId}/scooter/{scooterId}/start-rent/")]
        public IActionResult StartRent(int companyId, int scooterId)
        {
            var company = _companyService.GetById(companyId);
            var scooter = _scooterEntityService.GetById(scooterId);

            if (scooter == null ||
                company == null)
            {
                return NotFound();
            }

            if (scooter.IsRented)
            {
                return Conflict();
            }

            _companyService.StartRent(company, scooter);

            var response = _mapper.Map<ScooterResponse>(scooter);

            return Ok(response);
        }

        [HttpPatch, Route("scooter/{scooterId}/end-rent")]
        public IActionResult EndRent(int scooterId)
        {
            var scooter = _scooterEntityService.GetById(scooterId);
            
            if (scooter == null)
            {
                return NotFound();
            }

            if (!scooter.IsRented)
            {
                return Conflict();
            }

            var income = _companyService.EndRent(scooter);

            return Ok(income);
        }

        [HttpPost, Route("{id}/income")]
        public IActionResult CalculateIncome(int id, IncomeReportRequest request)
        {
            var income = _companyService.CalculateIncome(id, request);

            return Ok(income);
        }
    }
}
