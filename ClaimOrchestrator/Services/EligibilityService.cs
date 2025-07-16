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
	}
}