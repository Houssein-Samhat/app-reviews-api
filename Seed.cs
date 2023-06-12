using Apps_Review_Api.Data;
using Apps_Review_Api.Models;
using Apps_Review_Api.Services;

namespace Apps_Review_Api
{
    public class Seed
    {
        private readonly EncodingUserPassService _registerdata;
        private readonly DataContext _dataContext;
        public Seed(DataContext dataContext ,EncodingUserPassService registerdata)
        {
            this._registerdata = registerdata;
            this._dataContext = dataContext;
        }

        public void seedData()
        {
            if(!_dataContext.Usernames.Any())
            {
                List<User> users = new List<User>();

                User u1 = new User();
                u1.Username = "housseinsamhat2000@gmail.com";
                _registerdata.CreatePasswordHash("Souna8742@!", out byte[] passHash, out byte[] passSalt);
                u1.PasswordHash = passHash;
                u1.PasswordSalt = passSalt;
                users.Add(u1);

                User u2 = new User();
                u2.Username = "Carla96@gmail.com";
                _registerdata.CreatePasswordHash("Carla8441@!", out byte[] passHash1, out byte[] passSalt1);
                u2.PasswordHash = passHash1;
                u2.PasswordSalt = passSalt1;
                users.Add(u2);

                _dataContext.Usernames.AddRange(users);
                _dataContext.SaveChanges();
            }
        }


    }
}
