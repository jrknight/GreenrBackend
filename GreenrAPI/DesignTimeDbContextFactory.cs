using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GreenrAPI
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GreenrDbContext>
    {
        public DesignTimeDbContextFactory()
        {
        }

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
