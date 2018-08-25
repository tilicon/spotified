namespace WhatNext.Communication.Web.Spotify.Tests
{
    using System;
    using System.Threading;
    using Contracts.Services;
    using Moq;
    using Xunit;
    using Services;

    using Web.Contracts.Services;

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
        public void Should()
        {
            _spotifyService.AuthorizeAsync(CancellationToken.None);
        }
    }
}