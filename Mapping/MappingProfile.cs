using System.Linq;
using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Models;
using AutoMapper;

namespace AspNetCoreAngularApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            //Domain to API Resource
            CreateMap<Make, MakeResource>(); 
            CreateMap<Model, ModelResource>(); 
            CreateMap<Feature, FeatureResource>(); 
            //Api Resource to Domain 
            CreateMap<VehicleResource,Vehicle>()
            .ForMember(v => v.ContactName, opt =>opt.MapFrom(vr => vr.Contact.Name))
             .ForMember(v => v.ContactEmail, opt =>opt.MapFrom(vr => vr.Contact.Email))
              .ForMember(v => v.ContactPhone, opt =>opt.MapFrom(vr => vr.Contact.Phone))
               .ForMember(v => v.Features, opt =>opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature{FeatureId = id}))); 

        }
    }
}