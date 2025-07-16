using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Services
{
	public interface IEligibilityService
	{
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