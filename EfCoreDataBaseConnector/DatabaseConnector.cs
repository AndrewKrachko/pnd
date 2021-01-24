using Items;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EfCoreRepository
{
    public class EfCoreDatabaseConnector : DbContext, IDatabaseConnector
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-3BAAIFR\\SQLEXPRESS;User ID=sa;Password=Password123; Initial Catalog = UsersDb");
        }

        public bool GetUserByName(string name, out IUser user)
        {
            var query = this.Users.FirstOrDefaultAsync(u => u.Name == name);
            user = query.Result;

            if (query.Result != null)
            {
                return true;
            }

            return false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "User", Password = "123" });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, Name = "User2", Password = "123" });
        }
    }
}
