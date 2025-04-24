using System;
using MediatR;

namespace PlanItPoker.Application.UserStories.Commands.UpdateUserStory
{
    public class UpdateUserStoryCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
    }
} 