namespace WhatNext.Communication.Web.Services
{
    using Contracts.Services;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebApiService : IWebApiService
    {
        private readonly HttpClient _httpClient;
        private readonly UriBuilder _uriBuilder;
        private bool _isDisposed;

        public WebApiService(Uri apiBaseUri, HttpClient httpClient)
        {
            _uriBuilder = new UriBuilder(apiBaseUri ?? throw new ArgumentNullException(nameof(apiBaseUri)));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<T> GetAsync<T>(string path, string query = "", CancellationToken cancellationToken = default)
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
