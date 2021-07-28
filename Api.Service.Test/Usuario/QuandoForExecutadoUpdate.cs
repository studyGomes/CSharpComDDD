using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoUpdate : UsuarioTestes
    {

        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName="É Possível executar o Método UPDATE.")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            // MOCKAR DADOS
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m=>m.Post(userDTOCreate)).ReturnsAsync(userDTOCreateResult);
            _service = _serviceMock.Object ;

            // Passando para Executar
            var result = await _service.Post(userDTOCreate);
            Assert.NotNull(result);
            Assert.Equal(NomeUsuario,result.Name);
            Assert.Equal(EmailUsuario,result.Email);

            // MOCKAR DADOS
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m=>m.Put(userDTOUpdate)).ReturnsAsync(userDTOUpdateResult);
            _service = _serviceMock.Object ;

            // Passando para Executar
            var resultUpdate = await _service.Put(userDTOUpdate);
            Assert.NotNull(resultUpdate);
            Assert.Equal(NomeUsuarioAlterado,resultUpdate.Name);
            Assert.Equal(EmailUsuarioAlterado,resultUpdate.Email);


        }          
    }
}
