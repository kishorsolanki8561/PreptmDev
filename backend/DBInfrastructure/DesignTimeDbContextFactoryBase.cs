using CommonService.Other.AppConfig;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TranslationMicroService
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
    IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string ConnectionStringName = "Default";
        public TContext CreateDbContext(string[] args)
        {
            string basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}MasterMicroService", Path.DirectorySeparatorChar);
            string envname = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return Create(basePath, envname);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string basePath, string envname)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            if (string.IsNullOrEmpty(ConnectionStrings.Default))
            {
                ConnectionStrings config = new();
                configuration.Bind("ConnectionStrings", config);
            }
            string connectionString = ConnectionStrings.Default;
            return Create(connectionString);
        }
        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            DbContextOptionsBuilder<TContext> optionsBuilder = new();

            _ = optionsBuilder.UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
    public class ConnectionStrings
    {
        public static string? Default { get; set; }
        public static string? log { get; set; }
    }
}
