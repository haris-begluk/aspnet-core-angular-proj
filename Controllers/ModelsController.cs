using System.Collections.Generic;
using System.Threading.Tasks;
using aspnet_core_angular_proj.Controllers.Resourses;
using AspNetCoreAngularApp.Core.Models;
using AspNetCoreAngularApp.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_core_angular_proj.Controllers
{
    public class ModelsController :Controller
    {
        public AppDbContext Context { get; }
        public IMapper Mapper { get; set; }
        public ModelsController(AppDbContext context, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Context = context;

        }
        [HttpGet("/api/models")]
        public async Task<IEnumerable<ModelResource>> GetModels()
        {
            var models = await Context.Models.ToListAsync(); 
            return Mapper.Map<List<Model>, List<ModelResource>>(models);
        }
    }
}