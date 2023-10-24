using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_harness_ff_sample.Models;
using io.harness.cfsdk.client.api;

namespace dotnet_harness_ff_sample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICfClient cfClient;

    public HomeController(ILogger<HomeController> logger, ICfClient cfClient)
    {
        _logger = logger;
        this.cfClient = cfClient;
    }

    public IActionResult Index()
    {
        // initialize feature flag target for current user
        var target = io.harness.cfsdk.client.dto.Target.builder()
                .Name("User1") //can change with your target name
                .Identifier("user1@example.com") //can change with your target identifier
                .build();

        // fetch feature flag value for target
        var sampleFlag = this.cfClient.boolVariation("sample_flag", target, false);
        Console.WriteLine("Sample flag value: " + sampleFlag.ToString());
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
