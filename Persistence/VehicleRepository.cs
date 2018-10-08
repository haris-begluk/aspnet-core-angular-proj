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
        public async Task<Vehicle> GetVehicle(int id)
        {
            return await context.Vehicles
           .Include(v => v.Features)
           .ThenInclude(vf => vf.Feature)
           .Include(v => v.Model)
           .ThenInclude(m => m.Make)
           .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}