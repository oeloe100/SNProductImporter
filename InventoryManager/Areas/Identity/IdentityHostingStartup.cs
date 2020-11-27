using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(InventoryManager.Areas.Identity.IdentityHostingStartup))]
namespace InventoryManager.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            /*builder.ConfigureServices((context, services) => {
                services.AddDbContext<InventoryManagerContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("InventoryManagerContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<InventoryManagerContext>();
            });*/

            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}