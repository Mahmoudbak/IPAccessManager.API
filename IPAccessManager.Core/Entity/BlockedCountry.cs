using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.Entity
{
    public class BlockedCountry:BaseEntity
    {
        public BlockedCountry()
        {
            Id = CountryCode;
        }
        public required string CountryCode { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
    }
}
