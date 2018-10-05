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
            return BadRequest(ModelState);
         
            
            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);  
            vehicle.LastUpdate = DateTime.Now;
            context.Vehicles.Add(vehicle); 
            await context.SaveChangesAsync(); 
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle); 
            return Ok(result); } 
            
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]VehicleResource vehicleResource)
        { 
            if(!ModelState.IsValid) 
            return BadRequest(ModelState);
            var vehicle =await context.Vehicles.FindAsync(id);
            mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);  
            vehicle.LastUpdate = DateTime.Now;
            await context.SaveChangesAsync(); 
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle); 
            return Ok(result); 
        }
    }
}