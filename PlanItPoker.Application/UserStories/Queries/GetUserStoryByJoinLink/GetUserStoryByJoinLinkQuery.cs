using MediatR;
using PlanItPoker.Application.DTOs;

namespace PlanItPoker.Application.UserStories.Queries.GetUserStoryByJoinLink;

public class GetUserStoryByJoinLinkQuery : IRequest<UserStoryDto>
{
    public string JoinLink { get; set; } = string.Empty;
} 