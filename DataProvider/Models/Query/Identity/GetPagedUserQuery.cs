using DataProvider.Assistant.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Query.Identity;
public class GetPagedUserQuery: DefaultPaginationFilter
{
    public bool? IsLockedOut { get; set; }
}
