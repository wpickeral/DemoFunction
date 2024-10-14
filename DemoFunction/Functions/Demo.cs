using DemoFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DemoFunction.Functions;

public class Demo
{
    private readonly ILogger<Demo> _logger;
    private readonly IConfiguration _configuration;

    public Demo(ILogger<Demo> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [Function("Demo")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,"get", "post")] HttpRequest req)
    {

        var cnn = _configuration.GetConnectionString("DefaultConnection");

        // You can access values in the values section of local.settings.json directly
        // no need to add the values prefix
        var version = _configuration["AzureWebJobsStorage"];

        if (string.Equals(req.Method, "GET", StringComparison.CurrentCultureIgnoreCase))
        {
            return new OkObjectResult(cnn);
        }
        else
        {
            var data = await JsonSerializer.DeserializeAsync<PersonModel>(req.Body);
            // AuthorizationLevel.Function does not work locally
       
            return new OkObjectResult(data);
        }
    }
}
