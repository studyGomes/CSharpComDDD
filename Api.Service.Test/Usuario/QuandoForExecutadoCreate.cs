using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoCreate : UsuarioTestes
    {

        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName="É Possível executar o Método CREATE.")]
        public async Task E_Possivel_Executar_Metodo_Create()
        {
            // MOCKAR DADOS
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m=>m.Post(userDTOCreate)).ReturnsAsync(userDTOCreateResult);
            _service = _serviceMock.Object ;

            // Passando para Executar
            var result = await _service.Post(userDTOCreate);
            Assert.NotNull(result);
            //Assert.True(result.Id==IdUsuario);
            Assert.Equal(NomeUsuario,result.Name);
            Assert.Equal(EmailUsuario,result.Email);


        }        
    }
}
