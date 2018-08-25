namespace WhatNext.Communication.Web.Contracts.Models
{
    using System.Net;

    public class WebApiResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}