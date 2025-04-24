using System;
using MediatR;
using PlanItPoker.Application.DTOs;

namespace PlanItPoker.Application.UserStories.Queries.GetUserStoryById
{
    public class GetUserStoryByIdQuery : IRequest<UserStoryDto>
    {
        public required Guid Id { get; set; }
    }
} 