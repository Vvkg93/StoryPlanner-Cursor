using System;
using System.Collections.Generic;

namespace PlanItPoker.Domain.Entities
{
    public class Sprint
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        // Navigation properties
        public required ICollection<UserStory> UserStories { get; set; } = new List<UserStory>();
    }
} 