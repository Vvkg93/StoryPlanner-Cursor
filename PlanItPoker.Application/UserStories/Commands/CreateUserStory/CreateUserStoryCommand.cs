using System;
using MediatR;

namespace PlanItPoker.Application.UserStories.Commands.CreateUserStory;

public class CreateUserStoryCommand : IRequest<Guid>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required Guid SprintId { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
} 