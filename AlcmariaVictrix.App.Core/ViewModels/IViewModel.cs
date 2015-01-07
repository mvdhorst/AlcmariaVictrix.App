using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.Interfaces;

namespace WebMolen.Mobile.Core.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged, INavigationAware
    {        string Title { get; set; }
    }
}
