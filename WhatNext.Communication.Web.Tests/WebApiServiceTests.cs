namespace WhatNext.Communication.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using Services;
    using Xunit;

    public class WebApiServiceTests
    {
        [JsonObject]
        private class TestClass
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("value")]
            public string Value { get; set; }
        }

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

        [Fact]
        public async Task Given_a_web_api_service_get_request_then_should_trigger_a_http_client_send()
        {
            List<TestClass> actual;
            var httpClientHandler = new Mock<HttpMessageHandler>();
            httpClientHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'id':1,'value':'1'}]"),
                })
                .Verifiable();

            var uri = new Uri("http://localhost");
            var httpClient = new HttpClient(httpClientHandler.Object);
            using (var webApiService = new WebApiService(uri, httpClient))
            {
                actual = await webApiService.GetAsync<List<TestClass>>("localhost");
            }

            httpClientHandler
                .Protected()
                .Verify("SendAsync", 
                    Times.Once(), 
                    ItExpr.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Get), 
                    ItExpr.IsAny<CancellationToken>());

            Assert.Equal("1", actual.First().Id);
            Assert.Equal("1", actual.First().Value);
        }
    }
}
