using User_EFC_Interceptor.Models;
using User_EFC_Interceptor.Models.DTO;

namespace User_EFC_Interceptor.Services
{
    public interface IUserService
    {
        public Task<ServiceResult<bool>> AddUser(UserAddDTO userData);
        public Task<ServiceResult<string?>> GetUserPhrase(string username);
    }
}
