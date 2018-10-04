using System;
using System.Threading.Tasks;
using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Models;
using AspNetCoreAngularApp.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAngularApp.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private  IMapper mapper;
        private  AppDbContext context;
        public VehiclesController(IMapper mapper, AppDbContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]VehicleResource vehicleResource)
        {
            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);  
            vehicle.LastUpdate = DateTime.Now;
            context.Vehicles.Add(vehicle); 
            await context.SaveChangesAsync(); 
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle); 
            return Ok(result); 
            /* 
            
            {"modelId":15,
                "isRegistered":true,
                "contact": {
                "name":"name", 
                "phone": "phone", 
                "email": "email"
                },
                "lastUpdate":"2001-01-01T00:00:00",
                "features":[1,3,4]
                }
                
             */
        }
    }
}