using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMap.Common.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoMap.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubmitPage : ContentPage
    {
        public PhotoMapItem PhotoMapItem { get; set; }



        public SubmitPage()
        {
            InitializeComponent();
            ArtUploader.Clicked += ArtUploader_Clicked;
        }



        async void ArtUploader_Clicked(object sender, EventArgs args)
        {

        }
    }
}