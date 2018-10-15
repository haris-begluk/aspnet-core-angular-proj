using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Core.Models;

namespace aspnet_core_angular_proj.Controllers.Resourses
{
    public class ModelResource : KeyValuePairResource
    {
       
        public int MakeId { get; set; }
    }
}