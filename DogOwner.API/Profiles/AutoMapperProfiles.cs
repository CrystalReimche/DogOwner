using AutoMapper;
using DogOwner.API.Models.Domain;
using DogOwner.API.Models.DTO;

namespace DogOwner.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        { 
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Owner, AddOwnerDto>().ReverseMap();
            CreateMap<Owner, UpdateOwnerDto>().ReverseMap();

            CreateMap<Dog, DogDto>().ReverseMap();
            CreateMap<Dog, AddDogDto>().ReverseMap();
            CreateMap<Dog, UpdateDogDto>().ReverseMap();
        }
    }
}
