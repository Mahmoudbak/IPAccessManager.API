using IPAccessManager.API.Dtos;
using IPAccessManager.Core.Entity;
using IPAccessManager.Core.Repository.contrent;
using IPAccessManager.Core.specifications.BlockSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPAccessManager.API.Controllers
{
    public class CountriesController(IGenaricrepository<BlockedCountry> countryRepo) : BaseApiController
    {
        [HttpPost("block")]
        public async Task<IActionResult> BlockCountry([FromBody] string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return BadRequest("Country code is required.");

            countryCode = countryCode.ToUpper(); 

            
            var existingCountry = await countryRepo.GetByIdAsync(countryCode);
            if (existingCountry != null)
                return Conflict("Country is already blocked."); 

            var newCountry = new BlockedCountry { 
                Id=countryCode,
                CountryCode = countryCode };
            await countryRepo.AddAsync(newCountry);

            return Ok(new { Message = "Country blocked successfully.", CountryCode = countryCode });
        }


        [HttpDelete("block/{countryCode}")]
        public async Task<IActionResult> UnblockCountry(string countryCode)
        {
            countryCode = countryCode.ToUpper();

            var existingCountry = await countryRepo.GetByIdAsync(countryCode);
            if (existingCountry == null)
                return NotFound("Country is not blocked."); 

            await countryRepo.DeleteAsync(countryCode);

            return Ok(new { Message = "Country unblocked successfully." });
        }


        [HttpGet("blocked")]
        public async Task<IActionResult> GetBlockedCountries([FromQuery] BlockedCountrySpecParams countryParams)
        {
           
            var spec = new BlockedCountryWithParamsSpec(countryParams);
            var countries = await countryRepo.GetAllWithSpecAsync(spec);

            return Ok(countries);
        }

        [HttpPost("temporal-block")]
        public async Task<IActionResult> TemporalBlock([FromBody] TemporalBlockDto request)
        {
           
            if (request.DurationMinutes < 1 || request.DurationMinutes > 1440)
                return BadRequest("Duration must be between 1 and 1440 minutes.");

            var countryCode = request.CountryCode.ToUpper();

            
            var existingCountry = await countryRepo.GetByIdAsync(countryCode);
            if (existingCountry != null)
                return Conflict("Country is already blocked.");

            var newCountry = new BlockedCountry
            {
                Id = countryCode,
                CountryCode = countryCode,
                ExpiryDate = DateTimeOffset.UtcNow.AddMinutes(request.DurationMinutes)
            };

            await countryRepo.AddAsync(newCountry);

            return Ok(new
            {
                Message = "Country temporarily blocked.",
                Expiry = newCountry.ExpiryDate
            });
        }
       
    }
}
