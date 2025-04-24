using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Application.DTOs;
using AutoMapper;

namespace PlanItPoker.Application.UserStories.Queries.GetUserStoriesBySprintId;

public class GetUserStoriesBySprintIdQueryHandler : IRequestHandler<GetUserStoriesBySprintIdQuery, IEnumerable<UserStoryDto>>
{
    private readonly IUserStoryRepository _userStoryRepository;
    private readonly IMapper _mapper;

    public GetUserStoriesBySprintIdQueryHandler(IUserStoryRepository userStoryRepository, IMapper mapper)
    {
        _userStoryRepository = userStoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserStoryDto>> Handle(GetUserStoriesBySprintIdQuery request, CancellationToken cancellationToken)
    {
        var userStories = await _userStoryRepository.GetBySprintIdAsync(request.SprintId);
        return _mapper.Map<IEnumerable<UserStoryDto>>(userStories);
    }
} 