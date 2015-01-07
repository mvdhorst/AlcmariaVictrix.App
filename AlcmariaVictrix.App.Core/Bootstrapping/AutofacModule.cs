using System;
using Autofac;
using Xamarin.Forms;
using WebMolen.Mobile.Core.Factories;
using WebMolen.Mobile.Core.Services;
using WebMolen.Mobile.Core.Interfaces;
using WebMolen.Mobile.Core.Views;

namespace WebMolen.Mobile.Core.Bootstrapping
{

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // service registration
            builder.RegisterType<DialogService>()
                .As<IDialogProvider>()
                .SingleInstance();

            builder.RegisterType<ViewFactory>()
                .As<IViewFactory>()
                .SingleInstance();

            builder.RegisterType<Navigator>()
                .As<INavigator>()
                .SingleInstance();

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
            }
            );

            // current PageProxy
            builder.RegisterType<PageProxy>()
                .As<IPage>()
                .SingleInstance();
        }
    }
}
