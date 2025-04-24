using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanItPoker.Domain.Entities;

namespace PlanItPoker.Domain.Interfaces;

public interface IUserStoryRepository
{
    Task<IEnumerable<UserStory>> GetBySprintIdAsync(Guid sprintId);
    Task<UserStory> GetByIdAsync(Guid id);
    Task<UserStory> CreateAsync(UserStory userStory);
    Task UpdateAsync(UserStory userStory);
    Task DeleteAsync(Guid id);
    Task<UserStory> GetByJoinLinkAsync(string joinLink);
} 