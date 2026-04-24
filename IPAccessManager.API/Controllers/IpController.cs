using IPAccessManager.Core.Entity;
using IPAccessManager.Core.Repository.contrent;
using IPAccessManager.Core.Services.Content;
using IPAccessManager.Core.specifications.IpLogSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPAccessManager.API.Controllers
{
    public class IpController(
        IIpValidationService ipValidationService,
        IGeoLocationService geoLocationService,
        IGenaricrepository<BlockAttemptLog> logRepo) : BaseApiController
    {
        [HttpGet("lookup")]
        public async Task<IActionResult> LookupIp([FromQuery] string? ipAddress)
        {
            ipAddress ??= HttpContext.Connection.RemoteIpAddress?.ToString();


            if (ipAddress == "::1" || ipAddress == "127.0.0.1")
            {
                ipAddress = "156.193.0.0"; // IP حقيقي للتجربة Ex 8.8.8.8 faceBook US
            }


            if (string.IsNullOrEmpty(ipAddress))
                return BadRequest("Could not determine IP Address.");

            var countryCode = await geoLocationService.GetCountryCodeFromIpAsync(ipAddress);

            if (countryCode == null)
                return NotFound("Could not retrieve country for this IP.");

            return Ok(new { IpAddress = ipAddress, CountryCode = countryCode });
        }




       
        [HttpGet("check-block")]
        public async Task<IActionResult> CheckBlock()
        {
           
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";


            if (ipAddress == "::1" || ipAddress == "127.0.0.1")
            {
                ipAddress = "156.193.0.0"; 
            }


            var userAgent = Request.Headers["User-Agent"].ToString();

            
            var log = await ipValidationService.ValidateIpAndLogAttemptAsync(ipAddress, userAgent);

            if (log.BlockedStatus)
                return StatusCode(403, new { Message = "Access Denied. Your country is blocked.", Details = log });

            return Ok(new { Message = "Access Granted.", Details = log });
        }

        
        [HttpGet("~/api/logs/blocked-attempts")] 
        public async Task<IActionResult> GetBlockedAttempts([FromQuery] BlockAttemptLogSpecParams logParams)
        {

          
            var spec = new BlockAttemptLogSpec(logParams);

            
            var pagedLogs = await logRepo.GetAllWithSpecAsync(spec);

            
            var allLogsCount = (await logRepo.GetAllAsync()).Count;

            return Ok(new
            {
                TotalRecords = allLogsCount,
                Page = logParams.PageIndex,
                PageSize = logParams.PageSize,
                Data = pagedLogs
            });
        }

    }
}
