using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Services
{
	public interface IEligibilityService
	{
		Task<EligibilityResult> CheckEligibilityAsync(Claim claim);
		Task<EligibilityResult> CheckAmountEligibilityAsync(decimal amount);
		Task<EligibilityResult> CheckGeographicEligibilityAsync(string address);
		Task<EligibilityResult> CheckTimeEligibilityAsync(DateTime createdAt);
	}
	
	public class EligibilityResult
	{
		public bool IsEligible { get; set; }
		public string Message { get; set; } = string.Empty;
		public List<string> Reasons { get; set; } = new List<string>();
		public decimal? AdjustedAmount { get; set; }
		
		public static EligibilityResult Eligible(string message = "Claim is eligible", decimal? adjustedAmount = null)
		{
			return new EligibilityResult
			{
				IsEligible = true,
				Message = message,
				AdjustedAmount = adjustedAmount
			};
		}
		
		public static EligibilityResult NotEligible(string message, List<string>? reasons = null)
		{
			return new EligibilityResult
			{
				IsEligible = false,
				Message = message,
				Reasons = reasons ?? new List<string>()
			};
		}
	}
} 