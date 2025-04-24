using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanItPoker.Application.DTOs;
using PlanItPoker.Application.Sprints.Commands.CreateSprint;
using PlanItPoker.Application.Sprints.Commands.DeleteSprint;
using PlanItPoker.Application.Sprints.Commands.UpdateSprint;
using PlanItPoker.Application.Sprints.Queries.GetSprintById;
using PlanItPoker.Application.Sprints.Queries.GetSprints;

namespace PlanItPoker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SprintController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SprintController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SprintDto>>> GetAll()
        {
            var query = new GetSprintsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SprintDto>> GetById(Guid id)
        {
            var query = new GetSprintByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateSprintCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSprintCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteSprintCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
} 