using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanItPoker.Domain.Entities;
using PlanItPoker.Domain.Interfaces;
using PlanItPoker.Infrastructure.Data;

namespace PlanItPoker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var guidId = Guid.Parse(id);
            var user = await _context.Users.FindAsync(guidId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new KeyNotFoundException($"User with email {email} not found.");
            return user;
        }

        public async Task<User> GetByDisplayNameAsync(string displayName)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.DisplayName == displayName);
            if (user == null)
                throw new KeyNotFoundException($"User with display name {displayName} not found.");
            return user;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var guidId = Guid.Parse(id);
            var user = await _context.Users.FindAsync(guidId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
} 