namespace WhatNext.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Communication.Web.Spotify.Contracts.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;

        public RecommendationsController(ISpotifyService spotifyService)
        {
            //TODO: Replace with an abstracted music service that depends on the spotify service interface
            _spotifyService = spotifyService;
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<string>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _spotifyService.ListCategoriesAsync(cancellationToken);
        }
    }
}