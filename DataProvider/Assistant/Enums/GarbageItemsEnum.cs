using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Assistant.Enums;
public enum GarbageItemsEnum
{
    [Display(Name = "Post Entity")]
    Post = 1,
    [Display(Name = "Post Category Entity")]
    PostCategory = 2,
    [Display(Name = "Post Comment Entity")]
    PostComment = 3,
    [Display(Name = "User Entity")]
    User = 4,
    [Display(Name = "Role Entity")]
    Role = 5,
    [Display(Name = "User Role Entity")]
    UserRole = 6
}
