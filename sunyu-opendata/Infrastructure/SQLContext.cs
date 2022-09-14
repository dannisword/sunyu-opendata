using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using sunyu_opendata.Entities;

namespace sunyu_opendata.Infrastructure
{
    
    public class SQLContext : DbContext
    {
        public DbSet<THSR> THSRs { get; set; }
        public DbSet<CourtVerdict> CourtVerdicts { get; set; }
        public DbSet<JUList> JLists { get; set; }
        public DbSet<JUDoc> JDocs { get; set; }

        public SQLContext(string connectName) : base(connectName)
        {
            var connectStr = Database.Connection.ConnectionString;
        }
    }
}
