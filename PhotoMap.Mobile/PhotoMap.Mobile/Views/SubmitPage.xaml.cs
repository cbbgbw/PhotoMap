using System;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Location = Xamarin.Essentials.Location;

namespace PhotoMap.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubmitPage : ContentPage
    {
        private MediaFile _photoFile;
        private Location _location;
        public SubmitPage(MediaFile photoFile)
        {
            _photoFile = photoFile;
            InitializeComponent();

            PhotoImage.Source = ImageSource.FromStream(() => _photoFile.GetStream());

            ArtUploader.Clicked += ArtUploader_Clicked;


        }

        protected override async void OnAppearing()
        {
            if (_location == null)
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        LocalisationLabel.Text = LocalisationLabel.Text.Replace("checking",
                            $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    }
                    else
                    {
                        LocalisationLabel.Text = LocalisationLabel.Text.Replace("checking", "Wystąpił błąd");
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
            }
            base.OnAppearing();
        }

        async void ArtUploader_Clicked(object sender, EventArgs args)
        {
            var account = CloudStorageAccount.Parse("");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("photomapcontainer");
            await container.CreateIfNotExistsAsync();
            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.png");

            await blockBlob.UploadFromStreamAsync(_photoFile.GetStream());
            var URL = blockBlob.Uri.OriginalString;
        }
    }
}