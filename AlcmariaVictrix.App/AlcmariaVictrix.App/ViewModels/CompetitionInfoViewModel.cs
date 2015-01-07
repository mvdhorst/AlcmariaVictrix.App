using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.ViewModels;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class CompetitionInfoViewModel : ViewModelBase
    {
        private string _competitionName;
        private string _teamName;
        public string Name { 
            get { return _competitionName; }
            set { SetProperty(ref _competitionName, value); } 
        }

        public string TeamName
        {
            get { return _teamName; }
            set { SetProperty(ref _teamName, value); }
        }
    }
}
