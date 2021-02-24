using FPT_Trainning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Trainning.ViewModel
{
    public class CourseCategoriesViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}