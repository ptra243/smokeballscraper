using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Scraper.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Scraper.Domain.Services.Test
{
    public class ScraperServiceTest : BaseTest
    {
        private IScraperService _searchService { get; set; }

        [SetUp]
        public void Setup()
        {
            _searchService = ServiceProvider.GetService<IScraperService>() ?? throw new NotImplementedException();
            //TODO: set up mock http client requests.
            //The actual results are out of our control and could change any time
            //so we should be mocking the http request
        }

        [Test]
        [TestCase("conveyancing software", "https://www.smokeball.com.au/", 1)]
        [TestCase("conveyancing software", "www.smokeball.com.au/", 1)]
        [TestCase("conveyancing software", "www.smokeball.com.au", 1)]
        [TestCase("Pizza", "https://www.dominos.com.au/", 3)]
        [TestCase("Pizza Hut", "https://www.dominos.com.au/", 2)]
        public async Task SearchTest(string searchTerm, string url, int expectedResult)
        {
            var result = await _searchService.SearchURLForSearchTerm(searchTerm, url);
            Assert.AreEqual(expectedResult, result);

        }

        [Test]
        [TestCase("", "https://www.smokeball.com.au/")]
        [TestCase("Pizza", "dominos")]
        public async Task SearchTestFail(string searchTerm, string url)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => { await _searchService.SearchURLForSearchTerm(searchTerm, url); });
        }
    }
}