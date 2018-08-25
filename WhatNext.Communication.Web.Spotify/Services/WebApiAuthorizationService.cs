namespace WhatNext.Communication.Web.Spotify.Services
{
    using System;
    using Contracts.Services;
    using WhatNext.Communication.Web.Services;

    public class WebApiAuthorizationService : WebApiService, IWebApiAuthorizationService
    {
        public WebApiAuthorizationService(Uri apiBaseUri, string tokenType, string accessToken) : base(apiBaseUri)
        {
            SetAuthorizationHeader(tokenType, accessToken);
        }
    }
}