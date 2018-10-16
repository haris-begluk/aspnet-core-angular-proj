using System.Collections.Generic;
using System.Threading.Tasks;
using aspnet_core_angular_proj.Core.Models;
using AspNetCoreAngularApp.Core.Models;

namespace AspNetCoreAngularApp.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true ); 
        void Add(Vehicle vehicle); 
        void Remove(Vehicle vehicle); 
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
    }
}