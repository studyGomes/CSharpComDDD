using System.Threading.Tasks;
using Api.Domain.DTOs;

namespace Api.Service.Services
{
    public interface ILoginService
    {
         Task<object> FindByLogin(LoginDTO user);
    }
}
