using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Domain.Entities;

namespace PlanItPoker.Application.Sprints.Commands.CreateSprint;

public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, Guid>
{
    private readonly ISprintRepository _sprintRepository;

    public CreateSprintCommandHandler(ISprintRepository sprintRepository)
    {
        _sprintRepository = sprintRepository;
    }

    public async Task<Guid> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = new Sprint
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            UserStories = new List<UserStory>()
        };

        var createdSprint = await _sprintRepository.CreateAsync(sprint);
        return createdSprint.Id;
    }
} 