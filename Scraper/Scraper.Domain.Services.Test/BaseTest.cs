using Microsoft.Extensions.DependencyInjection;
using Scraper.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Domain.Services.Test
{
    public class BaseTest
    {
        protected ServiceProvider ServiceProvider { get; set; }
        public BaseTest() {
            var services = new ServiceCollection();
            services.AddTransient<IScraperService, ScraperService>();
            services.AddLogging();
            ServiceProvider = services.BuildServiceProvider();

        }
    }
}
