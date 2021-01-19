using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Trainning.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public int Experience { get; set; }
        public string Education { get; set; }
        public string Location { get; set; }
        public int ToeicScore{get;set;}
        public string Department { get; set; }
    }
}