using System;
using MediatR;

namespace PlanItPoker.Application.Sprints.Commands.CreateSprint
{
    public class CreateSprintCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
    }
} 