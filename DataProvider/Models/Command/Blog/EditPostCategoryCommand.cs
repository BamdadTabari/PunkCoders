﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Command.Blog;
public class EditPostCategoryCommand
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = " Id is Required")]
    public int PostCategoryId { get; set; }
}
