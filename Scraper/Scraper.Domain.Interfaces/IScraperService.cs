
namespace Scraper.Domain.Interfaces
{
    public interface IScraperService
    {
        Task<int> SearchURLForSearchTerm(string searchterm, string url);

    }
}