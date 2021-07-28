using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName="É Póssivel Mapear os Modelos")]
        public void E_Possivel_Mapear_os_Modelos()
        {
            var model = new UserModel
            {
                Id      = Guid.NewGuid(),
                Name    = Faker.Name.FullName(),
                Email   = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
            };

            var listaEntity = new List<UserEntity>();
            for (int i = 0; i < 10; i++)
            {
                var item = new UserEntity
                {
                    Id      = Guid.NewGuid(),
                    Name    = Faker.Name.FullName(),
                    Email   = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                }; 
                listaEntity.Add(item);               
            }


            // Model  => Entity
            var entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(entity.Id         , model.Id);
            Assert.Equal(entity.Name       , model.Name);
            Assert.Equal(entity.Email      , model.Email);
            Assert.Equal(entity.CreateAt   , model.CreateAt);
            Assert.Equal(entity.UpdateAt   , model.UpdateAt);

            // Entity => UserDTO
            var userDTO = Mapper.Map<UserDTO>(entity);
            Assert.Equal(userDTO.Id         , entity.Id);
            Assert.Equal(userDTO.Name       , entity.Name);
            Assert.Equal(userDTO.Email      , entity.Email);
            Assert.Equal(userDTO.CreateAt   , entity.CreateAt);
            //Assert.Equal(userDTO.UpdateAt   , entity.UpdateAt);

            var listDTO = Mapper.Map<List<UserDTO>>(listaEntity);
            Assert.True(listDTO.Count()==listaEntity.Count());
            for (int i = 0; i < listDTO.Count() ; i++)
            {
                Assert.Equal(listDTO[i].Id         , listaEntity[i].Id);
                Assert.Equal(listDTO[i].Name       , listaEntity[i].Name);
                Assert.Equal(listDTO[i].Email      , listaEntity[i].Email);
                               
            }

            var userDTOCreateResult = Mapper.Map<UserDTOCreateResult>(entity);
            Assert.Equal(userDTOCreateResult.Id         , entity.Id);
            Assert.Equal(userDTOCreateResult.Name       , entity.Name);
            Assert.Equal(userDTOCreateResult.Email      , entity.Email);
            Assert.Equal(userDTOCreateResult.CreateAt   , entity.CreateAt);            

            var userDTOUpdateResult = Mapper.Map<UserDTOUpdateResult>(entity);
            Assert.Equal(userDTOUpdateResult.Id         , entity.Id);
            Assert.Equal(userDTOUpdateResult.Name       , entity.Name);
            Assert.Equal(userDTOUpdateResult.Email      , entity.Email);
            Assert.Equal(userDTOUpdateResult.UpdateAt   , entity.UpdateAt);    

            // DTO para Model
            var userModel = Mapper.Map<UserModel>(userDTO);
            Assert.Equal(userModel.Id         , userDTO.Id);
            Assert.Equal(userModel.Name       , userDTO.Name);
            Assert.Equal(userModel.Email      , userDTO.Email);
            Assert.Equal(userModel.CreateAt   , userDTO.CreateAt);
            //Assert.Equal(userModel.UpdateAt   , model.UpdateAt);            

            var userDTOCreate = Mapper.Map<UserDTOCreate>(userModel);
            Assert.Equal(userDTOCreate.Name       , userModel.Name);
            Assert.Equal(userDTOCreate.Email      , userModel.Email);
            
            var userDTOUpdate = Mapper.Map<UserDTOUpdate>(userModel);
            Assert.Equal(userDTOUpdate.Id         , userModel.Id);
            Assert.Equal(userDTOUpdate.Name       , userModel.Name);
            Assert.Equal(userDTOUpdate.Email      , userModel.Email);

        }
    }
}
