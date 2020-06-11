using System;
using CSharpFunctions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace CSharpFunctionsTests
{
    public class UnitTest1
    {
        [Fact]
        public void WhatTime_ShouldReturnValue()
        {
            // ARRANGE
            var httpContext = new DefaultHttpContext();
            var queryStringValue = "abc";
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection
                (
                    new System.Collections.Generic.Dictionary<string, StringValues>()
                    {
            { "notrequired", queryStringValue }
                    }
                )
            };

            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            // ACT
            var response = WhatTime.Run(request, logger);
            response.Wait();

            // ASSERT
            // Check that the response is an "OK" response
            Assert.IsAssignableFrom<OkObjectResult>(response.Result);

            // Check that the contents of the response are the expected contents
            var result = (OkObjectResult)response.Result;
            var now = DateTime.UtcNow;

            string watchInfo = $"The time is: {now.ToLongDateString()} {now.ToLongTimeString()}";
            Assert.Equal(watchInfo, result.Value);
        }
    }
}
