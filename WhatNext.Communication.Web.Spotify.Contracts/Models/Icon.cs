namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Icon
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}