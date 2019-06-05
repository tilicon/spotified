namespace WhatNext.Communication.Web.Spotify.Contracts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    public class SpotifyClientHandler : HttpClientHandler
    {
        private readonly HttpClient _httpClient;
        private DateTime _tokenExpiry;
        private AuthenticationHeaderValue _authenticationHeaderValue;
        private readonly string _tokenEndpoint;

        public SpotifyClientHandler(string authority, string tokenEndpoint, string tokenType, string clientSecret)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(authority),
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, clientSecret);
            _tokenEndpoint = tokenEndpoint;
        }

        [ExcludeFromCodeCoverage]
        protected override async Task<HttpResponseMessage> SendAsync([NotNull]HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_tokenExpiry < DateTime.UtcNow)
            {
                var uri = new Uri(_httpClient.BaseAddress, _tokenEndpoint);
                var formContent = new[] {new KeyValuePair<string, string>("grant_type", "client_credentials")};

                var response = await _httpClient.PostAsync(uri, new FormUrlEncodedContent(formContent), cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseProperties = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                    var ttl = int.Parse(responseProperties["expires_in"].ToString());
                    var tokenType = responseProperties["token_type"].ToString();
                    var token = responseProperties["access_token"].ToString();
                    _tokenExpiry = DateTime.UtcNow.AddSeconds(ttl).AddMinutes(-1);

                    _authenticationHeaderValue = new AuthenticationHeaderValue(tokenType, token);
                }
            }
            request.Headers.Authorization = _authenticationHeaderValue;
            return await base.SendAsync(request, cancellationToken);
        }

    }
}