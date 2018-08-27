namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Track
    {
        [JsonProperty("album")]
        public Album Album { get; set; }

        [JsonProperty("artists")]
        public IEnumerable<Artist> Artists { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}