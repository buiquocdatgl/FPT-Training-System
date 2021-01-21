using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Trainer
    {
        [ForeignKey("ApplicationUser")]
        public string TrainerId { get; set; }
        public string Education { get; set; }
        public string Phone { get; set; }
        public string WorkingPlace { get; set; }
        public string Type { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}