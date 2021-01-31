using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Trainer
    {
        [ForeignKey("ApplicationUser")]
        public string TrainerId { get; set; }
        public string UserName { get; set; }
        [DisplayName("Education level")]
        public string Education { get; set; }
        [DisplayName("Phone number")]
        public int Phone { get; set; }
        [DisplayName("Working location")]
        public string WorkingPlace { get; set; }
        public TrainerType Type { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
    public enum TrainerType
    {
        None = 0,
        Internal = 1,
        External = 2
    }
}