using BookingApi.Models;

namespace DAL.Repositories.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByEmail(string email);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
