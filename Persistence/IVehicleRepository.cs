using System.Threading.Tasks;
using AspNetCoreAngularApp.Models;

namespace AspNetCoreAngularApp.Persistence
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id);
    }
}