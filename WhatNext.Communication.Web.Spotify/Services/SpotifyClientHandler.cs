namespace WhatNext.Communication.Web.Spotify.Services
{
    using Newtonsoft.Json;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    public class SpotifyClientHandler : HttpClientHandler
    {
        private readonly HttpClient _authorizationClient;
        private DateTime _tokenExpiry;
        private AuthenticationHeaderValue _authenticationHeaderValue;
        private readonly string _tokenEndpoint;

        public SpotifyClientHandler(HttpClient authorizationClient, string tokenEndpoint, string tokenType, string clientSecret)
        {
            _authorizationClient = authorizationClient;
            _authorizationClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, clientSecret);
            _tokenEndpoint = tokenEndpoint;
        }

        protected override async Task<HttpResponseMessage> SendAsync([NotNull]HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_tokenExpiry < DateTime.UtcNow)
            {
                var uri = new Uri(_authorizationClient.BaseAddress, _tokenEndpoint);
                var formContent = new[] {new KeyValuePair<string, string>("grant_type", "client_credentials")};

                var response = await _authorizationClient.PostAsync(uri, new FormUrlEncodedContent(formContent), cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
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