using MediatR;
using PlanItPoker.Application.DTOs;

namespace PlanItPoker.Application.Sprints.Queries.GetSprintById;

public class GetSprintByIdQuery : IRequest<SprintDto>
{
    public Guid Id { get; set; }
} 