using GreenrAPI.Entities;
using Microsoft.AspNetCore.Http;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserTrip>()
                .HasOne(t => t.User)
                .WithMany(u => u.Trips)
                .HasForeignKey(t => t.UserId);

            

        }

        public DbContextOptions Options { get; }

        public DbSet<Trip> Trips { get; set; }

    }
}