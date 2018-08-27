namespace WhatNext.Communication.Web.Spotify.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface ISpotifyService
    {
        Task<IEnumerable<Category>> ListCategoriesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Artist>> GetRecommendedArtistsByGenreAsync(string[] genreList, CancellationToken cancellationToken);
        Task<IEnumerable<Track>> GetRecommendedTracksByArtistAsync(string[] artistIdList, CancellationToken cancellationToken);
    }
}
