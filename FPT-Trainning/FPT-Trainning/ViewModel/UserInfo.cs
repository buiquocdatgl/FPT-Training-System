using FPT_Trainning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Trainning.ViewModel
{
    public class UserInfo
    {
        public Trainer trainer { get; set; }
        public Trainee trainee { get; set; }
        public ApplicationUser user { get; set; }

    }
}