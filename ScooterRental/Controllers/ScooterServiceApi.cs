using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Models;

namespace ScooterRental.Controllers
{
    [Route("api/scooter")]
    [ApiController]
    public class ScooterServiceApi : BaseScooterRentalController
    {
        private IEntityService<Scooter> _scooterService;

        public ScooterServiceApi(
            IEntityService<Scooter> scooterService,
            IMapper mapper) : base(mapper)
        {
            _scooterService = scooterService;
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetScooter(int id)
        {
            var scooter = _scooterService.GetById(id);

            if (scooter == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ScooterResponse>(scooter);

            return Ok(response);
        }

        [HttpDelete, Route("{id}")]
        public IActionResult DeleteScooter(int id)
        {
            var scooter = _scooterService.GetById(id);

            if (scooter == null)
            {
                return NotFound();
            }

            if (scooter.IsRented)
            {
                return Conflict();
            }

            _scooterService.Delete(scooter);

            return Ok();
        }
    }
}
