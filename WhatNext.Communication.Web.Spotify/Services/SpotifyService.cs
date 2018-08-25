namespace WhatNext.Communication.Web.Spotify.Services
{
    using Contracts.Models;
    using Contracts.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Web.Contracts.Services;

    public class SpotifyService : ISpotifyService
    {
        private readonly IWebApiService _authorizationService;
        private readonly IWebApiService _libraryService;


        private AuthorizationResponse _authorizationResponse;

        public SpotifyService(IWebApiService libraryService, IWebApiAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
        }

        public async Task AuthorizeAsync(CancellationToken cancellationToken)
        {
            if (_authorizationResponse != null) return;
            _authorizationResponse = null;

            var formContent = new[] {new KeyValuePair<string, string>("grant_type", "client_credentials")};
            var response = await _authorizationService.PostFormAsync<AuthorizationResponse>(ApiCalls.AuthorizationPath, formContent, cancellationToken);

            _libraryService.SetAuthorizationHeader(response.TokenType, response.AccessToken);
            _authorizationResponse = response;
        }

        public async Task<IEnumerable<string>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            await AuthorizeAsync(cancellationToken);

            var response = await _libraryService.GetAsync<CategoryResponse>(ApiCalls.CategoriesPath, ApiCalls.CategoriesQuery, cancellationToken);

            return response.CategoriesInformation.Categories.Select(c => c.Name);
        }
    }
}