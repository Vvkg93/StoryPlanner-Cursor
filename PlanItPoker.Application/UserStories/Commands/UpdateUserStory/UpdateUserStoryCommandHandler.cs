using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;

namespace PlanItPoker.Application.UserStories.Commands.UpdateUserStory
{
    public class UpdateUserStoryCommandHandler : IRequestHandler<UpdateUserStoryCommand, Unit>
    {
        private readonly IUserStoryRepository _userStoryRepository;

        public UpdateUserStoryCommandHandler(IUserStoryRepository userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
        }

        public async Task<Unit> Handle(UpdateUserStoryCommand request, CancellationToken cancellationToken)
        {
            var userStory = await _userStoryRepository.GetByIdAsync(request.Id)
                ?? throw new Exception($"User story with ID {request.Id} not found.");

            userStory.Name = request.Name;
            userStory.Description = request.Description;
            userStory.Status = request.Status;

            await _userStoryRepository.UpdateAsync(userStory);
            return Unit.Value;
        }
    }
} 