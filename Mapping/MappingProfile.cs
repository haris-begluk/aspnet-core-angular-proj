using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Models;
using AutoMapper;

namespace AspNetCoreAngularApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>(); 
            CreateMap<Model, ModelResource>(); 
            CreateMap<Feature, FeatureResource>();
        }
    }
}