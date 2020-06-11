using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace sandpit_azfunctions
{
    public static class what_time
    {
        [FunctionName("what_time")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request for 'what-time'.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var now = DateTime.UtcNow;
            string responseMessage = $"The time is: {now.ToLongDateString()} {now.ToLongTimeString()}";

            return new OkObjectResult(responseMessage);
        }
    }
}
