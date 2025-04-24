using System;
using MediatR;

namespace PlanItPoker.Application.Sprints.Commands.UpdateSprint;

public class UpdateSprintCommand : IRequest<Unit>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
} 