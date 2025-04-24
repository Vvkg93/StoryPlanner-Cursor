using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Application.DTOs;
using AutoMapper;

namespace PlanItPoker.Application.UserStories.Queries.GetUserStoryByJoinLink;

public class GetUserStoryByJoinLinkQueryHandler : IRequestHandler<GetUserStoryByJoinLinkQuery, UserStoryDto>
{
    private readonly IUserStoryRepository _userStoryRepository;
    private readonly IMapper _mapper;

    public GetUserStoryByJoinLinkQueryHandler(IUserStoryRepository userStoryRepository, IMapper mapper)
    {
        _userStoryRepository = userStoryRepository;
        _mapper = mapper;
    }

    public async Task<UserStoryDto> Handle(GetUserStoryByJoinLinkQuery request, CancellationToken cancellationToken)
    {
        var userStory = await _userStoryRepository.GetByJoinLinkAsync(request.JoinLink)
            ?? throw new Exception($"User story with join link {request.JoinLink} not found.");

        return _mapper.Map<UserStoryDto>(userStory);
    }
} 