﻿using System.ComponentModel.DataAnnotations;

namespace DataProvider.Models.Query.Blog;
public class GetPostCategoryQuery
{
    [Required(ErrorMessage = " Id is Required")]
    public int PostCategoryId { get; set; }
}
