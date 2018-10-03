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
    public class MakesController : Controller
    {
        public AppDbContext Context { get; }
        public IMapper Mapper { get; set; }
        public MakesController(AppDbContext context, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Context = context;

        }
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await Context.Makes.Include(m => m.Models).ToListAsync(); 
            return Mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}