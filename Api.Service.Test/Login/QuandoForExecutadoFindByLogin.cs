using System;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class QuandoForExecutadoFindByLogin 
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName="É Possível executar o Método FINDBYLOGIN.")]
        public async Task E_Possivel_Executar_Metodo_FindByLogin()
        {
            var email = Faker.Internet.Email();
            var objetoRetorno = new
            {
                authenticated   = true,
                created         = DateTime.UtcNow,
                expiration      = DateTime.UtcNow,
                accessToken     = Guid.NewGuid(),
                userName        = email,
                name            = Faker.Name.FullName(),
                message         = "Usuário Logado com sucesso" 
            }; 

            var loginDTO = new LoginDTO
            {
                Email = email
            };        

            // MOCKAR DADOS
            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m=>m.FindByLogin(loginDTO)).ReturnsAsync(objetoRetorno);
            _service = _serviceMock.Object ;

            var result = await _service.FindByLogin(loginDTO);
            Assert.NotNull(result);
            

        }

    }
}
