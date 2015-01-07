﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebMolen.Mobile.Core.Interfaces
{
    public interface IPage : IDialogProvider
    {
        INavigation Navigation { get; }
    }
}
