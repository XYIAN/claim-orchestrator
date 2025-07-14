using ClaimOrchestrator.Data;
using ClaimOrchestrator.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimOrchestrator.Services
{
	public interface IClaimProcessingService
	{
		Task<bool> ValidateClaimAsync(int claimId);
		Task<bool> CheckDuplicationAsync(int claimId);
		Task<bool> CheckEligibilityAsync(int claimId);
		Task<bool> ProcessClaimAsync(int claimId);
		Task<List<ProcessingLog>> GetProcessingLogsAsync(int claimId);
	}
	
	public class ClaimProcessingService : IClaimProcessingService
	{
		private readonly ClaimContext _context;
		private readonly ILogger<ClaimProcessingService> _logger;
		
		public ClaimProcessingService(ClaimContext context, ILogger<ClaimProcessingService> logger)
		{
			_context = context;
			_logger = logger;
		}
		
		public async Task<bool> ValidateClaimAsync(int claimId)
		{
			try
			{
				var claim = await _context.Claims.FindAsync(claimId);
				if (claim == null)
				{
					await LogProcessingStepAsync(claimId, "Validation", "Failed", "Claim not found");
					return false;
				}
				
				// Simulate validation logic
				var isValid = !string.IsNullOrEmpty(claim.ClaimantName) && 
							 !string.IsNullOrEmpty(claim.Address) && 
							 claim.Amount > 0;
				
				var status = isValid ? "Completed" : "Failed";
				var message = isValid ? "Claim validation successful" : "Claim validation failed - missing required fields";
				
				await LogProcessingStepAsync(claimId, "Validation", status, message);
				
				if (isValid)
				{
					claim.Status = "Validated";
					await _context.SaveChangesAsync();
				}
				
				return isValid;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error validating claim {ClaimId}", claimId);
				await LogProcessingStepAsync(claimId, "Validation", "Error", ex.Message);
				return false;
			}
		}
		
		public async Task<bool> CheckDuplicationAsync(int claimId)
		{
			try
			{
				var claim = await _context.Claims.FindAsync(claimId);
				if (claim == null)
				{
					await LogProcessingStepAsync(claimId, "Deduplication", "Failed", "Claim not found");
					return false;
				}
				
				// Simulate deduplication check
				var duplicateCount = await _context.Claims
					.Where(c => c.ClaimNumber == claim.ClaimNumber && c.Id != claimId)
					.CountAsync();
				
				var isDuplicate = duplicateCount > 0;
				var status = isDuplicate ? "Failed" : "Completed";
				var message = isDuplicate ? "Duplicate claim found" : "No duplicates found";
				
				await LogProcessingStepAsync(claimId, "Deduplication", status, message);
				
				if (!isDuplicate)
				{
					claim.Status = "Deduplicated";
					await _context.SaveChangesAsync();
				}
				
				return !isDuplicate;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking duplication for claim {ClaimId}", claimId);
				await LogProcessingStepAsync(claimId, "Deduplication", "Error", ex.Message);
				return false;
			}
		}
		
		public async Task<bool> CheckEligibilityAsync(int claimId)
		{
			try
			{
				var claim = await _context.Claims.FindAsync(claimId);
				if (claim == null)
				{
					await LogProcessingStepAsync(claimId, "Eligibility", "Failed", "Claim not found");
					return false;
				}
				
				// Simulate eligibility check
				var isEligible = claim.Amount >= 100 && claim.Amount <= 10000;
				var status = isEligible ? "Completed" : "Failed";
				var message = isEligible ? "Claim is eligible" : "Claim amount outside eligible range";
				
				await LogProcessingStepAsync(claimId, "Eligibility", status, message);
				
				if (isEligible)
				{
					claim.Status = "Eligible";
					await _context.SaveChangesAsync();
				}
				
				return isEligible;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking eligibility for claim {ClaimId}", claimId);
				await LogProcessingStepAsync(claimId, "Eligibility", "Error", ex.Message);
				return false;
			}
		}
		
		public async Task<bool> ProcessClaimAsync(int claimId)
		{
			try
			{
				// Run all processing steps
				var validationResult = await ValidateClaimAsync(claimId);
				if (!validationResult) return false;
				
				var deduplicationResult = await CheckDuplicationAsync(claimId);
				if (!deduplicationResult) return false;
				
				var eligibilityResult = await CheckEligibilityAsync(claimId);
				if (!eligibilityResult) return false;
				
				// Update final status
				var claim = await _context.Claims.FindAsync(claimId);
				if (claim != null)
				{
					claim.Status = "Processed";
					await _context.SaveChangesAsync();
					await LogProcessingStepAsync(claimId, "Final Processing", "Completed", "Claim processing completed successfully");
				}
				
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error processing claim {ClaimId}", claimId);
				await LogProcessingStepAsync(claimId, "Final Processing", "Error", ex.Message);
				return false;
			}
		}
		
		public async Task<List<ProcessingLog>> GetProcessingLogsAsync(int claimId)
		{
			return await _context.ProcessingLogs
				.Where(p => p.ClaimId == claimId)
				.OrderByDescending(p => p.Timestamp)
				.ToListAsync();
		}
		
		private async Task LogProcessingStepAsync(int claimId, string stepName, string status, string message)
		{
			var log = new ProcessingLog
			{
				ClaimId = claimId,
				StepName = stepName,
				Status = status,
				Message = message,
				Timestamp = DateTime.UtcNow
			};
			
			_context.ProcessingLogs.Add(log);
			await _context.SaveChangesAsync();
		}
	}
} 