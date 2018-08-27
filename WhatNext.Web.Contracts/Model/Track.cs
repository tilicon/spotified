namespace WhatNext.Web.Contracts.Model
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public string PreviewUrl { get; set; }
    }
}
