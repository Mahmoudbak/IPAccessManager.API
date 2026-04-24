using IPAccessManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.specifications.BlockSpec
{
    public class BlockedCountrySpecParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public string? Sort { get; set; }

    }
}
