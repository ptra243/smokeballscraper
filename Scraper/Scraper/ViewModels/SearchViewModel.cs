using Microsoft.Toolkit.Mvvm.Input;
using Scraper.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scraper.Presentation.ViewModels
{
    public class SearchViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private IScraperService _scraperService;

        public SearchViewModel(IScraperService service)
        {
            SEOSearch = new AsyncRelayCommand(DoSearch);
            _scraperService = service;
        }

        #region Properties
        #region SearchTerm
        private string _searchTerm = "conveyancing software";
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                ClearErrors(nameof(SearchTerm));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(SearchTerm), "Search Term is required");
                }
                OnPropertyChanged(nameof(SearchTerm));
            }
        }
        #endregion

        #region searchURL
        private string _searchURL = "www.smokeball.com";
        public string SearchURL
        {
            get { return _searchURL; }
            set
            {
               _searchURL = value;
                OnPropertyChanged(nameof(SearchURL));
                //Validation, needs to be a valid url
                ClearErrors(nameof(SearchURL));
                bool isurl = Uri.IsWellFormedUriString(!SearchURL.StartsWith("http")? "https://" + SearchURL : SearchURL, UriKind.Absolute);
                if (!isurl)
                {
                    AddError(nameof(SearchURL), "A valid URL to search for is required");
                }
                if (!isurl)
                {
                    AddError(nameof(SearchURL), "Please enter a valid URL to search for");
                }
            }
        }

        private Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        #endregion

        private int? _result = null;
        public int? Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(ResultMessage));
            }
        }
        public string ResultMessage
        {
            get
            {
                if (Result == null)
                {
                    return "";
                }
                else
                {
                    return $"There are {Result} results from the google search!";
                }
            }
        }
        #endregion

        #region Commands
        public ICommand SEOSearch { get; }


        public async Task DoSearch()
        {
            Result = await _scraperService.SearchURLForSearchTerm(SearchTerm, SearchURL);
        }
        #endregion

        #region Error Handling
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Any(e => e.Value.Count() > 0);

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return Enumerable.Empty<string>();
            return _propertyErrors.GetValueOrDefault(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors[propertyName].Clear();
            }
            OnErrorsChanged(propertyName);
        }

        public void AddError(string propertyName, string error)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }
            _propertyErrors[propertyName].Add(error);
            OnErrorsChanged(propertyName);

        }

        private void OnErrorsChanged(string propertyname)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyname));
        }
        #endregion
    }
}
