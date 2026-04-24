using IPAccessManager.Core.Entity;
using IPAccessManager.Core.Repository.contrent;
using IPAccessManager.Core.Services.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Services.IpValidationService
{
    public class IpValidationService : IIpValidationService
    {
       
        private readonly IGeoLocationService _geoLocationService;
        private readonly IGenaricrepository<BlockedCountry> _countryRepo;
        private readonly IGenaricrepository<BlockAttemptLog> _logRepo;

        public IpValidationService(
            IGeoLocationService geoLocationService,
            IGenaricrepository<BlockedCountry> countryRepo,
            IGenaricrepository<BlockAttemptLog> logRepo)
        {
            _geoLocationService = geoLocationService;
            _countryRepo = countryRepo;
            _logRepo = logRepo;
        }
        public async Task<BlockAttemptLog> ValidateIpAndLogAttemptAsync(string ipAddress, string userAgent)
        {
            // 1.IP API
            var countryCode = await _geoLocationService.GetCountryCodeFromIpAsync(ipAddress) ?? "Unknown";

            // 2. If the Block Country
            var country = await _countryRepo.GetByIdAsync(countryCode);

            //   ExpiryDate Block 
            bool isBlocked = country != null &&
                             (!country.ExpiryDate.HasValue || country.ExpiryDate.Value > DateTimeOffset.UtcNow);

            // 3. Log
            var log = new BlockAttemptLog
            {
                IpAddress = ipAddress,
                CountryCode = countryCode,
                BlockedStatus = isBlocked,
                UserAgent = userAgent
            };

            // 4. In-Memory Repository
            await _logRepo.AddAsync(log);

            // 5. return
            return log;
        }
    }
}
