using System;

namespace PlanItPoker.Application.DTOs
{
    public class VoteDto
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string UserAvatar { get; set; }
        public required double Estimation { get; set; }
    }
} 