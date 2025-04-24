using System.Collections.Generic;
using MediatR;
using PlanItPoker.Application.DTOs;

namespace PlanItPoker.Application.Sprints.Queries.GetSprints;

public record GetSprintsQuery : IRequest<IEnumerable<SprintDto>>; 