using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.Views
{
    public class ResultsView : ContentPage
    {
        public ResultsView()
        {
            Title = "Uitslagen";
            Content = new StackLayout
            {
                Children = {
					new Label { Text = "Hello ContentPage" }
				}
            };
        }
    }
}
