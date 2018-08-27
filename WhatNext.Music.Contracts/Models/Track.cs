namespace WhatNext.Music.Contracts.Models
{
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Artist Artist { get; set; }
    }
}
