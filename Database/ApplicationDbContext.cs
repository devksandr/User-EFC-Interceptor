using Microsoft.EntityFrameworkCore;
using User_EFC_Interceptor.Models.Entities;

namespace User_EFC_Interceptor.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; } = null!;
    }
}
