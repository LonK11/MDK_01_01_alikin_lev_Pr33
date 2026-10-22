using ChatStudents_Alikin.Classes.Common;
using ChatStudents_Alikin.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatStudents_Alikin.Classes
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UsersContext() =>
            Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(Config.config);
    }
}
