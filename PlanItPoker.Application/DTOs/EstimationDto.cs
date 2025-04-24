using System;

namespace PlanItPoker.Application.DTOs
{
    public class EstimationDto
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required double Value { get; set; }
    }
} 