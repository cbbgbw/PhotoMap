using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using PhotoMap.Dto.Models;
using PhotoMap.Mobile.Services;
using PhotoMap.Mobile.Views;

namespace PhotoMap.Mobile.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string login;

        public bool isAuthorized = false;

        private readonly RestService _restService;

        public LoginViewModel()
        {
            _restService = DependencyService.Get<RestService>();
            SubmitCommand = new Command(OnSubmit);
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Login"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        //We will use the ICommand interface that allows defining and implementing a command what we call commanding
        public ICommand SubmitCommand { protected set; get; }

        public async void OnSubmit()
        {
            try
            {
                await _restService.PostAuthUserAsync(login, password);
                isAuthorized = true;
            }
            catch
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
