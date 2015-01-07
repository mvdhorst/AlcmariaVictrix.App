using System;
using System.ComponentModel;

namespace WebMolen.Mobile.Core.Interfaces
{
    public interface INavigationAware
    {
        void NavigatedTo();

        void NavigatedFrom();
    }
}
