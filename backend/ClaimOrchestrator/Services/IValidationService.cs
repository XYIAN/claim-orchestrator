using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Services
{
	public interface IValidationService
	{
		// All claim-related validation interface logic removed. This interface is now a stub for Test users only.
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