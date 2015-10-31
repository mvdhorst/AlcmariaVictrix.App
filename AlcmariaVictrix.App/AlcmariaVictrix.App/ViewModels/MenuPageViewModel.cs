using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.Factories;
using WebMolen.Mobile.Core.ViewModels;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        IViewFactory _viewFactory;
        public List<MenuItem> MenuItems { get; private set; }
        public MenuPageViewModel(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            Title = "Menu";
            ShowDetail1Command = new Command(ShowDetail);
            ShowDetail2Command = new Command(ShowDetail2);
            MenuItems = new List<MenuItem>();
            MenuItem menuItem = new MenuItem() { Command = new Command(ShowDetail), Text = "Home", CommandParameter = _viewFactory.Resolve<MainViewModel>() };
            MenuItems.Add(menuItem);
            menuItem = new MenuItem() { Command = new Command(ShowDetail), Text = "Wedstrijden", CommandParameter = _viewFactory.Resolve<GamesViewModel>() };
            MenuItems.Add(menuItem);
            menuItem = new MenuItem() { Command = new Command(ShowDetail), Text = "Teams", CommandParameter = _viewFactory.Resolve<CompetitionsViewModel>() };
            MenuItems.Add(menuItem);
            menuItem = new MenuItem() { Command = new Command(ShowDetail), Text = "Nieuws", CommandParameter = _viewFactory.Resolve<NewsViewModel>() };
            MenuItems.Add(menuItem);
            OnPropertyChanged("MenuItems");
        }


        public Command ShowDetail1Command { get; set; }
        public void ShowDetail(object commandParameter)
        {
            Page page = commandParameter as Page;
            if (page == null)
                return;
            var mainPage = _viewFactory.Resolve<RootPageViewModel>();

            ((MasterDetailPage)mainPage).Detail = new NavigationPage(page);
        }

        public Command ShowDetail2Command { get; set; }
        public void ShowDetail2()
        {
            var mainPage = _viewFactory.Resolve<RootPageViewModel>();

            ((MasterDetailPage)mainPage).Detail = new NavigationPage(_viewFactory.Resolve<GamesViewModel>());
        }

    }
}
