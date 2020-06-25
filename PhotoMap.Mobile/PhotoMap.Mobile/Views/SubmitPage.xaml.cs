using System;
using Microsoft.WindowsAzure.Storage;
using PhotoMap.Dto.Models;
using PhotoMap.Mobile.Services;
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
        private readonly MediaFile _photoFile;
        private Location _location;

        public RestService DataStore => DependencyService.Get<RestService>();

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
                    _location = await Geolocation.GetLocationAsync(request);

                    if (_location != null)
                    {
                        LocationLabel.Text = LocationLabel.Text.Replace("checking",
                            $"Latitude: {_location.Latitude}, Longitude: {_location.Longitude}, Altitude: {_location.Altitude}");
                    }
                    else
                    {
                        LocationLabel.Text = LocationLabel.Text.Replace("checking", "Wystąpił błąd");
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
            PhotoInsertModel model = new PhotoInsertModel{PhotoRowguid = Guid.NewGuid()};
            await DataStore.PostAuthUserAsync();
            var account = CloudStorageAccount.Parse(DataStore.User.BlobAzureKey);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("photomapcontainer");
            await container.CreateIfNotExistsAsync();
            var blockBlob = container.GetBlockBlobReference($"{model.PhotoRowguid}.png");

            await blockBlob.UploadFromStreamAsync(_photoFile.GetStream());

            model.Latitude = _location.Latitude.ToString();
            model.Title = NameText.Text;
            model.Description = DescText.Text;
            model.Longitude = _location.Longitude.ToString();
            model.PhotoPath = blockBlob.Uri.OriginalString;
            model.UserRowguid = DataStore.User.UserROWGUID;
            await DataStore.PostPhotoAsync(model);
        }
    }
}