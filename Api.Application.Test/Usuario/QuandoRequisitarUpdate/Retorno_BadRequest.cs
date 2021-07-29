using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {

        private UsersController _controller;
        [Fact(DisplayName="É Possível Realizar o BADREQUEST.")]
        public async Task E_Possivel_Invocar_a_Controller_BadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            var nome        = Faker.Name.FullName();
            var email       = Faker.Internet.Email();

            serviceMock.Setup(m=>m.Put(It.IsAny<UserDTOUpdate>())).ReturnsAsync(
                new UserDTOUpdateResult
                {
                    Id      = Guid.NewGuid(),
                    Name    = nome,
                    Email   = email,
                    UpdateAt= DateTime.UtcNow
                }
            );


            _controller = new UsersController(serviceMock.Object);
            // ADICIONAR Model State ERROR
            _controller.ModelState.AddModelError("Email","É um campo Obrigatório");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x=>x.Link(It.IsAny<string>(),It.IsAny<object>())).Returns("http://localhost:5000/");
            _controller.Url = url.Object;

            var userDTOUpdate = new UserDTOUpdate
            {
                Id      = Guid.NewGuid(),
                Name    = nome,
                Email   = email,
            };

            var result = await _controller.Put(userDTOUpdate);
            Assert.True(result is BadRequestObjectResult);
/*
            var resultValue = ((OkObjectResult) result).Value as UserDTOUpdateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(userDTOUpdate.Name ,resultValue.Name);
            Assert.Equal(userDTOUpdate.Email,resultValue.Email);
*/
        }        
    }
}
