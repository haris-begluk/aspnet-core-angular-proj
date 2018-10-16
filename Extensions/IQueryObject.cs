namespace aspnet_core_angular_proj.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }  
         bool IsSortAscending { get; set; } 
         int Page { get; set; }
         int PageSize { get; set; }
    }
}