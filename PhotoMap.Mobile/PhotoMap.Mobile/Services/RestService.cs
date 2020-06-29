using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhotoMap.Dto.Constants;
using PhotoMap.Dto.Models;
using Xamarin.Essentials;

namespace PhotoMap.Mobile.Services
{
    public class RestService
    {
        public HttpClient client;
        public UserAuthResponse User;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<Guid> PostPhotoAsync(PhotoModel photo)
        {
            var uri = new Uri(AppConstants.PhotoUrl);

            var json = Task.Factory.StartNew(() => JsonConvert.SerializeObject(photo)).Result;
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            string respondContent = await response.Content.ReadAsStringAsync();
            return Guid.Parse(JsonConvert.DeserializeObject<string>(respondContent));
        }

        /// <summary>
        /// https://docs.microsoft.com/pl-pl/xamarin/xamarin-forms/data-cloud/web-services/rest
        /// </summary>
        /// <returns></returns>
        public async Task PostAuthUserAsync()
        {
            var uri = new Uri(AppConstants.AccountAuthUrl);
            var authUser = new User("adam.nowak", "aaaa");
            var json = Task.Factory.StartNew(() => JsonConvert.SerializeObject(authUser)).Result;
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string respondContent = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<UserAuthResponse>(respondContent);
            }
            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", User.Token);
        }

        public async Task<List<PhotoModel>> GetPhotosAsync()
        {
            var uri = new Uri(AppConstants.AccountAuthUrl);
            var response = await client.GetAsync(uri);
            string respondContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PhotoModel>>(respondContent);
        }
    }
}