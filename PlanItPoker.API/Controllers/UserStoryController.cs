using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanItPoker.Application.DTOs;
using PlanItPoker.Application.UserStories.Commands.CreateUserStory;
using PlanItPoker.Application.UserStories.Commands.UpdateUserStory;
using PlanItPoker.Application.UserStories.Commands.DeleteUserStory;
using PlanItPoker.Application.UserStories.Queries.GetUserStoriesBySprintId;
using PlanItPoker.Application.UserStories.Queries.GetUserStoryById;
using PlanItPoker.Application.UserStories.Queries.GetUserStoryByJoinLink;

namespace PlanItPoker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserStoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserStoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("sprint/{sprintId}")]
        public async Task<ActionResult<IEnumerable<UserStoryDto>>> GetBySprintId(Guid sprintId)
        {
            var query = new GetUserStoriesBySprintIdQuery { SprintId = sprintId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserStoryDto>> GetById(Guid id)
        {
            var userStory = await _mediator.Send(new GetUserStoryByIdQuery { Id = id });
            if (userStory == null)
                return NotFound();

            return Ok(userStory);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateUserStoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBySprintId), new { sprintId = command.SprintId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserStoryCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUserStoryCommand { Id = id });
            return NoContent();
        }

        [HttpGet("join/{joinLink}")]
        public async Task<ActionResult<UserStoryDto>> GetByJoinLink(string joinLink)
        {
            var query = new GetUserStoryByJoinLinkQuery { JoinLink = joinLink };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
} 