using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Application.DTOs;
using PlanItPoker.Domain.Entities;

namespace PlanItPoker.Application.Sprints.Queries.GetSprints
{
    public class GetSprintsQueryHandler : IRequestHandler<GetSprintsQuery, IEnumerable<SprintDto>>
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IMapper _mapper;

        public GetSprintsQueryHandler(ISprintRepository sprintRepository, IMapper mapper)
        {
            _sprintRepository = sprintRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SprintDto>> Handle(GetSprintsQuery request, CancellationToken cancellationToken)
        {
            var sprints = await _sprintRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SprintDto>>(sprints);
        }
    }
} 