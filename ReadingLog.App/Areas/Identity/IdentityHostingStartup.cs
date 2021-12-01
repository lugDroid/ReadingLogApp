using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingLog.App.Areas.Identity.Data;

[assembly: HostingStartup(typeof(ReadingLog.App.Areas.Identity.IdentityHostingStartup))]
namespace ReadingLog.App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                // services.AddDbContext<IdentityDataContext>(options =>
                //     options.UseSqlServer(
                //         context.Configuration.GetConnectionString("IdentityDataContextConnection")));

                // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //     .AddEntityFrameworkStores<IdentityDataContext>();
            });
        }
    }
}