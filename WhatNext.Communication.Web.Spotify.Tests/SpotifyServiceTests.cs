namespace WhatNext.Communication.Web.Spotify.Tests
{
    using Contracts.Models;
    using Moq;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Web.Contracts.Services;
    using Xunit;

    public class SpotifyServiceTests
    {
        [Fact]
        public void Should_throw_exception_when_not_injecting_a_library_service()
        {
            Assert.Throws<ArgumentNullException>(() => new SpotifyService(null));
        }

        [Fact]
        public async Task Should_pass_authorization_when_response_is_received()
        {
            //arrange
            var webApiLibraryService = new Mock<IWebApiService>();

            webApiLibraryService
                .Setup(s => s.GetAsync<CategoryResponse>(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(new CategoryResponse
                {
                    CategoriesInformation = new CategoriesInformation
                    {
                        Categories = Enumerable.Empty<Category>(),
                    },
                });
            var spotifyService = new SpotifyService(webApiLibraryService.Object);

            //act
            var actual = await spotifyService.ListCategoriesAsync(CancellationToken.None);

            //assert
            Assert.NotNull(actual);
        }
    }
}
