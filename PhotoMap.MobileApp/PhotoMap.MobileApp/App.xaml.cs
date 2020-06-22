using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoMap.MobileApp.Services;
using PhotoMap.MobileApp.Views;

namespace PhotoMap.MobileApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
