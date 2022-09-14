using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyu_opendata.Models
{
    public class Timetables
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string[] TrainDates { get; set; }
    }
}
