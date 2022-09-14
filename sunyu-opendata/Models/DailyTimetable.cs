using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyu_opendata.Models
{
    public class DailyTimetable
    {
        public string TrainDate { get; set; }

        public DailyTrain DailyTrainInfo { get; set; }

        public DateTime UpdateTime { get; set; }

        public List<StopTime> StopTimes { get; set; }

        public int VersionID { get; set; }
    }
}
