using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PlanItPoker.Application.DTOs;
using PlanItPoker.Domain.Interfaces;

namespace PlanItPoker.Application.UserStories.Queries.GetUserStoryById
{
    public class GetUserStoryByIdQueryHandler : IRequestHandler<GetUserStoryByIdQuery, UserStoryDto>
    {
        private readonly IUserStoryRepository _userStoryRepository;
        private readonly IMapper _mapper;

        public GetUserStoryByIdQueryHandler(IUserStoryRepository userStoryRepository, IMapper mapper)
        {
            _userStoryRepository = userStoryRepository;
            _mapper = mapper;
        }

        public async Task<UserStoryDto> Handle(GetUserStoryByIdQuery request, CancellationToken cancellationToken)
        {
            var userStory = await _userStoryRepository.GetByIdAsync(request.Id);
            if (userStory == null)
            {
                throw new KeyNotFoundException($"User story with ID {request.Id} was not found.");
            }
            return _mapper.Map<UserStoryDto>(userStory);
        }
    }
} 