using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimOrchestrator.Models
{
    /// <summary>
    /// Represents a processing log entry for claim processing steps.
    /// </summary>
    public class ProcessingLog
    {
        /// <summary>
        /// Gets or sets the unique identifier for the processing log.
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the foreign key to the associated claim.
        /// </summary>
        [Required]
        [Display(Name = "Claim ID")]
        public int ClaimId { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the processing step. Must be between 1-100 characters.
        /// </summary>
        [Required(ErrorMessage = "Step name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Step name must be between 1 and 100 characters.")]
        [Display(Name = "Step Name")]
        public string StepName { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the status of the processing step. Must be between 1-50 characters.
        /// </summary>
        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Status must be between 1 and 50 characters.")]
        [Display(Name = "Status")]
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the optional message or details about the processing step. Maximum 500 characters.
        /// </summary>
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        [Display(Name = "Message")]
        public string? Message { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the processing log was created.
        /// </summary>
        [Display(Name = "Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Gets or sets the navigation property to the associated claim.
        /// </summary>
        [ForeignKey(nameof(ClaimId))]
        public virtual Claim Claim { get; set; } = null!;
    }
} 