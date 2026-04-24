using IPAccessManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.specifications.IpLogSpec
{
    public class BlockAttemptLogSpec : BaseSpecifcations<BlockAttemptLog>
    {
        public BlockAttemptLogSpec(BlockAttemptLogSpecParams logParams)
            : base()
        {
            AddOrderByDesc(x => x.Timestamp);

            
            ApplyPagination(logParams.PageSize * (logParams.PageIndex - 1), logParams.PageSize);
        }
    }
}
