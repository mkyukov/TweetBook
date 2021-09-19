using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetBook.Data;

namespace TweetBook.Installers
{
    public class DbInstaller:IInstaller
    {
        public DbInstaller()
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
               options.UseSqlite(
                   config.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();
        }
    }
}
