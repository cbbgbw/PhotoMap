using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Newtonsoft.Json;
using PhotoMap.Dto.Constants;
using PhotoMap.Dto.Models;

namespace PhotoMap.Mobile.Services
{
    public class RestService
    {
        private HttpClient client;
        public UserAuthResponse User;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<Guid> PostPhotoAsync(PhotoInsertModel photo)
        {
            Uri uri = new Uri(AppConstants.PostPhotoUrl);

            string json = JsonConvert.SerializeObject(photo);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            string respondContent = await response.Content.ReadAsStringAsync();
            //HttpResponseMessage response = await client.PostAsync();
            return Guid.Empty;
        }

        /// <summary>
        /// https://docs.microsoft.com/pl-pl/xamarin/xamarin-forms/data-cloud/web-services/rest
        /// </summary>
        /// <returns></returns>
        public async Task PostAuthUserAsync()
        {
            Uri uri = new Uri(AppConstants.AccountAuthUrl);
            User authUser = new User("adam.nowak","aaaa");
            string json = JsonConvert.SerializeObject(authUser);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string respondContent = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<UserAuthResponse>(respondContent);
            }
            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", User.Token);
        }
    }
}