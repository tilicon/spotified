namespace WhatNext.Communication.Web.Spotify.Tests
{
    using Contracts.Models;
    using Contracts.Services;
    using Moq;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Web.Contracts.Exceptions;
    using Web.Contracts.Services;
    using Xunit;

    public class SpotifyServiceTests
    {
        private ISpotifyService _spotifyService;

        public SpotifyServiceTests()
        {
            var webApiLibraryService = new Mock<IWebApiService>();
            var webApiAuthorizationService = new Mock<IWebApiAuthorizationService>();
            _spotifyService = new SpotifyService(webApiLibraryService.Object, webApiAuthorizationService.Object);
        }

        [Fact]
        public void Should_throw_exception_when_not_injecting_any_service()
        {
            Assert.Throws<ArgumentNullException>(() => new SpotifyService(null, null));
        }

        [Fact]
        public void Should_throw_exception_when_not_injecting_an_authorization_service()
        {
            Assert.Throws<ArgumentNullException>(() => new SpotifyService(new Mock<IWebApiService>().Object, null));
        }

        [Fact]
        public void Should_throw_exception_when_not_injecting_a_library_service()
        {
            Assert.Throws<ArgumentNullException>(() => new SpotifyService(null, new Mock<IWebApiAuthorizationService>().Object));
        }

        [Fact]
        public async Task Should_pass_authorization_when_response_is_received()
        {
            //arrange
            var webApiLibraryService = new Mock<IWebApiService>();
            var webApiAuthorizationService = new Mock<IWebApiAuthorizationService>();

            webApiAuthorizationService
                .Setup(s => s
                    .PostFormAsync<AuthorizationResponse>(It.IsAny<string>(), It.IsAny<IEnumerable<KeyValuePair<string, string>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthorizationResponse
                {
                    AccessToken = "1234",
                    TokenType = "Bearer",
                });

            webApiLibraryService
                .Setup(s => s.GetAsync<CategoryResponse>(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(new CategoryResponse
                {
                    CategoriesInformation = new CategoriesInformation
                    {
                        Categories = Enumerable.Empty<Category>(),
                    },
                });
            var spotifyService = new SpotifyService(webApiLibraryService.Object, webApiAuthorizationService.Object);

            //act
            var actual = await spotifyService.ListCategoriesAsync(CancellationToken.None);

            //assert
            Assert.NotNull(actual);
        }
    }
}