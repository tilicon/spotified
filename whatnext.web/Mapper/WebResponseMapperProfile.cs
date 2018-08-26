namespace WhatNext.Web.Mapper
{
    using AutoMapper;

    public class WebResponseMapperProfile : Profile
    {
        public WebResponseMapperProfile()
        {
            CreateMap<Music.Contracts.Models.Category, Contracts.Category>();
        }
    }
}