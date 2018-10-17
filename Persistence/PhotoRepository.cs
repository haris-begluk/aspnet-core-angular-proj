using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet_core_angular_proj.Core;
using aspnet_core_angular_proj.Core.Models;
using AspNetCoreAngularApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace aspnet_core_angular_proj.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext context;
        public PhotoRepository(AppDbContext context)
        {
            this.context = context;

        } 
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId){
                return await context.Photos 
                .Where( p => p.VehicleId == vehicleId)
                .ToListAsync();
        }
    }
}