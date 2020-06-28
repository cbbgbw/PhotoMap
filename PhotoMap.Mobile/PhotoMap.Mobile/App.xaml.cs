using System;
using PhotoMap.Dto.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoMap.Mobile.Services;
using PhotoMap.Mobile.Views;

namespace PhotoMap.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<RestService>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
