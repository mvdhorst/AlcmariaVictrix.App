using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AlcmariaVictrix.Shared
{
    public partial class AlcmariaVictrixApp : Application
    {
        public AlcmariaVictrixApp()
        {
            InitializeComponent();

            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
        }
    }
}
