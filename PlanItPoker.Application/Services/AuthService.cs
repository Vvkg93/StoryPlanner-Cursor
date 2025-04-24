using System;
using System.Threading.Tasks;
using PlanItPoker.Application.DTOs;
using PlanItPoker.Application.Interfaces;
using PlanItPoker.Domain.Entities;
using PlanItPoker.Domain.Interfaces;
using System.Collections.Generic;

namespace PlanItPoker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> LoginAsync(string username, string password)
        {
            // Simple admin check
            if (username == "admin" && password == "admin123")
            {
                return new UserDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "admin@example.com",
                    DisplayName = "Admin User",
                    Avatar = "https://via.placeholder.com/150?text=A",
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    LastLoginAt = DateTime.UtcNow
                };
            }

            try
            {
                var user = await _userRepository.GetByEmailAsync(username); // Using username as email for now
                
                // In a real application, you would hash the password and compare hashes
                if (password != "user123") // Simple password check for demo
                {
                    throw new UnauthorizedAccessException("Invalid credentials");
                }

                user.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);

                return MapToDto(user);
            }
            catch (KeyNotFoundException)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
        }

        public async Task<UserDto> RegisterAsync(string email, string displayName, string password)
        {
            try
            {
                await _userRepository.GetByEmailAsync(email);
                throw new InvalidOperationException("User with this email already exists");
            }
            catch (KeyNotFoundException)
            {
                // This is the expected path - user doesn't exist
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                DisplayName = displayName,
                Avatar = $"https://via.placeholder.com/150?text={displayName[0]}",
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return MapToDto(user);
        }

        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                DisplayName = user.DisplayName,
                Avatar = user.Avatar,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt
            };
        }
    }
} 