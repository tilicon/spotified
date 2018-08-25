namespace WhatNext.Communication.Web.Services
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using WhatNext.Communication.Web.Contracts.Models;
    using WhatNext.Communication.Web.Contracts.Services;

    public class WebApiService : IWebApiService
    {
        private HttpClient _httpClient;
        private UriBuilder _uriBuilder;
        private bool _isDisposed;

        public WebApiService(Uri apiBaseUri)
        {
            InitializeWebApiService(apiBaseUri);
        }

        public void InitializeWebApiService(Uri apiBaseUri)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = apiBaseUri
            };
            _uriBuilder = new UriBuilder(apiBaseUri);
        }

        public async Task<WebApiResult<T>> GetAsync<T>(string path, string query = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = MakeUri(path, query);
            var result = await _httpClient.GetAsync(uri, cancellationToken);
            var data = await result.Content.ReadAsStringAsync();

            return GenerateWebResult<T>(data, result);
        }
        
        private Uri MakeUri(string path, string query)
        {
            _uriBuilder.Path = path;
            _uriBuilder.Query = query;

            return _uriBuilder.Uri;
        }

        private WebApiResult<T> GenerateWebResult<T>(string data, HttpResponseMessage result)
        {
            var webResult = new WebApiResult<T> { StatusCode = result.StatusCode };
            if (result.IsSuccessStatusCode)
                webResult.Data = typeof(T) == typeof(string)
                    ? (T)Convert.ChangeType(data, typeof(T))
                    : GetDataObject(data, webResult);

            return webResult;
        }

        private static T GetDataObject<T>(string data, WebApiResult<T> webResult)
        {
            var dataObject = JsonConvert.DeserializeObject<T>(data);
            webResult.Data = dataObject;
            return dataObject;
        }

        public Task<WebApiResult<T>> PostJsonAsync<T>(string path, object dataObject, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<WebApiResult<T>> PostFormAsync<T>(string path, IEnumerable<KeyValuePair<string, string>> dataObject, CancellationToken cancellationToken)
        {
            var requestUri = MakeUri(path, null);
            var content = dataObject != null ? new FormUrlEncodedContent(dataObject) : null;
            var result = await _httpClient.PostAsync(requestUri, content, cancellationToken);
            var data = await result.Content.ReadAsStringAsync();

            return GenerateWebResult<T>(data, result);
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _httpClient?.Dispose();
            _isDisposed = true;
        }

        public void SetAuthorizationHeader(string tokenType, string accessToken)
        {
            var accessTokenB64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(accessToken));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessTokenB64);
        }
    }
}
