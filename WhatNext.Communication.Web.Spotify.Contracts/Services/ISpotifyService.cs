namespace WhatNext.Communication.Web.Spotify.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISpotifyService
    {
        Task AuthorizeAsync(CancellationToken cancellationToken);
        Task<IEnumerable<string>> ListCategoriesAsync(CancellationToken cancellationToken);
    }
}
