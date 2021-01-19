using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WorkingPlace { get; set; }
        public string Type { get; set; }
    }
}