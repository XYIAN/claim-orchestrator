using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimOrchestrator.Data;
using ClaimOrchestrator.Models;

namespace ClaimOrchestrator.Controllers
{
    /// <summary>
    /// Controller for managing test users in the claim orchestration system.
    /// </summary>
    public class ClaimsController : Controller
    {
        private readonly ClaimContext _context;
        private readonly ILogger<ClaimsController> _logger;

        /// <summary>
        /// Initializes a new instance of the ClaimsController.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger instance.</param>
        public ClaimsController(ClaimContext context, ILogger<ClaimsController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Displays a list of all test users.
        /// </summary>
        /// <returns>The test users index view.</returns>
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var testUsers = await _context.Tests.ToListAsync();
                ViewBag.Title = "Test Users Management - Claim Orchestrator";
                ViewBag.Description = "View and manage all test users in the system.";
                ViewBag.Keywords = "test users, management, authentication";
                return View(testUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test users list");
                TempData["ErrorMessage"] = "An error occurred while retrieving the test users list.";
                return View(new List<Test>());
            }
        }

        /// <summary>
        /// Creates a new test user.
        /// </summary>
        [HttpPost]
        [Route("api/test")]
        public async Task<IActionResult> Create([FromBody] Test testUser)
        {
            if (testUser == null || string.IsNullOrWhiteSpace(testUser.Username) || string.IsNullOrWhiteSpace(testUser.Password))
            {
                return BadRequest("Username and Password are required.");
            }
            _context.Tests.Add(testUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = testUser.Id }, testUser);
        }

        /// <summary>
        /// Gets a test user by ID.
        /// </summary>
        [HttpGet]
        [Route("api/test/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Tests.FindAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Updates an existing test user.
        /// </summary>
        [HttpPut]
        [Route("api/test/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Test updatedUser)
        {
            var user = await _context.Tests.FindAsync(id);
            if (user == null)
                return NotFound();
            if (string.IsNullOrWhiteSpace(updatedUser.Username) || string.IsNullOrWhiteSpace(updatedUser.Password))
                return BadRequest("Username and Password are required.");
            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        /// <summary>
        /// Deletes a test user by ID.
        /// </summary>
        [HttpDelete]
        [Route("api/test/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Tests.FindAsync(id);
            if (user == null)
                return NotFound();
            _context.Tests.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}