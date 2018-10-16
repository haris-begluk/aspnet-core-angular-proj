
using System.Collections.Generic;
using System.Linq;
using aspnet_core_angular_proj.Controllers.Resourses;
using aspnet_core_angular_proj.Core.Models;
using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Core.Models;
using AutoMapper;

namespace AspNetCoreAngularApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            //Domain to API Resource  
            CreateMap<Photo, PhotoResource>();
            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));
            CreateMap<Make, MakeResource>(); 
            CreateMap<Make, KeyValuePairResource>();  
            CreateMap<Model,ModelResource>();
            CreateMap<Model, KeyValuePairResource>(); 
            CreateMap<Feature, KeyValuePairResource>();  
             CreateMap<Vehicle,SaveVehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}));
             
             CreateMap<Vehicle, VehicleResource>() 
            .ForMember(vr => vr.Make, opt => opt.MapFrom( v => v.Model.Make))
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource{ Id = vf.Feature.Id, Name = vf.Feature.Name })));

            //Api Resource to Domain  
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource,Vehicle>() 
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt =>opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt =>opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(v => v.ContactPhone, opt =>opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.Features, opt =>opt.Ignore()) 
            .AfterMap((vr, v) =>{
            //Remove unselected features   
              var removedFeatures =  v.Features
              .Where(f => !vr.Features
              .Contains(f.FeatureId))
              .ToList();
                   foreach (var rf in removedFeatures)
                        v.Features.Remove(rf);
            //Add new Features 
              var addedFeatures =  vr.Features
              .Where(id => !v.Features.Any(f => f.FeatureId == id))
              .Select(id => new VehicleFeature{FeatureId = id}) 
              .ToList(); 
              foreach (var f in addedFeatures)
                 v.Features.Add(f);
            });

        }
    }
}