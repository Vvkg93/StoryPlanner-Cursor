using System;

namespace PlanItPoker.Domain.Entities
{
    public class Estimation
    {
        public required Guid Id { get; set; }
        public required Guid UserStoryId { get; set; }
        public required UserStory UserStory { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }
        public required double Value { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
} 