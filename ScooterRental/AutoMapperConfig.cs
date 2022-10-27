using AutoMapper;
using ScooterRental.Core.Models;
using ScooterRental.Models;

namespace ScooterRental
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<CompanyRequest, RentalCompany>()
                        .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CompanyName));
                    cfg.CreateMap<RentalCompany, CompanyResponse>()
                        .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Name));

                    cfg.CreateMap<ScooterRequest, Scooter>();
                    cfg.CreateMap<Scooter, ScooterResponse>();
                });

            return config.CreateMapper();
        }
    }
}