using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Application.DTOs;
using AutoMapper;

namespace PlanItPoker.Application.Sprints.Queries.GetSprintById;

public class GetSprintByIdQueryHandler : IRequestHandler<GetSprintByIdQuery, SprintDto>
{
    private readonly ISprintRepository _sprintRepository;
    private readonly IMapper _mapper;

    public GetSprintByIdQueryHandler(ISprintRepository sprintRepository, IMapper mapper)
    {
        _sprintRepository = sprintRepository;
        _mapper = mapper;
    }

    public async Task<SprintDto> Handle(GetSprintByIdQuery request, CancellationToken cancellationToken)
    {
        var sprint = await _sprintRepository.GetByIdAsync(request.Id)
            ?? throw new Exception($"Sprint with ID {request.Id} not found.");

        return _mapper.Map<SprintDto>(sprint);
    }
} 