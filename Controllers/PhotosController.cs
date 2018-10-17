using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aspnet_core_angular_proj.Controllers.Resourses;
using aspnet_core_angular_proj.Core;
using aspnet_core_angular_proj.Core.Models;
using AspNetCoreAngularApp.Core;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace aspnet_core_angular_proj.Controllers
{
    // /api/vehicles/1/photos 
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        
        // private readonly int MAX_BYTES =10 * 1024 * 1024; 
        // private readonly string[] ACCEPTED_FILE_TYPES = new []{ ".jpg", ".jpeg", ".png"}; 
        private readonly PhotoSettings photoSettings;
        private readonly IHostingEnvironment host; 
         public IVehicleRepository vehicleRepository { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IMapper mapper { get; } 
        private readonly IPhotoRepository photoRepository;

        public PhotosController(
        IHostingEnvironment host, 
        IVehicleRepository vehicleRepository, 
        IPhotoRepository photoRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        { 
            this.photoSettings = options.Value;
            this.host = host;
            this.vehicleRepository = vehicleRepository;
            UnitOfWork = unitOfWork;
            this.mapper = mapper; 
            this.photoRepository = photoRepository;
        }

       
        [HttpGet] 
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId) {
        var photos = await photoRepository.GetPhotos(vehicleId);
        return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
        //To uplodat one file we use IFormFile 
        //To uploda multiple files we use IFormCollection
        [HttpPost]
        public async  Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {  

            var vehicle = await vehicleRepository.GetVehicle(vehicleId, includeRelated:false);
            if(vehicle == null) 
            return NotFound();  
            if(file == null) 
            return BadRequest("Null file."); 
            if(file.Length == 0) return BadRequest("Empty file.");  
            if(file.Length > photoSettings.MaxBytes) return BadRequest("Maximum file size exceeded. 10MB"); 
            if(!photoSettings.isSuported(file.FileName)) 
            return BadRequest("Invalid file type.");


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