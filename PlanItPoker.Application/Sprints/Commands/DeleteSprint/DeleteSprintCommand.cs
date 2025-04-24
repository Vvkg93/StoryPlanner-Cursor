using System;
using MediatR;

namespace PlanItPoker.Application.Sprints.Commands.DeleteSprint
{
    public class DeleteSprintCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
} 