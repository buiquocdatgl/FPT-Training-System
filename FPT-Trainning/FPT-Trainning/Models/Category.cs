using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Category Description")]
        public string Description { get; set; }
    }
}