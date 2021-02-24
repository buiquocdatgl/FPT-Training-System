using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name should not be Empty !!!")]
        [StringLength(255)]
        [DisplayName("Course Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description should not be Empty !!!")]
        [StringLength(255)]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsAvailable { get; set; }
    }
}