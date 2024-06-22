using Microsoft.EntityFrameworkCore;
using User_EFC_Interceptor.Entities;

namespace User_EFC_Interceptor.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
    }
}
