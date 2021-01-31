using FPT_Trainning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Trainning.ViewModel
{
    public class ListTrainerAndTraineeInCourse
    {
        public Course Course { get; set;  }
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<Trainee> Trainees { get; set; }
            
    }
}