using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimOrchestrator.Data;
using ClaimOrchestrator.Models;
using ClaimOrchestrator.Services;

namespace ClaimOrchestrator.Controllers
{
	public class ClaimsController : Controller
	{
		private readonly ClaimContext _context;
		private readonly IClaimProcessingService _processingService;
		private readonly ILogger<ClaimsController> _logger;
		
		public ClaimsController(ClaimContext context, IClaimProcessingService processingService, ILogger<ClaimsController> logger)
		{
			_context = context;
			_processingService = processingService;
			_logger = logger;
		}
		
		// GET: Claims
		public async Task<IActionResult> Index()
		{
			var claims = await _context.Claims
				.OrderByDescending(c => c.CreatedAt)
				.ToListAsync();
				
			return View(claims);
		}
		
		// GET: Claims/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var claim = await _context.Claims
				.Include(c => c.ProcessingLogs.OrderByDescending(p => p.Timestamp))
				.FirstOrDefaultAsync(c => c.Id == id);
				
			if (claim == null)
			{
				return NotFound();
			}
			
			return View(claim);
		}
		
		// GET: Claims/Upload
		public IActionResult Upload()
		{
			return View();
		}
		
		// POST: Claims/Upload
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upload(IFormFile? file)
		{
			if (file == null || file.Length == 0)
			{
				// For demo purposes, create sample claims
				await CreateSampleClaimsAsync();
				TempData["Message"] = "Sample claims created successfully!";
				return RedirectToAction(nameof(Index));
			}
			
			// TODO: Implement actual CSV processing
			TempData["Message"] = "File upload functionality will be implemented in future versions.";
			return RedirectToAction(nameof(Index));
		}
		
		// POST: Claims/Process/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Process(int id)
		{
			var claim = await _context.Claims.FindAsync(id);
			if (claim == null)
			{
				return NotFound();
			}
			
			var result = await _processingService.ProcessClaimAsync(id);
			
			TempData["Message"] = result 
				? "Claim processed successfully!" 
				: "Claim processing failed. Check logs for details.";
				
			return RedirectToAction(nameof(Details), new { id });
		}
		
		// POST: Claims/Validate/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Validate(int id)
		{
			var result = await _processingService.ValidateClaimAsync(id);
			
			TempData["Message"] = result 
				? "Claim validation completed successfully!" 
				: "Claim validation failed. Check logs for details.";
				
			return RedirectToAction(nameof(Details), new { id });
		}
		
		// POST: Claims/CheckDuplication/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CheckDuplication(int id)
		{
			var result = await _processingService.CheckDuplicationAsync(id);
			
			TempData["Message"] = result 
				? "Deduplication check completed successfully!" 
				: "Duplicate claim found or check failed. Check logs for details.";
				
			return RedirectToAction(nameof(Details), new { id });
		}
		
		// POST: Claims/CheckEligibility/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CheckEligibility(int id)
		{
			var result = await _processingService.CheckEligibilityAsync(id);
			
			TempData["Message"] = result 
				? "Eligibility check completed successfully!" 
				: "Claim is not eligible or check failed. Check logs for details.";
				
			return RedirectToAction(nameof(Details), new { id });
		}
		
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