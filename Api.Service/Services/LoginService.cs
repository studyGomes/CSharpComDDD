using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Repository;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        public LoginService(IUserRepository repository)
        {
            _repository = repository ;
        }

        public async Task<object> FindByLogin(LoginDTO user)
        {
            //var baseUser = new UserEntity();
            if(user!=null && !string.IsNullOrWhiteSpace(user.Email))
            {
                //baseUser = await _repository.FindByLogin(user.Email); 
                return await _repository.FindByLogin(user.Email); 
            }
            else
            {
                return null ;
            }
            
        }
    }
}
