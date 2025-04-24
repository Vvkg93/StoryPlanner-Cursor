using System;
using System.Collections.Generic;

namespace PlanItPoker.Domain.Entities
{
    public class UserStory
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double AverageEstimation { get; set; }
        public required string Status { get; set; }
        public required string JoinLink { get; set; }
        public required Guid SprintId { get; set; }
        public required ICollection<Vote> Votes { get; set; } = new List<Vote>();
        public string CreatedBy { get; set; } = string.Empty;
    }
} 