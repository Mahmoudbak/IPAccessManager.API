using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.specifications.IpLogSpec
{
    public class BlockAttemptLogSpecParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int PageIndex { get; set; } = 1;
    }
}
