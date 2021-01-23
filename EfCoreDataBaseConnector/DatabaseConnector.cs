using Items;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfCoreRepository
{
    public class EfCoreDatabaseConnector : DbContext, IDatabaseConnector
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=KALV-19-09;User ID=sa;Password=1234; Initial Catalog = UsersDb");
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
        }
    }
}
