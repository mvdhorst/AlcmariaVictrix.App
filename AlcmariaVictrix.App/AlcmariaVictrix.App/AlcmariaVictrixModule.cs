using AlcmariaVictrix.Shared.Services;
using AlcmariaVictrix.Shared.ViewModels;
using AlcmariaVictrix.Shared.Views;
using Autofac;
using System;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared
{
    public class AlcmariaVictrixModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // service registration
            builder.RegisterType<GameService>()
                .As<IGameService>()
                .SingleInstance();

            // view model registration
            //builder.RegisterType<MainVM>()
            //    .SingleInstance();

            builder.RegisterType<CompetitionInfoViewModel>()
                .SingleInstance();

            builder.RegisterType<CompetitionsViewModel>()
                .SingleInstance();

            builder.RegisterType<CompetitionViewModel>();

            builder.RegisterType<GameViewModel>();
            builder.RegisterType<GamesViewModel>();
            
            builder.RegisterType<NewsItemViewModel>();
            builder.RegisterType<NewsViewModel>().SingleInstance();

            builder.RegisterType<ResultsViewModel>().SingleInstance();

            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<MenuPageViewModel>().SingleInstance();
            builder.RegisterType<RootPageViewModel>().SingleInstance();
            builder.RegisterType<MenuPageViewModel>().SingleInstance();



            // view registration
            //builder.RegisterType<MainView>()
            //    .SingleInstance();

            builder.RegisterType<GamesView>();

            builder.RegisterType<CompetitionsView>()
                .SingleInstance();

            builder.RegisterType<CompetitionInfoView>();

            builder.RegisterType<NewsView>().SingleInstance();
            builder.RegisterType<NewsItemView>();

            builder.RegisterType<ResultsView>().SingleInstance();

            builder.RegisterType<MainView>().SingleInstance();
            builder.RegisterType<MenuPageView>().SingleInstance();
            builder.RegisterType<RootPageView>().SingleInstance();


            // current page resolver
            //builder.RegisterInstance<Func<Page>>(() =>
            //    ((NavigationPage)Application.Current.MainPage).CurrentPage);

            // default page resolver
            builder.RegisterInstance<Func<Page>>(() =>
            {
                // Check if we are using MasterDetailPage
                var masterDetailPage = Application.Current.MainPage as MasterDetailPage;

                var page = masterDetailPage != null
                    ? masterDetailPage.Detail
                    : Application.Current.MainPage;

                // Check if page is a NavigationPage
                var navigationPage = page as IPageContainer<Page>;

                return navigationPage != null
                    ? navigationPage.CurrentPage
                        : page;
            });
        }
    }
}
