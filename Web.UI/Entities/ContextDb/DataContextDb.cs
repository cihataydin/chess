using Microsoft.EntityFrameworkCore;

namespace Web.UI.Entities.ContextDb
{
    public class DataContextDb:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server =localhost; Database = Users; integrated Security = true");
        }

        public DbSet<Users> Users { get; set; }
    }
}
