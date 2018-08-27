namespace WhatNext.Web.Contracts.Model
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Category
    {
        public string IconUrl { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
