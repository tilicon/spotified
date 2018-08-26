namespace WhatNext.Web.Controllers
{
    using AutoMapper;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Music.Contracts.Services;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ApiControllerBase
    {
        private readonly IMusicService _musicService;

        public RecommendationsController(IMusicService spotifyService, IMapper mapper) : base(mapper)
        {
            _musicService = spotifyService;
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _musicService.ListCategoriesAsync(cancellationToken);
            return Map<IEnumerable<Category>>(categories);
        }
    }
}