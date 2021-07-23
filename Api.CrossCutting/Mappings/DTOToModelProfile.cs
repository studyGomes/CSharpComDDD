using Api.Domain.DTOs.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DTOToModelProfile : Profile
    {
        public DTOToModelProfile()
        {
            // Convert Source -> Destino
            CreateMap<UserModel             , UserDTO>()        .ReverseMap();
            CreateMap<UserModel             , UserDTOCreate>()  .ReverseMap();
            CreateMap<UserModel             , UserDTOUpdate>()  .ReverseMap();
        }
    }
}
