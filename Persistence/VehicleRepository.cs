using System.Threading.Tasks;
using AspNetCoreAngularApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using AspNetCoreAngularApp.Core;
using System.Collections.Generic;
using aspnet_core_angular_proj.Core.Models; 
using System.Linq;

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
        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj){
            var query = context.Vehicles
            .Include(v => v.Model) 
            .ThenInclude(m => m.Make) 
            .Include(v => v.Features) 
            .ThenInclude(vf => vf.Feature) 
            .AsQueryable();
            if(queryObj.MakeId.HasValue)
            query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);  
            if(queryObj.ModelId.HasValue)
            query = query.Where(v => v.ModelId == queryObj.ModelId.Value); 

            if(queryObj.SortBy == "make")
            query = (queryObj.IsSortAscending) ? 
            query.OrderBy(v => v.Model.Make.Name): 
            query.OrderByDescending(v => v.Model.Make.Name); 
            if(queryObj.SortBy == "model")
            query = (queryObj.IsSortAscending) ? 
            query.OrderBy(v => v.Model.Name): 
            query.OrderByDescending(v => v.Model.Name); 
            if(queryObj.SortBy == "contactName")
            query = (queryObj.IsSortAscending) ? 
            query.OrderBy(v => v.ContactName): 
            query.OrderByDescending(v => v.ContactName); 
            if(queryObj.SortBy == "id")
            query = (queryObj.IsSortAscending) ? 
            query.OrderBy(v => v.Id): 
            query.OrderByDescending(v => v.Id);

            return await query.ToListAsync();
        }
    }
}