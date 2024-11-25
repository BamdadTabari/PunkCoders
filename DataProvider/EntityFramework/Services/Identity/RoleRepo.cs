using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.EntityFramework.Services.Identity;
public interface IRoleRepo: IRepository<Role>
{
    
}
public class RoleRepo(AppDbContext context) : Repository<Role>(context), IRoleRepo
{
}
