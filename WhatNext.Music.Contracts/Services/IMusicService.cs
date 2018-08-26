namespace WhatNext.Music.Contracts.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMusicService
    {
        Task<IEnumerable<Category>> ListCategoriesAsync(CancellationToken cancellationToken);
    }
}
