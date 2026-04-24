using IPAccessManager.Core.Entity;
using IPAccessManager.Core.Repository.contrent;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Services.TemporalBlockCleanupService
{
    public class TemporalBlockCleanupService : BackgroundService
    {

        private readonly IGenaricrepository<BlockedCountry> _countryRepo;

        public TemporalBlockCleanupService(IGenaricrepository<BlockedCountry> countryRepo)
        {
            _countryRepo = countryRepo;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));

           
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                
                var allCountries = await _countryRepo.GetAllAsync();

                
                var expiredCountries = allCountries
                    .Where(c => c.ExpiryDate.HasValue && c.ExpiryDate.Value <= DateTime.UtcNow)
                    .ToList();

               
                foreach (var country in expiredCountries)
                {
                    await _countryRepo.DeleteAsync(country.Id);
                   
                }
            }
        }
    }
}
