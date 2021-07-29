using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
       private UsersController _controller;
        [Fact(DisplayName="É Possível Realizar o BADREQUEST.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUserService>();
            var nome        = Faker.Name.FullName();
            var email       = Faker.Internet.Email();

            serviceMock.Setup(m=>m.GetAll()).ReturnsAsync(
                new List<UserDTO>
                {
                    new UserDTO
                    {
                        Id      = Guid.NewGuid(),
                        Name    = Faker.Name.FullName(),
                        Email   = Faker.Internet.Email(),
                        CreateAt= DateTime.UtcNow
                    },
                    new UserDTO
                    {
                        Id      = Guid.NewGuid(),
                        Name    = Faker.Name.FullName(),
                        Email   = Faker.Internet.Email(),
                        CreateAt= DateTime.UtcNow
                    },                    
                }
            );


            _controller = new UsersController(serviceMock.Object);
            // ADICIONAR Model State ERROR
            _controller.ModelState.AddModelError("Name","É um campo Obrigatório");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);

            //var resultValue = ((OkObjectResult) result).Value as IEnumerable<UserDTO>;
            //Assert.True(resultValue.Count()==2);

        }        
    }
}
