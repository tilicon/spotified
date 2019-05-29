namespace WhatNext.Music.Contracts.Mapper
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;

    public class SpotifyProfile : Profile
    {
        public SpotifyProfile()
        {
            CreateMap<Communication.Web.Spotify.Contracts.Models.Category, Models.Category>()
                .ForMember(core => core.IconUrl, spotify => spotify.MapFrom(s => GetUrl(s.Icons)));

            CreateMap<Communication.Web.Spotify.Contracts.Models.Artist, Models.Artist>();

            CreateMap<Communication.Web.Spotify.Contracts.Models.Track, Models.Track>()
                .ForMember(core => core.Artist, spotify => spotify.MapFrom(s => s.Artists != null ? s.Artists.FirstOrDefault() : null));
        }

        private static string GetUrl(IEnumerable<Communication.Web.Spotify.Contracts.Models.Icon> icons)
        {
            return icons?.FirstOrDefault()?.Url;
        }
    }
}