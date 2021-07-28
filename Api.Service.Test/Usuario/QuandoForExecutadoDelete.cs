using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoDelete : UsuarioTestes
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;
       [Fact(DisplayName="É Possível executar o Método DELETE.")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            // MOCKAR DADOS
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m=>m.Delete(IdUsuario)).ReturnsAsync(true);
            _service = _serviceMock.Object ;

            // Passando para Executar
            var deletado = await _service.Delete(IdUsuario);
            Assert.True(deletado);

            // MOCKAR DADOS
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m=>m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object ;

            // Passando para Executar
            deletado = await _service.Delete(Guid.NewGuid());
            Assert.False(deletado);



        }                    
    }
}
