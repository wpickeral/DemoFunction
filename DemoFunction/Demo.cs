using DemoFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DemoFunction;

public class Demo
{
    private readonly ILogger<Demo> _logger;

    public Demo(ILogger<Demo> logger)
    {
        _logger = logger;
    }

    [Function("Demo")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {

        var data = await JsonSerializer.DeserializeAsync<PersonModel>(req.Body);
        // AuthorizationLevel.Function does not work locally
        return new OkObjectResult(data);
    }
}
