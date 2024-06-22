using User_EFC_Interceptor.Database;
using User_EFC_Interceptor.Models;
using User_EFC_Interceptor.Models.DTO;
using User_EFC_Interceptor.Models.Entities;

namespace User_EFC_Interceptor.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceResult<bool> AddUser(UserAddDTO userData)
        {
            try
            {
                if (CheckUserExists(userData.Username) != null)
                {
                    return new ServiceResult<bool>(false, $"User with username '{userData.Username}' already exists");
                }

                var user = new User
                {
                    Username = userData.Username,
                    Phrase = userData.Phrase
                };
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return new ServiceResult<bool>(false, "Error occured while trying to add user to database");
            }
            
            return new ServiceResult<bool>(true);
        }

        public ServiceResult<string?> GetUserPhrase(string username)
        {
            var user = CheckUserExists(username);
            if (user is null)
            {
                return new ServiceResult<string?>(null, $"User with username '{username}' doesn't exist");
            }

            return new ServiceResult<string?>(user.Phrase);
        }

        private User? CheckUserExists(string username) 
            => _db.Users.FirstOrDefault(u => u.Username == username);
    }
}
