﻿using FPT_Trainning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Trainning.ViewModel
{
    public class ChangeTraineeAssign
    {
        public IEnumerable<Course> Courses { get; set; }
        public Trainee Trainee { get; set; }
    }
}