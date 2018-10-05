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
            if(!ModelState.IsValid) 
            return BadRequest(ModelState);//when we pass ModelState to view we can display error messages to client
           
        //    if(true){ //Business Rule Validation
        //        ModelState.AddModelError("...", "Error"); 
        //        return BadRequest(ModelState);
        //    } 
            var model = await context.Models.FindAsync(vehicleResource.ModelId);  
            if(model == null){
                ModelState.AddModelError("ModelId", "Invalid Model!"); 
                return BadRequest(ModelState);
            }
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