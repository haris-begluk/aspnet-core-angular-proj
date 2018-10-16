using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aspnet_core_angular_proj.Controllers.Resourses;
using aspnet_core_angular_proj.Core.Models;
using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Core;
using AspNetCoreAngularApp.Core.Models;
using AspNetCoreAngularApp.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngularApp.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public VehiclesController(IMapper mapper,  IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource vehicleResource)
        { 
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();
            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();
            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await unitOfWork.CompleteAsync(); 
            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);
            if (vehicle == null)
                return NotFound();
            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();
            return Ok(id);
        }
        [HttpGetAttribute("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();
            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }  
        [HttpGet]
        public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filterResource)
        { 
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var queryResult = await repository.GetVehicles(filter); 
            return mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
        }
    }
}