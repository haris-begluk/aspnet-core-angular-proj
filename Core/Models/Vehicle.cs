using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aspnet_core_angular_proj.Core.Models;

namespace AspNetCoreAngularApp.Core.Models
{ 
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool isRegistered { get; set; }  
        [Required] 
        [StringLength(255)]
        public string ContactName { get; set; }  
        [StringLength(255)]
        public string ContactEmail { get; set; } 
        [Required] 
        [StringLength(255)]
        public string ContactPhone { get; set; } 
        public DateTime LastUpdate { get; set; } 

        public ICollection<VehicleFeature> Features { get; set; }  
        public ICollection<Photo> Photos { get; set; }
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>(); 
            Photos = new Collection<Photo>();
        }
    }
}