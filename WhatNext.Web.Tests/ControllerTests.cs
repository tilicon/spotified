namespace WhatNext.Web.Tests
{
    using AutoMapper;

    using Communication.Web.Services;
    using Communication.Web.Spotify.Contracts.Models;
    using Communication.Web.Spotify.Services;

    using Controllers;

    using Mapper;

    using Moq;
    using Moq.Protected;

    using Music.Contracts.Mapper;
    using Music.Contracts.Services;
    using Music.Services;

    using Newtonsoft.Json;

    using Shouldly;

    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;

    public class ControllerTests
    {
        private readonly IMapper _mapper;

        public ControllerTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WebResponseMapperProfile>();
                cfg.AddProfile<SpotifyProfile>();
            }).CreateMapper();
        }

        [Fact]
        public void Given_null_parameter_when_creating_controller_then_should_throw()
        {
            Should.Throw<ArgumentNullException>(() => new RecommendationsController(null, Mock.Of<IMapper>()));
            Should.Throw<ArgumentNullException>(() => new RecommendationsController(Mock.Of<IMusicService>(), null));
        }

        [Fact]
        public async Task Given_request_for_category_list_then_should_return_all_categories()
        {
            var clientHandler = new Mock<SpotifyClientHandler>(Mock.Of<HttpClient>(), "http://remotehost", "token", "secret");
            clientHandler                
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new CategoryResponse
                    {
                        CategoriesInformation = new CategoriesInformation
                        {
                            Categories = new []
                            {
                                new Category
                                {
                                    Id = "1",
                                    Name = "Category 1",
                                }, 
                                new Category
                                {
                                    Id = "2",
                                    Name = "Category 2",
                                },
                            }
                        }
                    })),
                })
                .Verifiable();

            var httpClient = new HttpClient(clientHandler.Object);

            var webApiService = new WebApiService(new Uri("http://localhost"), httpClient);
            var spotifyService = new SpotifyService(webApiService);

            var musicService = new MusicService(spotifyService, _mapper);

            var recommendationsController = new RecommendationsController(musicService, _mapper);

            var actual = await recommendationsController.ListCategoriesAsync(CancellationToken.None);

            actual.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Given_list_of_categories_then_should_return_related_artists()
        {
            var clientHandler = new Mock<SpotifyClientHandler>(Mock.Of<HttpClient>(), "http://remotehost", "token", "secret");
            clientHandler                
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new RecommendationsResponse
                    {
                        Tracks = new []
                        {
                            new Track
                            {
                                Artists = new []{new Artist{Id = "1", Name = "Cage the Elephant"}, },
                            }, 
                            new Track
                            {
                                Artists = new []{new Artist{Id = "11", Name = "The Ramones"}, },
                            }, 
                        }
                   })),
                })
                .Verifiable();

            var httpClient = new HttpClient(clientHandler.Object);

            var webApiService = new WebApiService(new Uri("http://localhost"), httpClient);
            var spotifyService = new SpotifyService(webApiService);

            var musicService = new MusicService(spotifyService, _mapper);

            var recommendationsController = new RecommendationsController(musicService, _mapper);

            var actual = await recommendationsController.GetArtistsAsync("rock,pop", CancellationToken.None);

            actual.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Given_list_of_artists_then_should_return_related_tracks()
        {
            var clientHandler = new Mock<SpotifyClientHandler>(Mock.Of<HttpClient>(), "http://remotehost", "token", "secret");
            clientHandler                
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new RecommendationsResponse
                    {
                        Tracks = new []
                        {
                            new Track
                            {
                                Id = "1",
                                Name = "Back Against the Wall",
                            }, 
                            new Track
                            {
                                Id = "2",
                                Name = "Seven Nation Army",
                            }, 
                            new Track
                            {
                                Id = "11",
                                Name = "Sheena is a Punk Rocker",
                            }, 
                        }
                    })),
                })
                .Verifiable();

            var httpClient = new HttpClient(clientHandler.Object);

            var webApiService = new WebApiService(new Uri("http://localhost"), httpClient);
            var spotifyService = new SpotifyService(webApiService);

            var musicService = new MusicService(spotifyService, _mapper);

            var recommendationsController = new RecommendationsController(musicService, _mapper);

            var actual = await recommendationsController.GetTracksAsync("1,2,11", CancellationToken.None);

            actual.Count().ShouldBe(3);
        }

    }
}
