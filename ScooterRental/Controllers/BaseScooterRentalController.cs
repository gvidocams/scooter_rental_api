using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScooterRental.Controllers
{
    [ApiController]
    public abstract class BaseScooterRentalController : ControllerBase
    {
        protected IMapper _mapper;

        protected BaseScooterRentalController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
