using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanItPoker.Domain.Entities;

namespace PlanItPoker.Domain.Interfaces;

public interface ISprintRepository
{
    Task<IEnumerable<Sprint>> GetAllAsync();
    Task<Sprint> GetByIdAsync(Guid id);
    Task<Sprint> CreateAsync(Sprint sprint);
    Task UpdateAsync(Sprint sprint);
    Task DeleteAsync(Guid id);
} 