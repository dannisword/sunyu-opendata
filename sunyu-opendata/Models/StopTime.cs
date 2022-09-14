using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyu_opendata.Models
{
    public class StopTime
    {
        public int StopSequence { get; set; }

        public string StationID { get; set; }

        public StationName StationName { get; set; }

        public string ArrivalTime { get; set; }

        public string DepartureTime { get; set; }
    }
}
