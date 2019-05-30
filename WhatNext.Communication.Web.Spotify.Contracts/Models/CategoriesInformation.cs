namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject]
    public class CategoriesInformation
    {
        [JsonProperty("items")]
        public IEnumerable<Category> Categories { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}