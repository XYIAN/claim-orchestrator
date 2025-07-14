using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimOrchestrator.Models
{
    /// <summary>
    /// Represents a claim in the claim orchestration system.
    /// </summary>
    public class Claim
    {
        /// <summary>
        /// Gets or sets the unique identifier for the claim.
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the claim number. Must be unique and between 1-50 characters.
        /// </summary>
        [Required(ErrorMessage = "Claim number is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Claim number must be between 1 and 50 characters.")]
        [Display(Name = "Claim Number")]
        public string ClaimNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the name of the claimant. Must be between 1-100 characters.
        /// </summary>
        [Required(ErrorMessage = "Claimant name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Claimant name must be between 1 and 100 characters.")]
        [Display(Name = "Claimant Name")]
        public string ClaimantName { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the address of the claimant. Must be between 1-200 characters.
        /// </summary>
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Address must be between 1 and 200 characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the claim amount. Must be greater than or equal to 0.
        /// </summary>
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0.")]
        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Gets or sets the current status of the claim. Must be between 1-50 characters.
        /// </summary>
        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Status must be between 1 and 50 characters.")]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";
        
        /// <summary>
        /// Gets or sets the date and time when the claim was created.
        /// </summary>
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Gets or sets the date and time when the claim was last updated.
        /// </summary>
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of processing logs associated with this claim.
        /// </summary>
        public virtual ICollection<ProcessingLog> ProcessingLogs { get; set; } = new List<ProcessingLog>();
    }
} 