using System.Threading.Tasks;
using AspNetCoreAngularApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngularApp.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext context;
        public VehicleRepository(AppDbContext context)
        {
            this.context = context;

        } 
       
        public async Task<Vehicle> GetVehicle(int id, bool inclueRelated = true)
        { 
            if(!inclueRelated)  
            return await context.Vehicles.FindAsync(id);

           return await context.Vehicles
           .Include(v => v.Features)
           .ThenInclude(vf => vf.Feature)
           .Include(v => v.Model)
           .ThenInclude(m => m.Make)
           .SingleOrDefaultAsync(v => v.Id == id);
        } 

        public void Add(Vehicle vehicle){
            context.Vehicles.Add(vehicle);
        } 
        public void Remove(Vehicle vehicle){
            context.Vehicles.Remove(vehicle);
        }
    }
}