using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyu_opendata.Models
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string token_type { get; set; }
        public int notbeforepolicy { get; set; }
        public string scope { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", access_token, expires_in, scope);
        }
    }
}
