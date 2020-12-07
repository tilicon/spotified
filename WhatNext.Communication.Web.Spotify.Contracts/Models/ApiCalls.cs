namespace WhatNext.Communication.Web.Spotify.Contracts.Models
{
    public static class ApiCalls
    {
        public const string AuthorizationPath = "api/token";

        public const string CategoriesPath = "v1/browse/categories";
        public const string CategoriesQuery = "?offset={0}";

        public const string RecommendationsPath = "v1/recommendations";
        public const string GenreSeedQuery = "?seed_genres={0}";

        public const string ArtistSeedQuery = "?seed_artists={0}&max_popularity=60";
    }
}
