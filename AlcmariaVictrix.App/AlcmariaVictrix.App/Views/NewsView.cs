using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.Views
{
    public class NewsView : ContentPage
    {
        public NewsView()
        {
                Title = "Nieuws";
                Content = new StackLayout
                {
                    Children = {
                    new BoxView { Color = Color.Blue },
                    new BoxView { Color = Color.Red}
                }
                };
        }
    }
}
