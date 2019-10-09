using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GreenrAPI
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GreenrDbContext>
    {
        public GreenrDbContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("config.json");
            var config = configBuilder.Build();

            var connectionString = config.GetConnectionString("LocalDB");

            var builder = new DbContextOptionsBuilder<GreenrDbContext>();

            builder.UseSqlServer(connectionString);

            var context = new GreenrDbContext(builder.Options);

            return context;

        }
        
    }
}
