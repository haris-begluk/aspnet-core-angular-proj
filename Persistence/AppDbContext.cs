using AspNetCoreAngularApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngularApp.Persistence
{
    public class AppDbContext :DbContext
    {
        
          public DbSet<Make> Makes { get; set; } 
          public DbSet<Feature> Features {get; set;}   
          public DbSet<Model> Models { get; set; }
          public DbSet<Vehicle> Vehicles { get; set; }
          public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
          
        }  
        protected override void OnModelCreating(ModelBuilder modelBuilder){ 
            //Creeating composit key for VehicleFeature class
                modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { 
                    vf.VehicleId, 
                    vf.FeatureId
                });
        }
    }
}