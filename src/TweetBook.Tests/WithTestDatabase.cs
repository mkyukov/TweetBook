using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Tests
{
    public class WithTestDatabase
    {
        public static async Task Run(Func<DataContext, Task> testFunc)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("IN_MEMORY_DATABASE")
                .Options;

            using (var context = new DataContext(options))
            {
                try
                {
                    await context.Database.EnsureCreatedAsync();
                    // PrepareTestDatabase(context);
                    await SeedInMemDb(context);
                    await testFunc(context);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private static async Task SeedInMemDb(DataContext context)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    await context.Posts.AddAsync(new Post() { Name = $"Test post #{i}" });
                }
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}