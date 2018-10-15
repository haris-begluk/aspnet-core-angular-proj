namespace aspnet_core_angular_proj.Core.Models
{
    public class VehicleQuery
    {
        public int? MakeId { get; set; } 
        public int? ModelId { get; set; } 
        public string SortBy { get; set; }  
        public bool IsSortAscending { get; set; }
    }
}