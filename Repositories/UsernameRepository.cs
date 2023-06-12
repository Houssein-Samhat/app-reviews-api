using Apps_Review_Api.Data;
using Apps_Review_Api.Interface;
using Apps_Review_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Apps_Review_Api.Repositories
{
    public class UsernameRepository : IUserName
    {
        private readonly DataContext _datacontext;

        public UsernameRepository(DataContext datacontext)
        {
            this._datacontext = datacontext;
        }

        public User IsUserFound(UserDto user)
        {
            User userName = null;
            userName = _datacontext.Usernames.Where(u=>u.Username == user.UserName).FirstOrDefault();
            return userName;
        }

        public bool LogingIn(UserDto user)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> Usernames()
        {
            return _datacontext.Usernames.OrderBy(u => u.Id).ToList();
        }
    }
}
