using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TokenAuthInjection.AccessTokens;

namespace TokenAuthInjection
{
    public class ExampleHttpFunction
    {
        private readonly IAccessTokenProvider _tokenProvider;

        public ExampleHttpFunction(IAccessTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [FunctionName("ExampleHttpFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "example")] HttpRequest req,  ILogger log)
        {
            var result = _tokenProvider.ValidateToken(req);

            if (result.Status == AccessTokenStatus.Valid)
            {
                log.LogInformation($"Request received for {result.Principal.Identity.Name}.");
                return new OkResult();
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
