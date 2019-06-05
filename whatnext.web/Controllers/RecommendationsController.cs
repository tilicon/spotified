namespace WhatNext.Web.Controllers
{
    using System;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Music.Contracts.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts.Model;

    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ApiControllerBase
    {
        private readonly IMusicService _musicService;

        public RecommendationsController(IMusicService spotifyService, IMapper mapper) : base(mapper)
        {
            _musicService = spotifyService ?? throw new ArgumentNullException(nameof(spotifyService));
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _musicService.ListCategoriesAsync(cancellationToken);
            return Map<IEnumerable<Category>>(categories.OrderBy(c => c.Name));
        }

        [HttpGet("artists")]
        public async Task<IEnumerable<Artist>> GetArtistsAsync(string genres, CancellationToken cancellationToken)
        {
            var genreList = genres.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var artists = await _musicService.GetRecommendedArtistsByGenreAsync(genreList, cancellationToken);

            return Map<IEnumerable<Artist>>(artists.GroupBy(a => a.Name).Select(g => g.First()).OrderBy(a => a.Name));
        }

        [HttpGet("tracks")]
        public async Task<IEnumerable<Track>> GetTracksAsync(string artistIds, CancellationToken cancellationToken)
        {
            var artistIdList = artistIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var tracks = await _musicService.GetRecommendedTracksByArtistAsync(artistIdList, cancellationToken);

            return Map<IEnumerable<Track>>(tracks.GroupBy(a => a.Name).Select(g => g.First()).OrderBy(a => a.Name));
        }
    }
}