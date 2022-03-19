using NUnit.Framework;
using Scraper.Domain.Interfaces;

namespace Scraper.Domain.Services.Test
{
    public class Tests
    {
        private IScraperService _searchService { get; set; }
        [SetUp]
        public void Setup()
        {
            //_searchService = new ScraperService();
        }

        [Test]
        public void SearchTest(string searchTerm, string url, int expectedResult)
        {
            Assert.Pass();
        }
    }
}