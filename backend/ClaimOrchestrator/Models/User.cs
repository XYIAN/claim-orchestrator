using System;

namespace ClaimOrchestrator.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid UserGuid { get; set; }
    }
} 