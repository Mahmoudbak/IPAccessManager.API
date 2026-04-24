using IPAccessManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.Services.Content
{
    public interface IIpValidationService
    {
        Task<BlockAttemptLog> ValidateIpAndLogAttemptAsync(string ipAddress, string userAgent);
    }
}
