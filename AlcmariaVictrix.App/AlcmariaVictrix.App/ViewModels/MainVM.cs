using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.XamForms.UserDialogs;
using WebMolen.Mobile.Core.Factories;
using WebMolen.Mobile.Core.ViewModels;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private readonly IUserDialogService _dialogService;
        private IEnumerable<Page> _views;

        public IEnumerable<Page> Views
        {
            get { return _views; }
            set { SetProperty(ref _views, value); }
        }


        public MainVM(
            IUserDialogService dialogService)
        {
            this._dialogService = dialogService;

        Title = "Alcmaria Victrix";
        TodayQuickGameInfo = "Vandaag geen wedstrijden";


    }
 
 
    public string TodayQuickGameInfo { get; set; }
    }
}
