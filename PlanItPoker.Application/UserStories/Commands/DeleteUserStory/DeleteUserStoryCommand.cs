using System;
using MediatR;

namespace PlanItPoker.Application.UserStories.Commands.DeleteUserStory
{
    public class DeleteUserStoryCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
    }
} 