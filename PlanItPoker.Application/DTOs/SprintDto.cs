using System;
using System.Collections.Generic;

namespace PlanItPoker.Application.DTOs
{
    public class SprintDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required ICollection<UserStoryDto> UserStories { get; set; }
    }
} 