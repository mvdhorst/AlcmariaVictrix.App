using AlcmariaVictrix.Shared.Services;
using AlcmariaVictrix.Shared.ViewModels;
using AlcmariaVictrix.Shared.Views;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.XamForms.UserDialogs;

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
            // view registration
            builder.RegisterType<MainView>()
                .SingleInstance();

            builder.RegisterType<GamesView>();

            builder.RegisterType<CompetitionsView>()
                .SingleInstance();

            builder.RegisterType<CompetitionInfoView>()
                .SingleInstance();

            builder.RegisterType<NewsView>().SingleInstance();

            builder.RegisterType<NewsItemViewModel>();

            // current page resolver
            builder.RegisterInstance<Func<Page>>(() =>
                ((NavigationPage)Application.Current.MainPage).CurrentPage);
        }
    }
}
