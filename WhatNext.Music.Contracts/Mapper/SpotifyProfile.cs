namespace WhatNext.Music.Contracts.Mapper
{
    using System.Linq;
    using AutoMapper;

    public class SpotifyProfile : Profile
    {
        public SpotifyProfile()
        {
            CreateMap<Communication.Web.Spotify.Contracts.Models.Category, Models.Category>()
                .ForMember(core => core.IconUrl, spotify => spotify.ResolveUsing(s => s.Icons?.FirstOrDefault()?.url ?? string.Empty));
        }
    }
}