namespace WhatNext.Web.Contracts.Model
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
