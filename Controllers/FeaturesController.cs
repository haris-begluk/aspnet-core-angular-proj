using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreAngularApp.Controllers.Resourses;
using AspNetCoreAngularApp.Core.Models;
using AspNetCoreAngularApp.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await Context.Features.ToListAsync();
            return Mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }
    }
}