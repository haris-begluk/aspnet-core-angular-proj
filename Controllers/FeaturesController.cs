using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Models;
using AspNetCoreAngularApp.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngularApp.Controllers
{
    public class FeaturesController
    {
        public AppDbContext Context { get; }
        public IMapper Mapper { get; set; }
        public FeaturesController(AppDbContext context, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Context = context;

        } 
        [HttpGet("/api/features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var features = await Context.Features.ToListAsync();
            return Mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }
    }
}