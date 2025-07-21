using Microsoft.EntityFrameworkCore;
using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Data
{
	public class ClaimContext : DbContext
	{
		public ClaimContext(DbContextOptions<ClaimContext> options) : base(options)
		{
		}
		
		public DbSet<Test> Tests { get; set; } // Only Test entity remains
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// No Claim or ProcessingLog configuration
		}
	}
}