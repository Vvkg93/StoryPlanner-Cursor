using System.Threading.Tasks;
using PlanItPoker.Application.DTOs;

namespace PlanItPoker.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(string username, string password);
        Task<UserDto> RegisterAsync(string email, string displayName, string password);
    }
} 