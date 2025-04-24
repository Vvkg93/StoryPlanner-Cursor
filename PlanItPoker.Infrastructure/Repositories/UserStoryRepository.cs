using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanItPoker.Domain.Entities;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Infrastructure.Data;

namespace PlanItPoker.Infrastructure.Repositories
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly ApplicationDbContext _context;

        public UserStoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserStory>> GetBySprintIdAsync(Guid sprintId)
        {
            return await _context.UserStories
                .Include(us => us.Votes)
                .Where(us => us.SprintId == sprintId)
                .ToListAsync();
        }

        public async Task<UserStory> GetByIdAsync(Guid id)
        {
            var userStory = await _context.UserStories
                .Include(us => us.Votes)
                .FirstOrDefaultAsync(us => us.Id == id);

            if (userStory == null)
                throw new KeyNotFoundException($"UserStory with ID {id} not found.");

            return userStory;
        }

        public async Task<UserStory> CreateAsync(UserStory userStory)
        {
            _context.UserStories.Add(userStory);
            await _context.SaveChangesAsync();
            return userStory;
        }

        public async Task UpdateAsync(UserStory userStory)
        {
            _context.Entry(userStory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var userStory = await _context.UserStories.FindAsync(id);
            if (userStory != null)
            {
                _context.UserStories.Remove(userStory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserStory> GetByJoinLinkAsync(string joinLink)
        {
            var userStory = await _context.UserStories
                .Include(us => us.Votes)
                .FirstOrDefaultAsync(us => us.JoinLink == joinLink);

            if (userStory == null)
                throw new KeyNotFoundException($"UserStory with join link {joinLink} not found.");

            return userStory;
        }

        public async Task AddVoteAsync(Guid userStoryId, Vote vote)
        {
            var userStory = await GetByIdAsync(userStoryId);
            userStory.Votes.Add(vote);
            await _context.SaveChangesAsync();
        }
    }
} 