using System;
using Api.CrossCutting.Mappings;
using AutoMapper;
using Xunit;

namespace Api.Service.Test
{
    public abstract class BaseTestService
    {
        public IMapper Mapper{ get; set;}
        public BaseTestService()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration( config =>
                {
                    config.AddProfile(new ModelToEntityProfile());
                    config.AddProfile(new DTOToModelProfile());
                    config.AddProfile(new EntityToDTOProfile());
                });
                return config.CreateMapper();
            }

            public void Dispose()
            {

            }
        }


    }
}
