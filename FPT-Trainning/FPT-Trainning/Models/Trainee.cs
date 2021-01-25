using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Trainee
    {
        [ForeignKey("ApplicationUser")]
        public string TraineeId { get; set; }
        public string ProgramLanguage { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public int Experience { get; set; }
        public string Education { get; set; }
        public string Location { get; set; }
        public int ToeicScore { get; set; }
        public int? CourseId { get; set; }
        public Course course { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}