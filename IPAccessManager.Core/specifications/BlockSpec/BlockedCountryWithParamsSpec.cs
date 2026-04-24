using IPAccessManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.specifications.BlockSpec
{
    public class BlockedCountryWithParamsSpec:BaseSpecifcations<BlockedCountry>
    {
        public BlockedCountryWithParamsSpec(BlockedCountrySpecParams SpecParams)
        :base(c=>
        (string.IsNullOrEmpty(SpecParams.Search))||c.CountryCode.ToLower().Contains(SpecParams.Search))
        {
            if (!string.IsNullOrEmpty(SpecParams.Sort))
            {
                switch (SpecParams.Sort)
                {
                    case "codeAsc":
                        AddOrderBy(x => x.CountryCode);
                        break;

                    case "codeDesc":
                        AddOrderByDesc(x => x.CountryCode);
                        break;

                    default:
                        AddOrderBy(x => x.CountryCode); 
                        break;

                }
            }
            else
                AddOrderBy(x => x.CountryCode);
            ApplyPagination((SpecParams.PageIndex - 1) * SpecParams.PageSize, SpecParams.PageSize);
        }
    }
}
