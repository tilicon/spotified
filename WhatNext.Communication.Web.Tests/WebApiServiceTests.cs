namespace WhatNext.Communication.Web.Tests
{
    using System;
    using Xunit;
    using Services;

    public class WebApiServiceTests
    {
        [Fact]
        public void Should_throw_exception_when_given_an_invalid_uri()
        {
            Assert.Throws<UriFormatException>(() => new WebApiService(new Uri("")));
        }

        [Fact]
        public void Should_throw_exception_when_given_null_as_uri()
        {
            Assert.Throws<ArgumentNullException>(() => new WebApiService(null));
        }
    }
}