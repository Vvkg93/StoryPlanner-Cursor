using System;
using System.Collections.Generic;

namespace PlanItPoker.Application.DTOs
{
    public class UserStoryDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double AverageEstimation { get; set; }
        public required string Status { get; set; }
        public required string JoinLink { get; set; }
        public required ICollection<VoteDto> Votes { get; set; }
    }
} 