using DataProvider.Assistant.Enums;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.Models.Query.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.EntityFramework.Extensions.Identity;
public static class UserRoleExtension
{
    public static IQueryable<UserRole> ApplyFilter(this IQueryable<UserRole> query, GetPagedUserRoleQuery filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.User.Username.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.User.Email.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.Role.Title.ToLower().Contains(filter.Keyword.ToLower().Trim()));
        
        if (filter.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == filter.IsDeleted.Value);

        return query;
    }


    public static IQueryable<UserRole> ApplySort(this IQueryable<UserRole> query, SortByEnum? sortBy)
    {
        return sortBy switch
        {
            SortByEnum.CreationDate => query.OrderBy(x => x.CreatedAt),
            SortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            SortByEnum.UpdateDate => query.OrderBy(x => x.UpdatedAt),
            SortByEnum.UpdateDateDescending => query.OrderByDescending(x => x.UpdatedAt),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}
