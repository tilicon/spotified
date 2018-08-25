namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    using Newtonsoft.Json;

    [JsonObject]
    public class CategoryResponse
    {
        [JsonProperty("categories")]
        public CategoriesInformation CategoriesInformation { get; set; }
    }
}
