using FPT_Trainning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Trainning.ViewModel
{
    public class ListTrainerAccount
    {
        public List<Trainer> Trainers { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}