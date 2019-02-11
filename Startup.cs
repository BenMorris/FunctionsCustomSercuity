using FunctionsCustomSercuity;
using FunctionsCustomSercuity.Binding;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Startup))]
namespace FunctionsCustomSercuity
{
    /// <summary>
    /// Runs when the Azure Functions host starts.
    /// </summary>
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddAccessTokenBinding();
        }
    }
}
