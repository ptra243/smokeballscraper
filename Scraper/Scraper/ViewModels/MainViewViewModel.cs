using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Presentation.ViewModels
{
    public class MainViewViewModel : ViewModelBase
    {
        public SearchViewModel SearchViewModel { get; set; }
        public MainViewViewModel(SearchViewModel model)
        {
            SearchViewModel = model;
        }

    }
}
