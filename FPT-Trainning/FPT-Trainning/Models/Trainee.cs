using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Trainee
    {
        [ForeignKey("ApplicationUser")]
        public string TraineeId { get; set; }
        public string UserName { get; set; }
        [DisplayName("Programming language")]
        public string ProgramLanguage { get; set; }
        [DisplayName("Age")]
        public int Age { get; set; }
        [DisplayName("Date of birth")]
        public DateTime DOB { get; set; }
        [DisplayName("Experience level")]
        public int Experience { get; set; }
        [DisplayName("Education level")]
        public string Education { get; set; }
        [DisplayName("Address")]
        public string Location { get; set; }
        [DisplayName("TOEIC rating")]
        public int ToeicScore { get; set; }
        public int? CourseId { get; set; }
        public Course course { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}