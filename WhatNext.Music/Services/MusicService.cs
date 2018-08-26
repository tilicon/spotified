namespace WhatNext.Music.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Communication.Web.Spotify.Contracts.Services;
    using Contracts.Models;
    using Contracts.Services;

    public class MusicService : IMusicService
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IMapper _mapper;

        public MusicService(ISpotifyService spotifyService, IMapper mapper)
        {
            _spotifyService = spotifyService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            var musicCategories = await _spotifyService.ListCategoriesAsync(cancellationToken);
            return _mapper.Map<IEnumerable<Category>>(musicCategories);
        }
    }
}