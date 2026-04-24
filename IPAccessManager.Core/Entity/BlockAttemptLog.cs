using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.Entity
{
    public class BlockAttemptLog:BaseEntity
    {
        public BlockAttemptLog()
        {
            Id= Guid.NewGuid().ToString();
        }
        public  string IpAddress { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public  string CountryCode { get; set; }
        public bool BlockedStatus { get; set; }
        public string UserAgent { get; set; } = string.Empty;
    }
}
