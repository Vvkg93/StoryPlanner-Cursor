using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanItPoker.Domain.Entities;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Infrastructure.Data;

namespace PlanItPoker.Infrastructure.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly ApplicationDbContext _context;

        public SprintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sprint>> GetAllAsync()
        {
            return await _context.Sprints
                .Include(s => s.UserStories)
                .ToListAsync();
        }

        public async Task<Sprint> GetByIdAsync(Guid id)
        {
            var sprint = await _context.Sprints
                .Include(s => s.UserStories)
                .FirstOrDefaultAsync(s => s.Id == id);
                
            if (sprint == null)
                throw new KeyNotFoundException($"Sprint with ID {id} not found.");
                
            return sprint;
        }

        public async Task<Sprint> CreateAsync(Sprint sprint)
        {
            _context.Sprints.Add(sprint);
            await _context.SaveChangesAsync();
            return sprint;
        }

        public async Task UpdateAsync(Sprint sprint)
        {
            _context.Entry(sprint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var sprint = await _context.Sprints.FindAsync(id);
            if (sprint != null)
            {
                _context.Sprints.Remove(sprint);
                await _context.SaveChangesAsync();
            }
        }
    }
} 