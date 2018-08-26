namespace WhatNext.Communication.Web.Spotify.Services
{
    using Contracts.Models;
    using Contracts.Services;
    using System;
    using System.Collections.Generic;
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

        private async Task AuthorizeAsync(CancellationToken cancellationToken)
        {
            if (_authorizationResponse != null) return;
            _authorizationResponse = null;

            var formContent = new[] {new KeyValuePair<string, string>("grant_type", "client_credentials")};
            var response = await _authorizationService.PostFormAsync<AuthorizationResponse>(ApiCalls.AuthorizationPath, formContent, cancellationToken);

            _libraryService.SetAuthorizationHeader(response.TokenType, response.AccessToken);
            _authorizationResponse = response;
        }

        public async Task<IEnumerable<Category>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            await AuthorizeAsync(cancellationToken);

            var categories = new List<Category>();
            var hasMoreCategories = true;
            while (hasMoreCategories)
            {
                var response = await _libraryService.GetAsync<CategoryResponse>(ApiCalls.CategoriesPath, string.Format(ApiCalls.CategoriesQuery, categories.Count), cancellationToken);
                categories.AddRange(response?.CategoriesInformation?.Categories);
                hasMoreCategories = response?.CategoriesInformation?.Total > categories.Count;
            }

            return categories;
        }
    }
}