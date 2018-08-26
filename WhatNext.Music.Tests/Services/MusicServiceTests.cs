namespace WhatNext.Music.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Communication.Web.Spotify.Contracts.Models;
    using Communication.Web.Spotify.Contracts.Services;
    using Contracts.Mapper;
    using Contracts.Services;
    using Moq;
    using Music.Services;
    using Xunit;

    public class MusicServiceTests
    {
        private const string TestIconUrl = "http://localhost/icon.ico";
        private readonly IMusicService _musicService;

        public MusicServiceTests()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SpotifyProfile>();
            }).CreateMapper();

            var spotifyService = new Mock<ISpotifyService>();
            spotifyService
                .Setup(s => s.ListCategoriesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Category>
                {
                    new Category{Id = "rock", Name = "Rock", Icons = new List<Icon>
                    {
                        new Icon{Url = TestIconUrl}
                    }},
                    new Category{Id = "hiphop", Name = "HipHop"},
                    new Category{Id = "rnb", Name = "RnB"},
                });
            _musicService = new MusicService(spotifyService.Object, mapper);
        }

        [Fact]
        public async Task Should_map_web_category_to_music_category()
        {
            var categories = await _musicService.ListCategoriesAsync(CancellationToken.None);
            var actual = categories.First();

            Assert.IsType<Contracts.Models.Category>(actual);
        }

        [Fact]
        public async Task Should_map_web_category_first_icon_url_to_single_url()
        {
            var expected = TestIconUrl;
            var categories = await _musicService.ListCategoriesAsync(CancellationToken.None);
            var actual = categories.First().IconUrl;

            Assert.Equal(expected, actual);
        }

    }
}
