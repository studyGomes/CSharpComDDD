using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            // Convert Source -> Destino
            CreateMap<UserDTO               , UserEntity>() .ReverseMap();
            CreateMap<UserDTOCreateResult   , UserEntity>() .ReverseMap();
            CreateMap<UserDTOUpdateResult   , UserEntity>() .ReverseMap();

        }
    }
}
