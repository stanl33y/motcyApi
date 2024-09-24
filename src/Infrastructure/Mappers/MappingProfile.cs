using AutoMapper;
using motcyApi.Application.DTOs;
using motcyApi.Domain.Entities;

namespace motcyApi.Infrastructure.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MotorcycleCreateDTO, Motorcycle>();
        CreateMap<DeliveryPersonRegisterDTO, DeliveryPerson>();
        CreateMap<RentalCreateDTO, Rental>();
        CreateMap<Motorcycle, MotorcycleDTO>();
        CreateMap<DeliveryPerson, DeliveryPersonDTO>();
        CreateMap<Rental, RentalDTO>();
    }
}
