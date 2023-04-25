using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace sunyu_opendata.Entities
{
    [Table("JUList")]
    public class JUList
    {
        [Key]
        public int ID { get; set; }

        public string ListDate { get; set; }

        public string ListItem { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUser { get; set; }

        public DateTime ModifyTime { get; set; }

        public int ModifyUser { get; set; }

    }
}
