using Apps_Review_Api.Models;

namespace Apps_Review_Api.Interface
{
    public interface IUserName
    {
        ICollection<User> Usernames();
        User IsUserFound (UserDto user);

        bool LogingIn (UserDto user);

    }
}
