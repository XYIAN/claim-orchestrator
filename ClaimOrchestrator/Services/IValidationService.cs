using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Services
{
	public interface IValidationService
	{
		Task<ValidationResult> ValidateClaimAsync(Claim claim);
		Task<ValidationResult> ValidateClaimNumberAsync(string claimNumber, int? excludeClaimId = null);
		Task<ValidationResult> ValidateClaimantInfoAsync(string claimantName, string address);
		Task<ValidationResult> ValidateAmountAsync(decimal amount);
	}
	
	public class ValidationResult
	{
		public bool IsValid { get; set; }
		public string Message { get; set; } = string.Empty;
		public List<string> Errors { get; set; } = new List<string>();
		
		public static ValidationResult Success(string message = "Validation successful")
		{
			return new ValidationResult
			{
				IsValid = true,
				Message = message
			};
		}
		
		public static ValidationResult Failure(string message, List<string>? errors = null)
		{
			return new ValidationResult
			{
				IsValid = false,
				Message = message,
				Errors = errors ?? new List<string>()
			};
		}
	}
} 