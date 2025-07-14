using Microsoft.EntityFrameworkCore;
using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Data
{
	public class ClaimContext : DbContext
	{
		public ClaimContext(DbContextOptions<ClaimContext> options) : base(options)
		{
		}
		
		public DbSet<Claim> Claims { get; set; }
		public DbSet<ProcessingLog> ProcessingLogs { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			// Configure relationships
			modelBuilder.Entity<ProcessingLog>()
				.HasOne(p => p.Claim)
				.WithMany(c => c.ProcessingLogs)
				.HasForeignKey(p => p.ClaimId)
				.OnDelete(DeleteBehavior.Cascade);
			
			// Configure indexes
			modelBuilder.Entity<Claim>()
				.HasIndex(c => c.ClaimNumber)
				.IsUnique();
				
			modelBuilder.Entity<ProcessingLog>()
				.HasIndex(p => p.ClaimId);
				
			modelBuilder.Entity<ProcessingLog>()
				.HasIndex(p => p.Timestamp);
		}
	}
} 