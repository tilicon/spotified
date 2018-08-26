namespace WhatNext.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    public class ApiControllerBase : ControllerBase
    {
        private readonly IMapper _mapper;

        public ApiControllerBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        internal T Map<T>(object source)
        {
            return _mapper.Map<T>(source);
        }
    }
}