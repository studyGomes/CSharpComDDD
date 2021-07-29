using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarDelete
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

            serviceMock.Setup(m=>m.Delete(It.IsAny<Guid>())).ReturnsAsync(
                false
            );


            _controller = new UsersController(serviceMock.Object);
            // ADICIONAR Model State ERROR
            _controller.ModelState.AddModelError("Id","Formato Inválido");

            var result = await _controller.Delete(default(Guid));
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
