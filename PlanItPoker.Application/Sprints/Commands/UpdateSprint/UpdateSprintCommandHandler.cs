using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Domain.Entities;

namespace PlanItPoker.Application.Sprints.Commands.UpdateSprint;

public class UpdateSprintCommandHandler : IRequestHandler<UpdateSprintCommand, Unit>
{
    private readonly ISprintRepository _sprintRepository;

    public UpdateSprintCommandHandler(ISprintRepository sprintRepository)
    {
        _sprintRepository = sprintRepository;
    }

    public async Task<Unit> Handle(UpdateSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _sprintRepository.GetByIdAsync(request.Id)
            ?? throw new Exception($"Sprint with ID {request.Id} not found.");

        sprint.Name = request.Name;
        sprint.StartDate = request.StartDate;
        sprint.EndDate = request.EndDate;

        await _sprintRepository.UpdateAsync(sprint);
        return Unit.Value;
    }
} 