namespace WhatNext.Web.Contracts.Model
{
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;

    [ExcludeFromCodeCoverage]
    [JsonObject]
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public string PreviewUrl { get; set; }
    }
}
