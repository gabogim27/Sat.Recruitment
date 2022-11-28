namespace Sat.Recruitment.Services.Interfaces
{
    using Sat.Recruitment.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface IUserService
    {
        Task<bool> CreateUserAsync(User user);

        Task<List<User>> ListUsersAsync();
    }
}
