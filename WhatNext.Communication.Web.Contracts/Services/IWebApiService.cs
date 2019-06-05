namespace WhatNext.Communication.Web.Contracts.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWebApiService : IDisposable
    {
        Task<T> GetAsync<T>(string path, string query = "", CancellationToken cancellationToken = default);
    }
}