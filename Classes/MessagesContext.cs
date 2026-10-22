using ChatStudents_Alikin.Classes.Common;
using ChatStudents_Alikin.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatStudents_Alikin.Classes
{
    public class MessagesContext : DbContext
    {
        public DbSet<Messages> Messages { get; set; }

        public MessagesContext() =>
            Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(Config.config);
    }
}
