namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Track
    {
        [JsonProperty("artists")]
        public IEnumerable<Artist> Artists { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }
    }
}