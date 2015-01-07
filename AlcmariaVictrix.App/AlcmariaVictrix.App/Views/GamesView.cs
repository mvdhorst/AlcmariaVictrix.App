using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.Views
{
    public class GamesView : ContentPage
    {
        public GamesView()
        {       
            Title = "Wedstrijden";
            Content = new BoxView
            {
                Color = Color.Blue,
                HeightRequest = 100f,
                VerticalOptions = LayoutOptions.Center
            };
        }
    }
}
