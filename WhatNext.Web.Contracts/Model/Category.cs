namespace WhatNext.Web.Contracts.Model
{
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;

    [ExcludeFromCodeCoverage]
    [JsonObject]
    public class Category
    {
        public string IconUrl { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
