using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using aspnet_core_angular_proj.Core.Models;
using AspNetCoreAngularApp.Core.Models;

namespace aspnet_core_angular_proj.Extensions
{
    public static class IQuerableExtensions
    {
        public static  IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query ,IQueryObject queryObj, Dictionary<string, Expression<Func<T,object>>> columnsMap){
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
            return query;
            if(queryObj.IsSortAscending) 
            return  query = query.OrderBy(columnsMap[queryObj.SortBy]);
            else
            return query = query.OrderByDescending(columnsMap[queryObj.SortBy]); 
        }
    }
}