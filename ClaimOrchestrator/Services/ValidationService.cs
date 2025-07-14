using ClaimOrchestrator.Data;
using ClaimOrchestrator.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimOrchestrator.Services
{
	public class ValidationService : IValidationService
	{
		private readonly ClaimContext _context;
		private readonly ILogger<ValidationService> _logger;
		
		public ValidationService(ClaimContext context, ILogger<ValidationService> logger)
		{
			_context = context;
			_logger = logger;
		}
		
		public async Task<ValidationResult> ValidateClaimAsync(Claim claim)
		{
			var errors = new List<string>();
			
			// Validate claim number
			var claimNumberResult = await ValidateClaimNumberAsync(claim.ClaimNumber, claim.Id);
			if (!claimNumberResult.IsValid)
			{
				errors.AddRange(claimNumberResult.Errors);
			}
			
			// Validate claimant info
			var claimantResult = await ValidateClaimantInfoAsync(claim.ClaimantName, claim.Address);
			if (!claimantResult.IsValid)
			{
				errors.AddRange(claimantResult.Errors);
			}
			
			// Validate amount
			var amountResult = await ValidateAmountAsync(claim.Amount);
			if (!amountResult.IsValid)
			{
				errors.AddRange(amountResult.Errors);
			}
			
			if (errors.Any())
			{
				return ValidationResult.Failure("Claim validation failed", errors);
			}
			
			return ValidationResult.Success("Claim validation successful");
		}
		
		public async Task<ValidationResult> ValidateClaimNumberAsync(string claimNumber, int? excludeClaimId = null)
		{
			if (string.IsNullOrWhiteSpace(claimNumber))
			{
				return ValidationResult.Failure("Claim number is required", new List<string> { "Claim number cannot be empty" });
			}
			
			if (claimNumber.Length > 50)
			{
				return ValidationResult.Failure("Claim number too long", new List<string> { "Claim number must be 50 characters or less" });
			}
			
			// Check for duplicates
			var query = _context.Claims.Where(c => c.ClaimNumber == claimNumber);
			if (excludeClaimId.HasValue)
			{
				query = query.Where(c => c.Id != excludeClaimId.Value);
			}
			
			var duplicateCount = await query.CountAsync();
			if (duplicateCount > 0)
			{
				return ValidationResult.Failure("Duplicate claim number", new List<string> { $"Claim number '{claimNumber}' already exists" });
			}
			
			return ValidationResult.Success("Claim number is valid");
		}
		
		public Task<ValidationResult> ValidateClaimantInfoAsync(string claimantName, string address)
		{
			var errors = new List<string>();
			
			if (string.IsNullOrWhiteSpace(claimantName))
			{
				errors.Add("Claimant name is required");
			}
			else if (claimantName.Length > 100)
			{
				errors.Add("Claimant name must be 100 characters or less");
			}
			
			if (string.IsNullOrWhiteSpace(address))
			{
				errors.Add("Address is required");
			}
			else if (address.Length > 200)
			{
				errors.Add("Address must be 200 characters or less");
			}
			
			if (errors.Any())
			{
				return Task.FromResult(ValidationResult.Failure("Claimant information validation failed", errors));
			}
			
			return Task.FromResult(ValidationResult.Success("Claimant information is valid"));
		}
		
		public Task<ValidationResult> ValidateAmountAsync(decimal amount)
		{
			if (amount <= 0)
			{
				return Task.FromResult(ValidationResult.Failure("Invalid amount", new List<string> { "Amount must be greater than zero" }));
			}
			
			if (amount > 1000000) // $1M limit
			{
				return Task.FromResult(ValidationResult.Failure("Amount too high", new List<string> { "Amount cannot exceed $1,000,000" }));
			}
			
			return Task.FromResult(ValidationResult.Success("Amount is valid"));
		}
	}
} 