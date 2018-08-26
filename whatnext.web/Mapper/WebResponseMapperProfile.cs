﻿namespace WhatNext.Web.Mapper
{
    using AutoMapper;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class WebResponseMapperProfile : Profile
    {
        public WebResponseMapperProfile()
        {
            CreateMap<Music.Contracts.Models.Category, Contracts.Model.Category>();
        }
    }
}