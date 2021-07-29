using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGet
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

            serviceMock.Setup(m=>m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UserDTO
                {
                    Id      = Guid.NewGuid(),
                    Name    = nome,
                    Email   = email,
                    CreateAt= DateTime.UtcNow
                }
            );


            _controller = new UsersController(serviceMock.Object);
            // ADICIONAR Model State ERROR
            _controller.ModelState.AddModelError("Id","É um campo Obrigatório");

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
/*
            var resultValue = ((CreatedResult) result).Value as UserDTO;
            Assert.NotNull(resultValue);
            Assert.Equal(nome ,resultValue.Name);
            Assert.Equal(email,resultValue.Email);
*/
        }
    }
}
