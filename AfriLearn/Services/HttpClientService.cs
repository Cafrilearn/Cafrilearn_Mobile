using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AfriLearn.Constants;
using AfriLearn.Dtos;
using AfriLearnMobile.Models;

namespace AfriLearn.Services
{
    class HttpClientService 
    {
        private protected HttpClient _httpClient;
        private protected HttpClientHandler _handler;
        private string _token;
        public HttpClientService()
        {

            _handler = new HttpClientHandler();
            _token = "";
            _handler.AllowAutoRedirect = false;
            _handler.Credentials = default;
            _httpClient = new HttpClient(_handler);
            _httpClient.BaseAddress = new Uri(HttpClientServiceConstants.BaseUri);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        }
                   
        public async Task<string> Get(string theUri)
        {
            var response = await _httpClient.GetAsync(theUri).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetById(int id, string theUri)
        {
            var response = await _httpClient.GetAsync(theUri + "/" + id.ToString()).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> Post(object objectToSend, string theUri)
        {
            var jsonDataUser = JsonConvert.SerializeObject(objectToSend);
            var httpContent = new StringContent(jsonDataUser);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync(theUri, httpContent);
            return response.Content.ReadAsStringAsync().Result;
        }
        public  async Task<string> UpDate(object objectToUpdate, string theUri)
        {
            var jsonDataUser = JsonConvert.SerializeObject(objectToUpdate);
            var httpcontent = new StringContent(jsonDataUser);
            httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PutAsync(theUri, httpcontent);
            return response.StatusCode.ToString();
        }
        public async Task<string> Delete(int id, string theUri)
        {
            var result = await _httpClient.DeleteAsync(theUri +"/" + id.ToString());
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return "Item deleted Succesfully";
            }
            else
            {
                return "Error, failed to delete the item";
            }
        }
        public  async Task<string> AuthenticateUser(string email, string password, AppUser appUser)
        {
            var userCred = new AuthDto()
            {
                UserName = appUser.Email,
                Password = appUser.PasswordHash
            };
            var jsonDataUser = JsonConvert.SerializeObject(userCred);
            var httpContent = new StringContent(jsonDataUser);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync("User/Authenticate", httpContent);
            return response.Content.ReadAsStringAsync().Result;
        }   
            
    }

 }
