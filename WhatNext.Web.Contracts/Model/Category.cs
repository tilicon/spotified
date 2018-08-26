using Newtonsoft.Json;

namespace WhatNext.Web.Contracts
{
    [JsonObject]
    public class Category
    {
        public string IconUrl { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
