using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.Services.Content
{
    public interface IGeoLocationService
    {
      public  Task <string?> GetCountryCodeFromIpAsync(string ipAddress);
    }
}
