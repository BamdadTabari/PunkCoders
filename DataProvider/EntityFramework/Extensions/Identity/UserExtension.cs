using DataProvider.Assistant.Enums;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.Models.Query.Blog.PostCategory;
using DataProvider.Models.Query.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.EntityFramework.Extensions.Identity;
public static class UserExtension
{
    public static IQueryable<User> ApplyFilter(this IQueryable<User> query, GetPagedUserQuery filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Username.ToLower().Contains(filter.Keyword.ToLower().Trim()) 
            || x.Email.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.UserRoles.Any(x => x.Role.Title.ToLower().Contains(filter.Keyword.ToLower().Trim())));
        if(filter.IsLockedOut.HasValue)
            query = query.Where(x => x.IsLockedOut == filter.IsLockedOut.Value);
        
        if (filter.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == filter.IsDeleted.Value);

        return query;
    }


    public static IQueryable<User> ApplySort(this IQueryable<User> query, SortByEnum? sortBy)
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
