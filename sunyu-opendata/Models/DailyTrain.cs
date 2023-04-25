using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyu_opendata.Models
{
    public class DailyTrain
    {
        public string TrainNo { get; set; }

        public byte Direction { get; set; }

        public string StartingStationID { get; set; }

        public StationName StartingStationName { get; set; }

        public string EndingStationID { get; set; }

        public StationName EndingStationName { get; set; }

        //public string Note { get; set; }
    }
}
