using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimOrchestrator.Data;
using ClaimOrchestrator.Models;
using ClaimOrchestrator.Services;

namespace ClaimOrchestrator.Controllers
{
    /// <summary>
    /// Controller for managing claims in the claim orchestration system.
    /// </summary>
    public class ClaimsController : Controller
    {
        private readonly ClaimContext _context;
        private readonly IClaimProcessingService _processingService;
        private readonly ILogger<ClaimsController> _logger;
        
        /// <summary>
        /// Initializes a new instance of the ClaimsController.
        /// </summary>
        /// <param name="context">The database context for claims.</param>
        /// <param name="processingService">The claim processing service.</param>
        /// <param name="logger">The logger instance.</param>
        public ClaimsController(ClaimContext context, IClaimProcessingService processingService, ILogger<ClaimsController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _processingService = processingService ?? throw new ArgumentNullException(nameof(processingService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        /// <summary>
        /// Displays a list of all claims ordered by creation date.
        /// </summary>
        /// <returns>The claims index view.</returns>
        [HttpGet]
        [Route("claims")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var claims = await _context.Claims
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
                
                ViewBag.Title = "Claims Management - Claim Orchestrator";
                ViewBag.Description = "View and manage all claims in the system. Track claim status, processing history, and perform validation checks.";
                ViewBag.Keywords = "claims management, claim processing, insurance claims, claim validation";
                
                return View(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving claims list");
                TempData["ErrorMessage"] = "An error occurred while retrieving the claims list.";
                return View(new List<Claim>());
            }
        }
        
        /// <summary>
        /// Displays detailed information about a specific claim including processing logs.
        /// </summary>
        /// <param name="id">The unique identifier of the claim.</param>
        /// <returns>The claim details view or NotFound if claim doesn't exist.</returns>
        [HttpGet]
        [Route("claims/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Details action called with null id");
                return NotFound();
            }
            
            try
            {
                var claim = await _context.Claims
                    .Include(c => c.ProcessingLogs.OrderByDescending(p => p.Timestamp))
                    .FirstOrDefaultAsync(c => c.Id == id);
                    
                if (claim == null)
                {
                    _logger.LogWarning("Claim with id {ClaimId} not found", id);
                    return NotFound();
                }
                
                ViewBag.Title = $"Claim {claim.ClaimNumber} - {claim.ClaimantName} - Claim Orchestrator";
                ViewBag.Description = $"Detailed view of claim {claim.ClaimNumber} for {claim.ClaimantName}. Amount: ${claim.Amount:N2}, Status: {claim.Status}";
                ViewBag.Keywords = $"claim {claim.ClaimNumber}, {claim.ClaimantName}, claim details, processing logs";
                
                return View(claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving claim details for id {ClaimId}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving claim details.";
                return RedirectToAction(nameof(Index));
            }
        }
        
        /// <summary>
        /// Displays the claim upload form.
        /// </summary>
        /// <returns>The upload view.</returns>
        [HttpGet]
        [Route("claims/upload")]
        public IActionResult Upload()
        {
            ViewBag.Title = "Upload Claims - Claim Orchestrator";
            ViewBag.Description = "Upload claims data via CSV file or create sample claims for demonstration purposes.";
            ViewBag.Keywords = "upload claims, CSV import, claim data, sample claims";
            
            return View();
        }
        
        /// <summary>
        /// Handles claim upload via CSV file or creates sample claims for demonstration.
        /// </summary>
        /// <param name="file">The uploaded CSV file (optional for demo purposes).</param>
        /// <returns>Redirect to claims index with success/error message.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("claims/upload")]
        public async Task<IActionResult> Upload(IFormFile? file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    // For demo purposes, create sample claims
                    await CreateSampleClaimsAsync();
                    _logger.LogInformation("Sample claims created successfully");
                    TempData["SuccessMessage"] = "Sample claims created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                
                // TODO: Implement actual CSV processing
                _logger.LogInformation("File upload received: {FileName}, Size: {FileSize}", file.FileName, file.Length);
                TempData["InfoMessage"] = "File upload functionality will be implemented in future versions.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during claim upload");
                TempData["ErrorMessage"] = "An error occurred during the upload process.";
                return RedirectToAction(nameof(Index));
            }
        }
        
        /// <summary>
        /// Processes a claim through all validation and eligibility checks.
        /// </summary>
        /// <param name="id">The unique identifier of the claim to process.</param>
        /// <returns>Redirect to claim details with processing result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("claims/{id:int}/process")]
        public async Task<IActionResult> Process(int id)
        {
            try
            {
                var claim = await _context.Claims.FindAsync(id);
                if (claim == null)
                {
                    _logger.LogWarning("Attempted to process non-existent claim with id {ClaimId}", id);
                    return NotFound();
                }
                
                var result = await _processingService.ProcessClaimAsync(id);
                
                if (result)
                {
                    _logger.LogInformation("Claim {ClaimId} processed successfully", id);
                    TempData["SuccessMessage"] = "Claim processed successfully!";
                }
                else
                {
                    _logger.LogWarning("Claim {ClaimId} processing failed", id);
                    TempData["ErrorMessage"] = "Claim processing failed. Check logs for details.";
                }
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing claim {ClaimId}", id);
                TempData["ErrorMessage"] = "An error occurred during claim processing.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
        
        /// <summary>
        /// Validates a specific claim.
        /// </summary>
        /// <param name="id">The unique identifier of the claim to validate.</param>
        /// <returns>Redirect to claim details with validation result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("claims/{id:int}/validate")]
        public async Task<IActionResult> Validate(int id)
        {
            try
            {
                var result = await _processingService.ValidateClaimAsync(id);
                
                if (result)
                {
                    _logger.LogInformation("Claim {ClaimId} validation completed successfully", id);
                    TempData["SuccessMessage"] = "Claim validation completed successfully!";
                }
                else
                {
                    _logger.LogWarning("Claim {ClaimId} validation failed", id);
                    TempData["ErrorMessage"] = "Claim validation failed. Check logs for details.";
                }
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating claim {ClaimId}", id);
                TempData["ErrorMessage"] = "An error occurred during claim validation.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
        
        /// <summary>
        /// Checks for duplicate claims.
        /// </summary>
        /// <param name="id">The unique identifier of the claim to check for duplicates.</param>
        /// <returns>Redirect to claim details with duplication check result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("claims/{id:int}/check-duplication")]
        public async Task<IActionResult> CheckDuplication(int id)
        {
            try
            {
                var result = await _processingService.CheckDuplicationAsync(id);
                
                if (result)
                {
                    _logger.LogInformation("Claim {ClaimId} deduplication check completed successfully", id);
                    TempData["SuccessMessage"] = "Deduplication check completed successfully!";
                }
                else
                {
                    _logger.LogWarning("Claim {ClaimId} duplicate found or check failed", id);
                    TempData["ErrorMessage"] = "Duplicate claim found or check failed. Check logs for details.";
                }
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking duplication for claim {ClaimId}", id);
                TempData["ErrorMessage"] = "An error occurred during duplication check.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
        
        /// <summary>
        /// Checks claim eligibility.
        /// </summary>
        /// <param name="id">The unique identifier of the claim to check eligibility.</param>
        /// <returns>Redirect to claim details with eligibility check result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("claims/{id:int}/check-eligibility")]
        public async Task<IActionResult> CheckEligibility(int id)
        {
            try
            {
                var result = await _processingService.CheckEligibilityAsync(id);
                
                if (result)
                {
                    _logger.LogInformation("Claim {ClaimId} eligibility check completed successfully", id);
                    TempData["SuccessMessage"] = "Eligibility check completed successfully!";
                }
                else
                {
                    _logger.LogWarning("Claim {ClaimId} is not eligible or check failed", id);
                    TempData["ErrorMessage"] = "Claim is not eligible or check failed. Check logs for details.";
                }
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking eligibility for claim {ClaimId}", id);
                TempData["ErrorMessage"] = "An error occurred during eligibility check.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
        
        /// <summary>
        /// Creates sample claims for demonstration purposes.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task CreateSampleClaimsAsync()
        {
            var sampleClaims = new List<Claim>
            {
                new Claim
                {
                    ClaimNumber = "CLM-001",
                    ClaimantName = "John Smith",
                    Address = "123 Main St, Anytown, CA 90210",
                    Amount = 2500.00m,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Claim
                {
                    ClaimNumber = "CLM-002",
                    ClaimantName = "Jane Doe",
                    Address = "456 Oak Ave, Somewhere, NY 10001",
                    Amount = 1500.00m,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new Claim
                {
                    ClaimNumber = "CLM-003",
                    ClaimantName = "Bob Johnson",
                    Address = "789 Pine Rd, Elsewhere, TX 75001",
                    Amount = 5000.00m,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Claim
                {
                    ClaimNumber = "CLM-004",
                    ClaimantName = "Alice Brown",
                    Address = "321 Elm St, Nowhere, FL 33101",
                    Amount = 750.00m,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                },
                new Claim
                {
                    ClaimNumber = "CLM-005",
                    ClaimantName = "Charlie Wilson",
                    Address = "654 Maple Dr, Anywhere, WA 98101",
                    Amount = 3000.00m,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                }
            };
            
            foreach (var claim in sampleClaims)
            {
                _context.Claims.Add(claim);
            }
            
            await _context.SaveChangesAsync();
        }
    }
} 