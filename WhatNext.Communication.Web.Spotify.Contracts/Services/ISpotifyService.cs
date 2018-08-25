namespace WhatNext.Communication.Web.Spotify.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface ISpotifyService
    {
        Task<IEnumerable<string>> ListCategoriesAsync(CancellationToken cancellationToken);
    }
}
