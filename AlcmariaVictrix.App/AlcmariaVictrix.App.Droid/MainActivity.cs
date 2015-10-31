using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using AlcmariaVictrix.Shared;
using Acr.UserDialogs;
using XLabs.Forms;

namespace AlcmariaVictrix.Droid
{
    [Activity(Label = "Alcmaria Victrix", Icon = "@drawable/ic_launcher", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity // XFormsApplicationDroid // 
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            LoadApplication(new AlcmariaVictrixApp());
        }
    }
}

