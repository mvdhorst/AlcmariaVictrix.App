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
using Acr.XamForms.UserDialogs;

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
            RegisterXamService<IUserDialogService>(builder);
            builder.RegisterModule<AlcmariaVictrixModule>();

        }

        protected override void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<CompetitionsViewModel, CompetitionsView>();
            viewFactory.Register<MainVM, MainView>();
            viewFactory.Register<GamesViewModel, GamesView>();
            viewFactory.Register<CompetitionInfoViewModel, CompetitionInfoView>();
            viewFactory.Register<NewsViewModel, NewsView>();
            //viewFactory.Register<ForecastReportViewModel, ForecastReportView>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            // set main page
            var viewFactory = container.Resolve<IViewFactory>();
            //var mainPage = viewFactory.Resolve<GamesViewModel>();
            var mainPage = viewFactory.Resolve<MainVM>();
            var navigationPage = new NavigationPage(mainPage);

            Color backgroundColor = (Color)_application.Resources["backgroundColor"];
            Color textColor = (Color)_application.Resources["textColor"];

            navigationPage.BarBackgroundColor = backgroundColor;
            navigationPage.BarTextColor = textColor;
            navigationPage.BackgroundColor = backgroundColor;

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
