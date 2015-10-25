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
            builder.RegisterType<MainVM>()
                .SingleInstance();

            builder.RegisterType<CompetitionsViewModel>()
                .SingleInstance();

            builder.RegisterType<CompetitionViewModel>();
            builder.RegisterType<GameViewModel>();

            builder.RegisterType<GamesViewModel>();

            builder.RegisterType<CompetitionInfoViewModel>()
                .SingleInstance();

            builder.RegisterType<NewsViewModel>().SingleInstance();
            builder.RegisterType<NewsItemViewModel>();


            // view registration
            builder.RegisterType<MainView>()
                .SingleInstance();

            builder.RegisterType<GamesView>();

            builder.RegisterType<CompetitionsView>()
                .SingleInstance();

            builder.RegisterType<CompetitionInfoView>();

            builder.RegisterType<NewsView>().SingleInstance();
            builder.RegisterType<NewsItemView>();


            // current page resolver
            builder.RegisterInstance<Func<Page>>(() =>
                ((NavigationPage)Application.Current.MainPage).CurrentPage);
        }
    }
}
