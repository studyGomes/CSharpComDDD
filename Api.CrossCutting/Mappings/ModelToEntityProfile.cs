using Api.Domain.Entities;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            // Convert Source -> Destino
            CreateMap<UserEntity            , UserModel>()  .ReverseMap();
           
        }
    }
}
