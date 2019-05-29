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

            var artists = new List<Artist>
            {
                new Artist {Id = "1", Name = "Cage the Elephant"},
                new Artist {Id = "2", Name = "The White Stripes"},
                new Artist {Id = "3", Name = "Dolly Parton"},
            };

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

            spotifyService
                .Setup(s => s.GetRecommendedTracksByArtistAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Track>
                {
                    new Track { Id = "1", Artists = artists.Where(a => a.Id == "1").ToList(), Name = "Back Against the Wall", PreviewUrl = "url1"},
                    new Track { Id = "2", Artists = artists.Where(a => a.Id == "2").ToList(), Name = "Seven Nation Army", PreviewUrl = "url2"},
                    new Track { Id = "3", Artists = artists.Where(a => a.Id == "3" || a.Id == "2").ToList(), Name = "Jolene", PreviewUrl = "url3"},
                });

            spotifyService
                .Setup(s => s.GetRecommendedArtistsByGenreAsync(It.IsAny<string []>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(artists);
            _musicService = new MusicService(spotifyService.Object, mapper);
        }

        [Fact]
        public async Task Should_map_web_category_to_music_category()
        {
            var categories = await _musicService.ListCategoriesAsync(CancellationToken.None);
            var actual = categories.First();

            Assert.IsType<Contracts.Models.Category>(actual);
            Assert.Contains(categories, category => category.Id == "rock" && category.Name == "Rock");
        }

        [Fact]
        public async Task Should_map_web_category_first_icon_url_to_single_url()
        {
            var expected = TestIconUrl;
            var categories = await _musicService.ListCategoriesAsync(CancellationToken.None);
            var actual = categories.First().IconUrl;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Given_list_of_artist_ids_then_should_return_tracks_by_artist()
        {
            var tracks = (await _musicService.GetRecommendedTracksByArtistAsync(new [] {"1", "2", "3"}, CancellationToken.None)).ToList();

            Assert.Equal(3, tracks.Count);
            Assert.Contains(tracks, t => t.Id == "2" && t.Name == "Seven Nation Army" && t.PreviewUrl == "url2");
            Assert.Contains(tracks, t => t.Id == "3" && t.Name == "Jolene" && t.Artist.Name == "Dolly Parton" || t.Artist.Name == "The White Stripes");
        }

        [Fact]
        public async Task Given_list_of_artist_ids_then_should_return_tracks_by_given_artists()
        {
            var artists = (await _musicService.GetRecommendedArtistsByGenreAsync(new []{"rock", "country"}, CancellationToken.None)).ToList();

            Assert.Equal(3, artists.Count);
            Assert.Contains(artists, a => a.Id == "1" && a.Name == "Cage the Elephant");
            Assert.Contains(artists, a => a.Id == "2" && a.Name == "The White Stripes");
        }

    }
}
