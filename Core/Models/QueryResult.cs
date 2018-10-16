using System.Collections.Generic;

namespace aspnet_core_angular_proj.Core.Models
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; } 
        public IEnumerable<T> Items { get; set; }
    }
}