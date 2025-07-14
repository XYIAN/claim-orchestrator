using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Controllers;

/// <summary>
/// Controller for handling home page and general application views.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Initializes a new instance of the HomeController.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Displays the home page with application overview.
    /// </summary>
    /// <returns>The home page view.</returns>
    [HttpGet]
    [Route("")]
    [Route("home")]
    public IActionResult Index()
    {
        ViewBag.Title = "Claim Orchestrator - Streamlined Claims Management System";
        ViewBag.Description = "Efficiently manage, validate, and process insurance claims with our comprehensive orchestration platform. Features automated validation, deduplication, and eligibility checks.";
        ViewBag.Keywords = "claim orchestrator, insurance claims, claims management, claim processing, validation, deduplication, eligibility";
        
        return View();
    }

    /// <summary>
    /// Displays the privacy policy page.
    /// </summary>
    /// <returns>The privacy policy view.</returns>
    [HttpGet]
    [Route("privacy")]
    public IActionResult Privacy()
    {
        ViewBag.Title = "Privacy Policy - Claim Orchestrator";
        ViewBag.Description = "Learn about how Claim Orchestrator protects your data and maintains privacy standards in claims processing.";
        ViewBag.Keywords = "privacy policy, data protection, claims privacy, security";
        
        return View();
    }

    /// <summary>
    /// Displays error information for debugging purposes.
    /// </summary>
    /// <returns>The error view with request details.</returns>
    [HttpGet]
    [Route("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        _logger.LogError("Error page accessed with request ID: {RequestId}", requestId);
        
        ViewBag.Title = "Error - Claim Orchestrator";
        ViewBag.Description = "An error occurred while processing your request.";
        
        return View(new ErrorViewModel { RequestId = requestId });
    }
}
