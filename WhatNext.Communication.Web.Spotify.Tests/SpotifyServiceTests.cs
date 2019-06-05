namespace WhatNext.Communication.Web.Spotify.Tests
{
    using Contracts.Models;
    using Moq;
    using Services;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts.Services;
    using Moq.Protected;
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
                        Total = 0,
                    },
                });
            var spotifyService = new SpotifyService(webApiLibraryService.Object);

            //act
            var actual = await spotifyService.ListCategoriesAsync(CancellationToken.None);

            //assert
            Assert.NotNull(actual);
            Assert.False(actual.Any());
        }

        [Fact]
        public async Task Should_fetch_recommended_artists_from_genre()
        {
            //arrange
            var webApiLibraryService = new Mock<IWebApiService>();

            webApiLibraryService
                .Setup(s => s.GetAsync<RecommendationsResponse>(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(new RecommendationsResponse()
                {
                    Tracks = new[]
                    {
                        new Track
                        {
                            Id = "1",
                            Artists = new[] {new Artist {Id = "1", Name = "Cage the Elephant"}},
                            Name = "Back Against the Wall",
                        }
                    }
                });
            var spotifyService = new SpotifyService(webApiLibraryService.Object);

            //act
            var actual = await spotifyService.GetRecommendedArtistsByGenreAsync(new[] {"rock"}, CancellationToken.None);

            //assert
            Assert.NotNull(actual);
            Assert.Contains(actual, artist => artist.Id == "1" && artist.Name == "Cage the Elephant");
        }

        [Fact]
        public async Task Should_fetch_recommended_tracks_from_artist_ids()
        {
            //arrange
            var webApiLibraryService = new Mock<IWebApiService>();

            webApiLibraryService
                .Setup(s => s.GetAsync<RecommendationsResponse>(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(new RecommendationsResponse()
                {
                    Tracks = new[]
                    {
                        new Track
                        {
                            Id = "1",
                            Artists = new[] {new Artist {Id = "1", Name = "Cage the Elephant"}},
                            Name = "Back Against the Wall",
                        }
                    }
                });
            var spotifyService = new SpotifyService(webApiLibraryService.Object);

            //act
            var actual = await spotifyService.GetRecommendedTracksByArtistAsync(new[] {"1"}, CancellationToken.None);

            //assert
            Assert.NotNull(actual);
            Assert.Contains(actual, track => track.Id == "1" && track.Name == "Back Against the Wall");
        }

        [Fact]
        public async Task Should_throw_exception_on_get_when_given_null_as_parameter()
        {
            var clientHandler = new SpotifyClientHandler("http://localhost", "http://remotehost", "Basic", "secret");
            var httpClient = new HttpClient(clientHandler);

            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClient.SendAsync(null));
        }

        [Fact]
        public async Task Given_a_request_then_should_call_client_handler_send_method()
        {
            var clientHandler = new Mock<SpotifyClientHandler>("http://localhost", "http://remotehost", "Basic", "secret");
            clientHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'id':1,'value':'1'}]"),
                })
                .Verifiable();

            var httpClient = new HttpClient(clientHandler.Object);

            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://localhost/"), CancellationToken.None);

            clientHandler
                .Protected()
                .Verify("SendAsync", 
                    Times.Once(), 
                    ItExpr.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Get), 
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}
