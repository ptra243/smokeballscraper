using Microsoft.Extensions.Logging;
using Scraper.Domain.Interfaces;

namespace Scraper.Domain.Services
{
    public class ScraperService : IScraperService
    {
        private ILogger _logger { get; set; }
        public ScraperService(ILogger logger)
        {
            _logger = logger;
        }
        public int SearchURLForSearchTerm(string searchterm, string url)
        {
            throw new NotImplementedException();
        }
    }
}