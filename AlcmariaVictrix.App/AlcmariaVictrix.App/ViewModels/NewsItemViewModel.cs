using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AlcmariaVictrix.Shared.Models;
using WebMolen.Mobile.Core.Services;
using WebMolen.Mobile.Core.ViewModels;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class NewsItemViewModel: ViewModelBase
    {
        private static readonly string htmlStart = "<!doctype html><html lang=\"NL\"><head><base href=\"http://www.alcmariavictrix.nl/\" /><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head><body>";
        private static readonly string htmlEnd = "</body></html>";
        HtmlWebViewSource htmlSource;                
        private readonly INavigator _navigator;
        private FeedItem _feedItem;
        private readonly GameViewModel _gameViewModel;

        public FeedItem Item
        {
            get { return _feedItem; }
            set 
            { 
                SetProperty(ref _feedItem, value);
            }
        }

        public HtmlWebViewSource HtmlSource
        {
            get 
            { 
                htmlSource.Html = htmlStart + _feedItem.Description + htmlEnd;
                return htmlSource; 
            }
        }

        public NewsItemViewModel(
            FeedItem item, 
            INavigator navigator)
        {
            _feedItem = item;
            Title = _feedItem.Title;
            _navigator = navigator;

            htmlSource = new HtmlWebViewSource();
            WebView view = new WebView();
            htmlSource.BaseUrl = "http://www.alcmariavictrix.nl/";

            ShowItemCommand = new Command(ShowItem);
        }

        public ICommand ShowItemCommand { get; set; }

        private async void ShowItem()
        {
            await _navigator.PushAsync<NewsItemViewModel>(this);
        }
    }
}
