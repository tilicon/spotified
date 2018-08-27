namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    [JsonObject]
    public class RecommendationsResponse
    {
        [JsonProperty("tracks")]
        public IEnumerable<Track> Tracks { get; set; }
    }
}