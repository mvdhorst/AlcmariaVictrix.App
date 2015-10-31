using AlcmariaVictrix.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.Factories;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.Views
{
    public class RootPageView : MasterDetailPage
    {
        public RootPageView(IViewFactory viewfactory)
        {
            Master = viewfactory.Resolve<MenuPageViewModel>();

            Detail = new NavigationPage(viewfactory.Resolve<GamesViewModel>());

        }
    }
}
