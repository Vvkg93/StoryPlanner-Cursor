using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlanItPoker.Domain.Interfaces;

namespace PlanItPoker.Application.UserStories.Commands.DeleteUserStory
{
    public class DeleteUserStoryCommandHandler : IRequestHandler<DeleteUserStoryCommand, Unit>
    {
        private readonly IUserStoryRepository _userStoryRepository;

        public DeleteUserStoryCommandHandler(IUserStoryRepository userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
        }

        public async Task<Unit> Handle(DeleteUserStoryCommand request, CancellationToken cancellationToken)
        {
            await _userStoryRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
} 