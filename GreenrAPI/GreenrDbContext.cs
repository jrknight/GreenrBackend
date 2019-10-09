using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenrAPI
{
    public class GreenrDbContext : IdentityDbContext
    {

        public GreenrDbContext(DbContextOptions<GreenrDbContext> options) : base(options)
        {

        }

        public DbContextOptions Options { get; }

    }
}
