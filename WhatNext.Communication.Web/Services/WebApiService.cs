namespace WhatNext.Communication.Web.Services
{
    using Contracts.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebApiService : IWebApiService
    {
        private HttpClient _httpClient;
        private UriBuilder _uriBuilder;
        private bool _isDisposed;

        public WebApiService(Uri apiBaseUri)
        {
            InitializeWebApiService(apiBaseUri);
        }

        private void InitializeWebApiService(Uri apiBaseUri)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = apiBaseUri
            };
            _uriBuilder = new UriBuilder(apiBaseUri);
        }

        public async Task<T> GetAsync<T>(string path, string query = "", CancellationToken cancellationToken = default(CancellationToken))
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

        private T GenerateWebResult<T>(string data, HttpResponseMessage result)
        {
            result.EnsureSuccessStatusCode();

            return typeof(T) == typeof(string)
                ? (T) Convert.ChangeType(data, typeof(string))
                : GetDataObject<T>(data);
        }

        private static T GetDataObject<T>(string data)
        {
            var dataObject = JsonConvert.DeserializeObject<T>(data);
            return dataObject;
        }

        public Task<T> PostJsonAsync<T>(string path, object dataObject, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<T> PostFormAsync<T>(string path, IEnumerable<KeyValuePair<string, string>> dataObject, CancellationToken cancellationToken)
        {
            var requestUri = MakeUri(path, null);
            var content = dataObject != null ? new FormUrlEncodedContent(dataObject) : null;
            var result = await _httpClient.PostAsync(requestUri, content, cancellationToken);
            var data = await result.Content.ReadAsStringAsync();

            return GenerateWebResult<T>(data, result);
        }

        public void SetAuthorizationHeader(string tokenType, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _isDisposed) return;

            _httpClient?.Dispose();
            _isDisposed = true;
        }
    }
}