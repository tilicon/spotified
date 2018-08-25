namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject]
    public class CategoriesInformation
    {
        public string href { get; set; }

        [JsonProperty("items")]
        public IEnumerable<Category> Categories { get; set; }

        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}