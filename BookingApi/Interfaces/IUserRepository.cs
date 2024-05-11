using BookingApi.Models;

namespace BookingApi.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        bool UserExists(int userId);    
        User GetUserById(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
