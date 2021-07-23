using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Service.Services
{
    public interface ILoginService
    {
         Task<object> FindByLogin(UserEntity user);
    }
}
