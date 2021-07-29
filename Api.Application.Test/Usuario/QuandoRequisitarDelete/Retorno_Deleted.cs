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
    public class Retorno_Deleted
    {

        private UsersController _controller;
        [Fact(DisplayName="É Possível Realizar o DELETED.")]
        public async Task E_Possivel_Invocar_a_Controller_Deleted()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m=>m.Delete(It.IsAny<Guid>())).ReturnsAsync(
                true
            );

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value ;
            Assert.NotNull(resultValue);
            Assert.True((bool) resultValue);
            //Assert.Equal(userDTOUpdate.Name ,resultValue.Name);
            //Assert.Equal(userDTOUpdate.Email,resultValue.Email);

        }        
    }
}
