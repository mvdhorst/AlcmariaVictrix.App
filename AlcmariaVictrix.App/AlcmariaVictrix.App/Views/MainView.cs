using AlcmariaVictrix.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.Views
{
    public class MainView : TabbedPage
    {
        public MainView()
        {
            BindingContext = new MainVM();
            this.Title = "Alcmaria Victrix";
            this.Children.Add(new GamesView());
            this.Children.Add(new NewsView());
            this.Children.Add(new ResultsView());
        }
    }
}
