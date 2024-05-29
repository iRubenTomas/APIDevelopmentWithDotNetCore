using AutoMapper;
using CarInventory.Application.Commands.Car.Create;
using CarInventory.Application.Commands.Car.Update;
using CarInventory.Application.Dtos;
using CarInventory.Domain.Entities;

namespace CarInventory.Application.Mappings
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<CreateCarCommand, Car>();
            CreateMap<UpdateCarCommand, Car>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id if it's inherited and set manually
         
        }
    }
}
