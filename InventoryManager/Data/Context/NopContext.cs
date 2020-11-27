using InventoryManager.Models.NopAccess;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Data.Context
{
    public class NopContext : DbContext
    {
        public DbSet<NopAuthorizationModel> NopComAuthorization { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-InventoryManager-C427894C-FBB2-48DF-A845-758675DC3172;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
