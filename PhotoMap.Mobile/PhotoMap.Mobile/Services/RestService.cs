using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Newtonsoft.Json;
using PhotoMap.DTO.Models;

namespace PhotoMap.Mobile.Services
{
    public class RestService
    {
        private HttpClient client;
        private string _token;
        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<Guid> PostPhotoAsync(PhotoInsertModel photo)
        {
            Uri uri = new Uri(DTO.Constants.Contstants.PostPhotoUrl);

            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
            //HttpResponseMessage response = await client.PostAsync();
            return Guid.Empty;
        }

        public async Task PostAuthUserAsync()
        {
            Uri uri = new Uri(DTO.Constants.Contstants.AuthUrl);
            User authUser = new User("adam.nowak","aaaa");
            string json = JsonConvert.SerializeObject(authUser);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
        }
    }
}