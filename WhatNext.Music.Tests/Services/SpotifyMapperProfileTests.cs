namespace WhatNext.Music.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts.Mapper;
    using Xunit;

    public class SpotifyProfileTests
    {
        private readonly IMapper _mapper;

        public SpotifyProfileTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SpotifyProfile>();
            }).CreateMapper();
        }

        [Fact]
        public void Given_spotify_artist_then_should_map_to_internal_artist()
        {
            var spotifyArtist = new Communication.Web.Spotify.Contracts.Models.Artist
            {
                Id = "42",
            };

            var internalArtist = _mapper.Map<Contracts.Models.Artist>(spotifyArtist);

            Assert.Equal(spotifyArtist.Id, internalArtist.Id);
        }

        [Fact]
        public void Given_a_category_then_should_map_icon_object_to_icon_url_string()
        {
            var spotifyCategory = new Communication.Web.Spotify.Contracts.Models.Category
            {
                Icons = new List<Communication.Web.Spotify.Contracts.Models.Icon>{new Communication.Web.Spotify.Contracts.Models.Icon{Url = "test.com"}},
            };

            var category = _mapper.Map<Contracts.Models.Category>(spotifyCategory);

            Assert.Equal(spotifyCategory.Icons.First().Url, category.IconUrl);
        }

        [Fact]
        public void Given_a_track_then_should_map_with_first_artist()
        {
            var spotifyTrack = new Communication.Web.Spotify.Contracts.Models.Track
            {
                Artists = new List<Communication.Web.Spotify.Contracts.Models.Artist>
                {
                    new Communication.Web.Spotify.Contracts.Models.Artist{Id = "1", Name = "Cage the Elephant"},
                    new Communication.Web.Spotify.Contracts.Models.Artist{Id = "2", Name = "The White Stripes"},
                },
            };

            var track = _mapper.Map<Contracts.Models.Track>(spotifyTrack);

            Assert.Equal(spotifyTrack.Artists.First().Id, track.Artist.Id);
        }

    }
}
