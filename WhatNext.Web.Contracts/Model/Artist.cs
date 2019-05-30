namespace WhatNext.Web.Contracts.Model
{
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;

    [ExcludeFromCodeCoverage]
    [JsonObject]
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
