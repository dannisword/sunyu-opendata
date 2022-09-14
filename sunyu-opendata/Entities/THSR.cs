using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sunyu_opendata.Entities
{
    [Table("THSR")]
    public class THSR
    {
        [Key]
        public int Seq { get; set; }

        public byte Direction { get; set; }

        public string CarNo { get; set; }

        public string StartStationName { get; set; }

        public string DepartureTime { get; set; }

        public string EndingStationName { get; set; }

        public string ArrivalTime { get; set; }

        public string Memo { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUser { get; set; }

        public DateTime ModifyTime { get; set; }

        public int ModifyUser { get; set; }

    }
}