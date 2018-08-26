namespace WhatNext.Communication.Web.Contracts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWebApiService : IDisposable
    {
        Task<T> GetAsync<T>(string path, string query = "", CancellationToken cancellationToken = default(CancellationToken));
        Task<T> PostFormAsync<T>(string path, IEnumerable<KeyValuePair<string, string>> dataObject, CancellationToken cancellationToken);
        void SetAuthorizationHeader(string tokenType, string accessToken);
    }
}