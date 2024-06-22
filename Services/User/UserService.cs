using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User_EFC_Interceptor.Database;
using User_EFC_Interceptor.Models;
using User_EFC_Interceptor.Models.DTO;
using UserEntity = User_EFC_Interceptor.Models.Entities.User;

namespace User_EFC_Interceptor.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext db, ILogger<UserService> logger, IMapper mapper)
        {
            (_db, _logger, _mapper) = (db, logger, mapper);
        }

        public async Task<ServiceResult<bool>> AddUser(UserAddDTO userData)
        {
            try
            {
                if (await CheckUserExists(userData.Username) != null)
                {
                    _logger.LogWarning("User with username '{username}' already exists", userData.Username);
                    return new ServiceResult<bool>(false, $"User with username '{userData.Username}' already exists");
                }

                var user = _mapper.Map<UserEntity>(userData);
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            catch
            {
                _logger.LogError("Error occured while trying to add user to database");
                return new ServiceResult<bool>(false, "Error occured while trying to add user to database");
            }

            return new ServiceResult<bool>(true);
        }

        public async Task<ServiceResult<string?>> GetUserPhrase(string username)
        {
            try
            {
                var user = await CheckUserExists(username);
                if (user is null)
                {
                    _logger.LogWarning("User with username '{username}' doesn't exist", username);
                    return new ServiceResult<string?>(null, $"User with username '{username}' doesn't exist");
                }

                return new ServiceResult<string?>(user.Phrase);
            }
            catch (Exception)
            {
                _logger.LogError("Error occured while trying to get user phrase");
                return new ServiceResult<string?>(null, "Error occured while trying to get user phrase");
            }
        }

        private async Task<UserEntity?> CheckUserExists(string username)
            => await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
