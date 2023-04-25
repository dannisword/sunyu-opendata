using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sunyu_opendata.Entities
{

    [Table("JUDoc")]
    public class JUDoc
    {
        [Key]
        public int ID { get; set; }

        public string ListDate { get; set; }

        public string Attachments { get; set; }

        public string JFullX { get; set; }

        public string JID { get; set; }

        public string JYear { get; set; }

        public string JCase { get; set; }

        public string JNO { get; set; }

        public string JDate { get; set; }

        public string JTitle { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUser { get; set; }

        public DateTime ModifyTime { get; set; }

        public int ModifyUser { get; set; }

    }
}