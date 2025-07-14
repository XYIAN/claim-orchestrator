using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Services
{
	public class EligibilityService : IEligibilityService
	{
		private readonly ILogger<EligibilityService> _logger;
		
		public EligibilityService(ILogger<EligibilityService> logger)
		{
			_logger = logger;
		}
		
		public async Task<EligibilityResult> CheckEligibilityAsync(Claim claim)
		{
			var reasons = new List<string>();
			
			// Check amount eligibility
			var amountResult = await CheckAmountEligibilityAsync(claim.Amount);
			if (!amountResult.IsEligible)
			{
				reasons.AddRange(amountResult.Reasons);
			}
			
			// Check geographic eligibility
			var geoResult = await CheckGeographicEligibilityAsync(claim.Address);
			if (!geoResult.IsEligible)
			{
				reasons.AddRange(geoResult.Reasons);
			}
			
			// Check time eligibility
			var timeResult = await CheckTimeEligibilityAsync(claim.CreatedAt);
			if (!timeResult.IsEligible)
			{
				reasons.AddRange(timeResult.Reasons);
			}
			
			if (reasons.Any())
			{
				return EligibilityResult.NotEligible("Claim is not eligible", reasons);
			}
			
			return EligibilityResult.Eligible("Claim is eligible");
		}
		
		public Task<EligibilityResult> CheckAmountEligibilityAsync(decimal amount)
		{
			// Basic amount range check
			if (amount < 100)
			{
				return Task.FromResult(EligibilityResult.NotEligible("Amount too low", new List<string> { "Minimum claim amount is $100" }));
			}
			
			if (amount > 10000)
			{
				return Task.FromResult(EligibilityResult.NotEligible("Amount too high", new List<string> { "Maximum claim amount is $10,000" }));
			}
			
			// Tier-based adjustments (example business logic)
			decimal? adjustedAmount = null;
			if (amount > 5000)
			{
				// Apply 5% reduction for high-value claims
				adjustedAmount = amount * 0.95m;
			}
			
			return Task.FromResult(EligibilityResult.Eligible("Amount is eligible", adjustedAmount));
		}
		
		public Task<EligibilityResult> CheckGeographicEligibilityAsync(string address)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				return Task.FromResult(EligibilityResult.NotEligible("Invalid address", new List<string> { "Address is required" }));
			}
			
			// Check for US addresses (basic check)
			var usStates = new[] { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };
			
			var hasValidState = usStates.Any(state => address.ToUpper().Contains(state));
			if (!hasValidState)
			{
				return Task.FromResult(EligibilityResult.NotEligible("Geographic restriction", new List<string> { "Only US addresses are eligible" }));
			}
			
			return Task.FromResult(EligibilityResult.Eligible("Geographic location is eligible"));
		}
		
		public Task<EligibilityResult> CheckTimeEligibilityAsync(DateTime createdAt)
		{
			// Check if claim was created within the last 2 years
			var twoYearsAgo = DateTime.UtcNow.AddYears(-2);
			if (createdAt < twoYearsAgo)
			{
				return Task.FromResult(EligibilityResult.NotEligible("Time restriction", new List<string> { "Claims must be filed within 2 years" }));
			}
			
			// Check if claim was created in the future (data validation)
			if (createdAt > DateTime.UtcNow.AddDays(1))
			{
				return Task.FromResult(EligibilityResult.NotEligible("Invalid date", new List<string> { "Claim date cannot be in the future" }));
			}
			
			return Task.FromResult(EligibilityResult.Eligible("Time requirements are met"));
		}
	}
} 