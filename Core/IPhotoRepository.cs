using System.Collections.Generic;
using System.Threading.Tasks;
using aspnet_core_angular_proj.Core.Models;

namespace aspnet_core_angular_proj.Core
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}