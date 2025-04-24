using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;

namespace PlanItPoker.Application.Sprints.Commands.DeleteSprint;

public class DeleteSprintCommandHandler : IRequestHandler<DeleteSprintCommand, Unit>
{
    private readonly ISprintRepository _sprintRepository;

    public DeleteSprintCommandHandler(ISprintRepository sprintRepository)
    {
        _sprintRepository = sprintRepository;
    }

    public async Task<Unit> Handle(DeleteSprintCommand request, CancellationToken cancellationToken)
    {
        await _sprintRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
} 