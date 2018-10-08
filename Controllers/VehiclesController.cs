using System;
using System.Threading.Tasks;
using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Models;
using AspNetCoreAngularApp.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource vehicleResource)
        { 
            if(!ModelState.IsValid) 
            return BadRequest(ModelState);
         
            
            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);  
            vehicle.LastUpdate = DateTime.Now;
            context.Vehicles.Add(vehicle); 
            await context.SaveChangesAsync(); 
             vehicle = await context.Vehicles
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature) 
            .Include(v => v.Model) 
            .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync(v => v.Id == vehicle.Id);  

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle); 
            return Ok(result); } 
            
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource vehicleResource)
        { 
            if(!ModelState.IsValid) 
            return BadRequest(ModelState);
           var vehicle = await context.Vehicles
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature) 
            .Include(v => v.Model) 
            .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync(v => v.Id == id); 
            if(vehicle == null) 
            return NotFound();         
            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);  
            vehicle.LastUpdate = DateTime.Now;
            await context.SaveChangesAsync(); 
            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle); 
            return Ok(result); 
        }  
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id){
            var vehicle = await context.Vehicles.FindAsync(id); 
            if(vehicle == null) 
            return NotFound();
            context.Remove(vehicle); 
            await  context.SaveChangesAsync(); 
            return Ok(id);
        }  
        [HttpGetAttribute("{id}")]
        public async Task<IActionResult> GetVehicle(int id){
            var vehicle = await context.Vehicles
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature) 
            .Include(v => v.Model) 
            .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync(v => v.Id == id); 
            if(vehicle == null)  
            return NotFound(); 
            var vehicleResource =mapper.Map<Vehicle,VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }
    }
}