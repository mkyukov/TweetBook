using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetBook.Data;
using TweetBook.Services;

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
                   config.GetConnectionString("DefaultC onnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();

            services.AddSingleton<IPostService, PostService>();
        }
    }
}
