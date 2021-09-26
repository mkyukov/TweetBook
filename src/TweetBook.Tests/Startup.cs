using Microsoft.Extensions.DependencyInjection;
using TweetBook.Services;

namespace TweetBook.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
        }
    }
}