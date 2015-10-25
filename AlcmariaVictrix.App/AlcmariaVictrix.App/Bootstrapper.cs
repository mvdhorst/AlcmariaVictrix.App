using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Autofac;
using AlcmariaVictrix.Shared.Views;
using WebMolen.Mobile.Core.Factories;
using AlcmariaVictrix.Shared.ViewModels;
using WebMolen.Mobile.Core.Bootstrapping;

namespace AlcmariaVictrix.Shared
{
    public class Bootstrapper : AutofacBootstrapper
    {
        private readonly Application _application;

        public Bootstrapper(Application application)
        {
            _application = application;
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterModule<AlcmariaVictrixModule>();

        }

        protected override void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<CompetitionsViewModel, CompetitionsView>();
            viewFactory.Register<MainVM, MainView>();
            viewFactory.Register<GamesViewModel, GamesView>();
            viewFactory.Register<CompetitionInfoViewModel, CompetitionInfoView>();
            viewFactory.Register<NewsViewModel, NewsView>();
            viewFactory.Register<NewsItemViewModel, NewsItemView>();
            //viewFactory.Register<ForecastReportViewModel, ForecastReportView>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            // set main page
            var viewFactory = container.Resolve<IViewFactory>();
            //var mainPage = viewFactory.Resolve<GamesViewModel>();
            var mainPage = viewFactory.Resolve<MainVM>();

            var tabbedPage = new TabbedPage();
            var gamesPage = viewFactory.Resolve<GamesViewModel>();
            var newsPage = viewFactory.Resolve<NewsViewModel>();
            var compPage = viewFactory.Resolve<CompetitionsViewModel>();

            tabbedPage.Children.Add(gamesPage);
            tabbedPage.Children.Add(newsPage);
            tabbedPage.Children.Add(compPage);

            var navigationPage = new NavigationPage(tabbedPage);

            Color backgroundColor = (Color)_application.Resources["backgroundColor"];
            Color textColor = (Color)_application.Resources["textColor"];

            navigationPage.BarBackgroundColor = backgroundColor;
            navigationPage.BarTextColor = textColor;
            navigationPage.BackgroundColor = backgroundColor;
            //navigationPage.Icon = 

            _application.MainPage = navigationPage;
        }

        private static void RegisterXamService<T>(ContainerBuilder builder) where T : class
        {
            builder
                .Register(x => DependencyService.Get<T>())
                .SingleInstance();
        }
    }
}
