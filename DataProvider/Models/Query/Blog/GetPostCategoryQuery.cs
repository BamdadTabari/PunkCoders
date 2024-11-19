using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Query.Blog;
public class GetPostCategoryQuery
{
    [Required(ErrorMessage = " Id is Required")]
    public int PostCategoryId { get; set; }
}
