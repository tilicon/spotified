namespace WhatNext.Communication.Web.Spotify.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts.Models;
    using Contracts.Services;
    using Exceptions;
    using Web.Contracts.Services;
    using WhatNext.Communication.Web.Services;

    public class SpotifyService : ISpotifyService
    {
        //private readonly IWebApiService _authorizationService;
        private readonly IWebApiService _libraryService;


        private AuthorizationResponse _authorizationResponse = null;

        public SpotifyService()
        {
            //_authorizationService = new WebApiService(new Uri("https://accounts.spotify.com/"));
            _libraryService = new WebApiService(new Uri("https://api.spotify.com/"));
        }

        public async Task AuthorizeAsync(CancellationToken cancellationToken)
        {
            if (_authorizationResponse != null) return;

            var authorizationUri = new Uri("https://accounts.spotify.com/");
            using (var authorizationService = new WebApiService(authorizationUri))
            {
                authorizationService.SetAuthorizationHeader("Basic", "996d0037680544c987287a9b0470fdbb:5a3c92099a324b8f9e45d77e919fec13");
                var formContent = new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") };
                var response = await authorizationService.PostFormAsync<AuthorizationResponse>(ApiCalls.AuthorizationPath, formContent, cancellationToken);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    _authorizationResponse = null;
                    throw new AuthorizationException(response.StatusCode.ToString("G"));
                }

                _libraryService.SetAuthorizationHeader(response.Data.TokenType, response.Data.AccessToken);
                _authorizationResponse = response.Data;
            }
        }

        public async Task<IEnumerable<string>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            await AuthorizeAsync(cancellationToken);

            var response = await _libraryService.GetAsync<CategoryResponse>(ApiCalls.CategoriesPath, ApiCalls.CategoriesQuery, cancellationToken);

            return response.Data.CategoriesInformation.Categories.Select(c => c.Name);
        }
    }
}