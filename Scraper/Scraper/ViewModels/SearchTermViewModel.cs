using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Presentation.ViewModels
{
    public class SearchTermViewModel : ViewModelBase//, INotifyDataErrorInfo
    {
        private string _searchTerm = "conveyancing software";
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(SearchTerm), "Search Term is required");
                    OnErrorsChanged(nameof(SearchTerm));
                }
                else
                {
                    _searchTerm = value;
                    OnPropertyChanged(nameof(SearchTerm));

                    _propertyErrors[nameof(SearchTerm)].Clear();
                    OnErrorsChanged(nameof(SearchTerm));
                }
            }
        }

        private string _searchURL = "www.smokeball.com";
        public string SearchURL
        {
            get { return _searchURL; }
            set
            {
                _searchURL = value;
                OnPropertyChanged(nameof(SearchURL));
            }
        }

        private Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        //protected ObservableCollection<string> errors = new ObservableCollection<string>();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public int Result { get; set; }

        public bool HasErrors => _propertyErrors.Any(e => e.Value.Count() > 0);

        //Command DoSearch() { }


        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName);
        }

        public void AddError(string propertyName, string error)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }
            _propertyErrors[propertyName].Add(error);
        }

        private void OnErrorsChanged(string propertyname)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyname));
        }
    }
}
