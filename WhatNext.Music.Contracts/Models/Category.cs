namespace WhatNext.Music.Contracts.Models
{
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class Category
    {
        public string IconUrl { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
