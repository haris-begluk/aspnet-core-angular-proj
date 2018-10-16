using System.Collections.Generic;

namespace aspnet_core_angular_proj.Controllers.Resourses
{
    public class QueryResultResource<T>
    {
        public int TotalItems { get; set; } 
        public IEnumerable<T> Items { get; set; }
    }
}