using System;
using System.IO;
using System.Threading.Tasks;
using aspnet_core_angular_proj.Controllers.Resourses;
using aspnet_core_angular_proj.Core.Models;
using AspNetCoreAngularApp.Core;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_angular_proj.Controllers
{
    // /api/vehicles/1/photos 
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host; 
         public IVehicleRepository Repository { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IMapper mapper { get; }

        public PhotosController(
        IHostingEnvironment host, 
        IVehicleRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            this.host = host;
            Repository = repository;
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }

       

        //To uplodat one file we use IFormFile 
        //To uploda multiple files we use IFormCollection
        [HttpPost]
        public async  Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {  

            var vehicle = await Repository.GetVehicle(vehicleId, includeRelated:false);
            if(vehicle == null) 
            return NotFound(); 

             var uploadsFolderPath = Path.Combine(host.WebRootPath, "Uploads"); 
             if(Directory.Exists(uploadsFolderPath)) 
             Directory.CreateDirectory(uploadsFolderPath); 

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); 
            var filePath = Path.Combine(uploadsFolderPath,fileName); 
            using(var stream = new FileStream(filePath, FileMode.Create)){
               await file.CopyToAsync(stream);
            } 

            //System.Drawing check this 
            var photo = new Photo{ FileName = fileName}; 
            vehicle.Photos.Add(photo); 
            await UnitOfWork.CompleteAsync(); 
            return Ok( mapper.Map<Photo,PhotoResource>(photo));
        }
    }
}