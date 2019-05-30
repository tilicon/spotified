using System.Collections.Generic;

namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Category
    {
        [JsonProperty("icons")]
        public IEnumerable<Icon> Icons { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
