using BlogManagementInfra.BbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagementInfra.Data
{
    public static class DatabaseSetup
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            bool useInMemory = false;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                using var testDbContext = new ApplicationDbContext(optionsBuilder.Options);
                if (testDbContext.Database.CanConnect())
                {
                    Console.WriteLine("Connected to the main database successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to connect to the main database.");
                    useInMemory = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to the main database. Fallback to in-memory database. Error: {ex.Message}");
                useInMemory = true;
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (useInMemory)
                {
                    Console.WriteLine("Using InMemory database as fallback.");
                    options.UseInMemoryDatabase("FallbackDatabase");
                }
                else
                {
                    Console.WriteLine("Using SQL Server as the main database.");
                    options.UseSqlServer(connectionString);
                }
            });

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (useInMemory)
            {
                Console.WriteLine("InMemory database initialized successfully.");
                dbContext.Database.EnsureCreated();
            }
            else
            {
                try
                {
                    Console.WriteLine("Applying migrations to the relational database.");
                    dbContext.Database.Migrate();
                    Console.WriteLine("Migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to apply migrations. Error: {ex.Message}");
                    throw;
                }
            }
        }
    }
}