namespace Sat.Recruitment.Repository.Helpers.Interfaces
{
    using Sat.Recruitment.Domain.Entities;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    
    public interface IUserFileHelper
    {
        Task<bool> SaveUsers(List<User> users);

        Task<bool> SaveUser(User user);

        StreamReader GetUserReader();
    }
}
