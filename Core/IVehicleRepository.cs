using System.Threading.Tasks;
using AspNetCoreAngularApp.Core.Models;

namespace AspNetCoreAngularApp.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true ); 
        void Add(Vehicle vehicle); 
        void Remove(Vehicle vehicle);
    }
}