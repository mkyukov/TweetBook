using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetBook.Data;

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
                    await testFunc(context);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    // CleanupTestDatabase(context);
                }
            }
        }
    }
}