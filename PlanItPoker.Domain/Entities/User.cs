using System;
using System.Collections.Generic;

namespace PlanItPoker.Domain.Entities
{
    public class User
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string DisplayName { get; set; }
        public required string Role { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public ICollection<Sprint> CreatedSprints { get; set; } = new List<Sprint>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
} 