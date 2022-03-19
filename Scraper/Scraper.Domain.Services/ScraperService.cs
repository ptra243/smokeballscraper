using Microsoft.Extensions.Logging;
using Scraper.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Scraper.Domain.Services
{
    public class ScraperService : IScraperService
    {
        private ILogger<ScraperService> _logger { get; set; }
        public ScraperService(ILogger<ScraperService> logger)
        {
            _logger = logger;
        }
        public async Task<int> SearchURLForSearchTerm(string searchterm, string url)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();

            var googleurl = $"https://www.google.com.au/search?num=100&q={sanitiseSearchTerms(searchterm)}";
            _logger.LogInformation("Google url: " + googleurl);
            HttpResponseMessage request = await httpClient.GetAsync(googleurl);
            //Cancel if requested and get html page
            cancellationToken.Token.ThrowIfCancellationRequested();
            string response = await request.Content.ReadAsStringAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            _logger.LogInformation(response);
            //TODO: replace with htmlagility pack
            //Regex for anchor tags with href
            var matches = Regex.Matches(response, "<a[^>]*href=\"([^\"]*)");
            return matches.Count(m => m.Value.Contains(url));
        }

        private string sanitiseSearchTerms(string searchterm)
        {
            //get rid of any special characters
            searchterm = Regex.Replace(searchterm, @"[^0-9a-zA-Z]+", "");
            //spaces should be replaced with +
            searchterm.Replace(' ', '+');
            return searchterm;
        }
    }
}