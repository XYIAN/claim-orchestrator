using System.ComponentModel.DataAnnotations;

namespace ClaimOrchestrator.Models
{
	public class ProcessingLog
	{
		public int Id { get; set; }
		
		public int ClaimId { get; set; }
		
		[Required]
		[StringLength(100)]
		public string StepName { get; set; } = string.Empty;
		
		[Required]
		[StringLength(50)]
		public string Status { get; set; } = string.Empty;
		
		[StringLength(500)]
		public string? Message { get; set; }
		
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
		
		// Navigation property
		public virtual Claim Claim { get; set; } = null!;
	}
} 