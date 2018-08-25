namespace WhatNext.Communication.Web.Contracts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface IWebApiService : IDisposable
    {
        Task<WebApiResult<T>> GetAsync<T>(string path, string query = "", CancellationToken cancellationToken = default(CancellationToken));
        Task<WebApiResult<T>> PostFormAsync<T>(string path, IEnumerable<KeyValuePair<string, string>> dataObject, CancellationToken cancellationToken);
        Task<WebApiResult<T>> PostJsonAsync<T>(string path, object dataObject, CancellationToken cancellationToken);
        void SetAuthorizationHeader(string tokenType, string accessToken);
    }
}