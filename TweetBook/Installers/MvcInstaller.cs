using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TweetBook.Installers
{
    public class MvcInstaller : IInstaller
    {
        public MvcInstaller()
        {
             
        }

        public void InstallServices(IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo());
            });
        }
    }
}
