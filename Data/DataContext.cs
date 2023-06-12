using Apps_Review_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Apps_Review_Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Usernames { get; set; }

    }
}
