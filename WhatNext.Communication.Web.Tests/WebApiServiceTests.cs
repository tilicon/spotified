namespace WhatNext.Communication.Web.Tests
{
    using System;
    using System.Net.Http;
    using Moq;
    using Services;
    using Xunit;

    public class WebApiServiceTests
    {
        [Fact]
        public void Should_throw_exception_when_given_a_null_uri()
        {
            Assert.Throws<ArgumentNullException>(() => new WebApiService(null, Mock.Of<HttpClient>()));
        }

        [Fact]
        public void Should_throw_exception_when_given_an_invalid_uri()
        {
            Assert.Throws<UriFormatException>(() => new WebApiService(new Uri(""), Mock.Of<HttpClient>()));
        }

        [Fact]
        public void Should_throw_exception_when_given_null_as_uri()
        {
            Assert.Throws<ArgumentNullException>(() => new WebApiService(null, Mock.Of<HttpClient>()));
        }

        [Fact]
        public void Should_throw_exception_when_given_null_as_http_client_handler()
        {
            Assert.Throws<ArgumentNullException>(() => new WebApiService(new Uri("http://localhost"), null));
        }
    }
}
