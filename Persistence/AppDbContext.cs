using AspNetCoreAngularApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngularApp.Persistence
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
          
        } 
          public DbSet<Make> Makes { get; set; } 
          public DbSet<Feature> Features {get; set;}
    }
}