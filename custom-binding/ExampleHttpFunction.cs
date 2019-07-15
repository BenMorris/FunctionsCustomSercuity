namespace TokenAuthCustomBinding
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using TokenAuthCustomBinding.Binding;
    using System.Security.Claims;

    public static class ExampleHttpFunction
    {
        [FunctionName("ExampleHttpFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "example")] HttpRequest req, 
            ILogger log, 
            [AccessToken] AccessTokenResult accessTokenResult)
        {
            log.LogInformation($"Request received for {accessTokenResult.Principal?.Identity.Name ?? "anonymous"}.");
            return new OkResult();
        }
    }
}
