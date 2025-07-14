using System.ComponentModel.DataAnnotations;

namespace ClaimOrchestrator.Models
{
	public class Claim
	{
		public int Id { get; set; }
		
		[Required]
		[StringLength(50)]
		public string ClaimNumber { get; set; } = string.Empty;
		
		[Required]
		[StringLength(100)]
		public string ClaimantName { get; set; } = string.Empty;
		
		[Required]
		[StringLength(200)]
		public string Address { get; set; } = string.Empty;
		
		[Required]
		[Range(0, double.MaxValue)]
		public decimal Amount { get; set; }
		
		[Required]
		[StringLength(50)]
		public string Status { get; set; } = "Pending";
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		// Navigation property for processing logs
		public virtual ICollection<ProcessingLog> ProcessingLogs { get; set; } = new List<ProcessingLog>();
	}
} 