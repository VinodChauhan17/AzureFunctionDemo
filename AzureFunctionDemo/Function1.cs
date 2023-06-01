using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureFunctionDemo
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        public Function1(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _config = configuration;
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            string dbConn = _config.GetSection("Settings").GetSection("DefaultDbConnection").Value;
            // var x = _config.GetSection("Settings").Value;
            response.WriteString("Welcome to Azure Functions!" + dbConn);

            return response;
        }
    }
}
