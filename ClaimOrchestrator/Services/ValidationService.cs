using ClaimOrchestrator.Data;
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
		
		// All claim validation logic removed. This service is now a stub for Test users only.
	}
}