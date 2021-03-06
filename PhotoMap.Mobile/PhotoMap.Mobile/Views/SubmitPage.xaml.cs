﻿using System;
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

        private readonly RestService _restService;

        public SubmitPage(MediaFile photoFile)
        {
            _restService = DependencyService.Get<RestService>();
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
                            $"Latitude: {_location.Latitude}, Longitude: {_location.Longitude}");
                        ArtUploader.IsEnabled = true;
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
            ArtUploader.IsEnabled = false;
            ArtUploader.Text = "Uploading...";
            PhotoModel model = new PhotoModel { PhotoRowguid = Guid.NewGuid() };
            await _restService.PostAuthUserAsync();
            var account = CloudStorageAccount.Parse(_restService.User.BlobAzureKey);
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
            model.UserRowguid = _restService.User.UserROWGUID;
            await _restService.PostPhotoAsync(model);
            ArtUploader.Text = "Done!";
            await Navigation.PopAsync(true);
        }
    }
}