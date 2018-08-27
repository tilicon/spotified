namespace WhatNext.Music.Contracts.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMusicService
    {
        Task<IEnumerable<Category>> ListCategoriesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Artist>> GetRecommendedArtistsByGenreAsync(string[] genreList, CancellationToken cancellationToken);
        Task<IEnumerable<Track>> GetRecommendedTracksByArtistAsync(string[] artistIdList, CancellationToken cancellationToken);
    }
}
