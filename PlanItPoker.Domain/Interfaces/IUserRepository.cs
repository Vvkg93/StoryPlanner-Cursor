using System.Threading.Tasks;
using PlanItPoker.Domain.Entities;

namespace PlanItPoker.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByDisplayNameAsync(string displayName);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
} 