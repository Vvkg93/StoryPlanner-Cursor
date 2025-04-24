using System;
using System.Collections.Generic;
using MediatR;
using PlanItPoker.Application.DTOs;

namespace PlanItPoker.Application.UserStories.Queries.GetUserStoriesBySprintId;

public class GetUserStoriesBySprintIdQuery : IRequest<IEnumerable<UserStoryDto>>
{
    public Guid SprintId { get; set; }
} 