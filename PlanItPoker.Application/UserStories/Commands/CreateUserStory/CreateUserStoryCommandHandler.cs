using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Domain.Entities;
using System.Collections.Generic;

namespace PlanItPoker.Application.UserStories.Commands.CreateUserStory;

public class CreateUserStoryCommandHandler : IRequestHandler<CreateUserStoryCommand, Guid>
{
    private readonly IUserStoryRepository _userStoryRepository;
    private readonly ISprintRepository _sprintRepository;

    public CreateUserStoryCommandHandler(IUserStoryRepository userStoryRepository, ISprintRepository sprintRepository)
    {
        _userStoryRepository = userStoryRepository;
        _sprintRepository = sprintRepository;
    }

    public async Task<Guid> Handle(CreateUserStoryCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _sprintRepository.GetByIdAsync(request.SprintId)
            ?? throw new Exception($"Sprint with ID {request.SprintId} not found.");

        var userStory = new UserStory
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            SprintId = request.SprintId,
            Status = "pending",
            JoinLink = Guid.NewGuid().ToString("N"),
            AverageEstimation = 0,
            Votes = new List<Vote>(),
            CreatedBy = request.CreatedBy
        };

        var createdUserStory = await _userStoryRepository.CreateAsync(userStory);
        return createdUserStory.Id;
    }
} 