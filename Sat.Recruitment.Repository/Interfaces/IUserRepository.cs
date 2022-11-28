using Sat.Recruitment.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> SaveUserAsync(User user);
        
        Task<List<User>> ListUsersAsync();
    }
}
